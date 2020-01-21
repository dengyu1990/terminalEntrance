using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace terminalEntrance
{
    public partial class frmTerEnc : Form
    {
        public frmTerEnc()
        {
            InitializeComponent();
        }
        //新建远程终端实例
        frmRDP rdpFrm;
        //获取当前屏幕分辨率
        int sWidth = Screen.PrimaryScreen.Bounds.Width;
        int sHeight = Screen.PrimaryScreen.Bounds.Height;
        //离线模式
        public static bool offline = false;
        //当前连接终端的IP静态变量
        public static string terIP = "";
        public static int curCPU = 0;
        public static int curMEM = 0;
        public static int curType = 0;
        public static string minIP = "";
        //创建接受性能数据的Socket
        public static Socket ClientSocket { get; set; }
        //存储性能数据（以ip作为区分粒度）
        Dictionary<string, string> perData = new Dictionary<string, string>();
        //创建Terminal终端（以获取性能ip粒度为准）
        Button[] terMachines;

        //初始化加载事件函数
        private void frmTerEnc_Load(object sender, EventArgs e)
        {    
            this.SetBounds(0, 0, sWidth, sHeight);            
            //设置退出和关于按钮的界面布局           
            btnExit.Left = sWidth - 160;
            btnExit.Top = sHeight - 50;
            lblUserName.Text = frmLogin.cnName;            
            lblStatus.Top = 33;
            lblStatus.Text = "------------[ Performance data initializing ]------------";
            lblStatus.Left = (sWidth - 560) / 2;
            //根据不同通道设置不同背景
            switch (frmLogin.channel)
            {
                case 1:
                    this.BackColor = Color.DarkSlateGray;
                    break;
                case 2:
                    this.BackColor = Color.DarkSlateBlue;
                    break;
                case 3:
                    this.BackColor = Color.SaddleBrown;
                    break;
            }
            //发送与接受消息
            Thread thread = new Thread(new ParameterizedThreadStart(ReceiveData));
            thread.IsBackground = true;
            thread.Start(ClientSocket);
            tmrRefresh.Enabled = true;
            return;
        }

        public void ReceiveData(object socket)
        {
            var proxSocket = socket as Socket;
            byte[] data = new byte[1024 * 1024];
            while (true)
            {
                int len = 0;
                try
                {
                    len = proxSocket.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch
                {
                    RefreshInMainThread(1, "离线模式（仍可访问Terminal）"); //开始进入离线模式（需在主线程里刷新）
                    StopConnect();
                    return;
                }
                if (len <= 0)
                {
                    //客户端正常退出
                    RefreshInMainThread(2, "该分组的远程性能Agent未启用（请联系邹智勇老师）");
                    StopConnect();
                    return; //让方法结束，终结当前接受客户端数据的异步线程
                }
                //把接受到的数据放到文本框输出上
                string recStr = Encoding.Default.GetString(data, 0, len);
                if (!recStr.Contains(';')) //当只有一条数据时是没有分割符；的
                {
                    if (perData.Keys.Count > 1)
                    {
                        perData.Clear();
                    }
                    PerStorage(recStr);
                    continue;
                }
                string[] terStr = recStr.Split(';');                            
                //当性能数据变少时的动态调整
                if (perData.Keys.Count > terStr.Count())
                {
                    List<string> perIPS = new List<string>();
                    foreach (string str in terStr)
                    {
                        string[] perStr = str.Split('-');
                        perIPS.Add(perStr[0]);
                    }
                    //开始进行差异匹配
                    string[] ips = perData.Keys.Except(perIPS).ToArray();
                    foreach (string ip in ips)
                    {
                        perData.Remove(ip); //移除掉未接受数据
                    }
                    return;
                }                              
                foreach (string str in terStr)
                {
                    PerStorage(str);
                }
            }
        }

        private void PerStorage(string pstr)
        {
            string[] perStr = pstr.Split('-');
            string ip = perStr[0];
            string per = "  CPU:" + perStr[1] + "%  RAM:" + perStr[2] + "%";
            //性能数据存储
            if (perData.ContainsKey(ip))
            {
                perData[ip] = per;
            }
            else
            {
                perData.Add(ip, per); //新增性能数据
            }
        }

        private void StopConnect()
        {
            try
            {
                if (ClientSocket.Connected)
                {
                    ClientSocket.Shutdown(SocketShutdown.Both);
                    ClientSocket.Close(100); //如果100秒还没关闭就强制关闭
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
        //在主线程里刷新字段综合函数
        public void RefreshInMainThread(int type, string str)
        {
            if (lblMainThread.InvokeRequired)
            {
                switch (type)
                {
                    case 1:  //进入离线模式（仍可访问Terminal终端）
                        lblMainThread.Invoke(new Action<string>(s =>
                        {                                                        
                            offline = true;
                            tmrRefresh.Enabled = false;
                            lblStatus.Text = "------------[  " + s + " ]------------";
                            MessageBox.Show("已自动切换为离线模式，Terminal仍可用！\n", "远程服务异常", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (terMachines == null)
                                return;
                            foreach (Button btn in terMachines)
                            {
                                btn.FlatAppearance.BorderColor = Color.LightGray;
                                btn.ForeColor = Color.LightGray;
                                btn.BackColor = Color.DimGray;
                                btn.FlatAppearance.MouseOverBackColor = Color.SlateGray;
                                btn.FlatAppearance.MouseDownBackColor = Color.LightSlateGray;
                                btn.Cursor = Cursors.PanNW;
                            }
                        }), str);
                        break;
                    case 2:
                        lblMainThread.Invoke(new Action<string>(s =>
                        {
                            lblStatus.Text = "------------[  " + s + " ]------------";
                        }), str);
                        break;
                    case 3:
                        lblMainThread.Invoke(new Action<string>(s =>
                        {

                        }), str);
                        break;
                }
            }
            else {
                switch (type)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                }
            }
        }

        //生成按钮的超级方法（只跑一遍是极好的）
        private void terMachine_Create(int nums)
        {
            terMachines = new Button[nums];
            for (int i = 0; i < nums; i++)
            {
                terMachines[i] = new Button();
                int interval = (sWidth - 900) / 4;
                terMachines[i].Location = new Point(interval + (i % 3) * (interval + 300), 160 + (i / 3) * 72);
                terMachines[i].Size = new Size(300, 30);
                this.Controls.Add(terMachines[i]);
                terMachines[i].BackColor = Color.Transparent;
                terMachines[i].FlatStyle = FlatStyle.Flat;
                terMachines[i].Font = new Font("微软雅黑", 12);
                terMachines[i].ForeColor = Color.SkyBlue;
                terMachines[i].FlatAppearance.MouseOverBackColor = Color.Teal;                
                terMachines[i].Text = "Terminal Connecting...";                
                terMachines[i].Click += new System.EventHandler(connTerminal_Click);
            }
            int n = 0;
            foreach (string key in perData.Keys)
            {
                terMachines[n++].Tag = key;
            }
        }

        //公共访问主函数
        private void connTerminal_Click(object sender, EventArgs e)
        {
            terIP = ((Button)sender).Tag.ToString();
            rdpFrm = new frmRDP();
            rdpFrm.SetBounds(0, 0, sWidth, sHeight);
            rdpFrm.Show();
            this.Hide();
        }

        //退出按钮（关闭应用）
        private void btnExit_Click(object sender, EventArgs e)
        {
             //退出之前必须关闭Socket连接，释放资源
            StopConnect();
            Application.Exit();
        }
        //刷新主定时器（如果界面没有Terminal会优先创建）
        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            int pers = perData.Keys.Count;
            if (terMachines == null )
            {
                if (pers > 0)
                {
                    terMachine_Create(pers);
                }
                return;
            }
            int nums = terMachines.Count();
            if (pers != nums)
            {
                for (int i = 0; i < nums; i++)
                {
                    this.Controls.Remove(terMachines[i]);
                }
                terMachine_Create(pers);
                return;
            }
            RefreshPerformance();
        }

        //根据性能数组动态生成按钮并刷新数据
        public void RefreshPerformance()
        {       
            int nums = terMachines.Count();
            int avgCPU = 0;
            int avgMEM = 0;
            int minCPU = 100;          
            for (int i = 0; i < nums; i++)
            {
                string ip = terMachines[i].Tag.ToString();
                string per = perData[ip];              
                terMachines[i].Text = ip + per;
                string[] strs = per.Split(new char[2] { ':', '%' });
                int cpu = Convert.ToInt32(strs[1]);
                int mem = Convert.ToInt32(strs[3]);
                //每次来有cpu的值哦
                if (cpu < minCPU)
                {
                    minIP = ip;
                    minCPU = cpu;
                }

                if (terIP == ip)
                {
                    curCPU = cpu;
                    curMEM = mem;
                    curType = perAnalysis(curCPU, curMEM);
                }
                int type = perAnalysis(cpu, mem);
                avgCPU += cpu;
                avgMEM += mem;
                //性能主控数据刷新，并改变按钮颜色
                switch (type)
                {
                    case 1:
                        terMachines[i].FlatAppearance.BorderColor = Color.White;
                        terMachines[i].ForeColor = Color.White;
                        terMachines[i].BackColor = Color.Red;
                        terMachines[i].FlatAppearance.MouseOverBackColor = Color.Firebrick;
                        terMachines[i].FlatAppearance.MouseDownBackColor = Color.LightCoral;
                        terMachines[i].Cursor = Cursors.No;
                        break;
                    case 2:
                        terMachines[i].FlatAppearance.BorderColor = Color.Gold;
                        terMachines[i].ForeColor = Color.Gold;
                        terMachines[i].BackColor = Color.Chocolate;
                        terMachines[i].FlatAppearance.MouseOverBackColor = Color.Sienna;
                        terMachines[i].FlatAppearance.MouseDownBackColor = Color.DarkOrange;
                        terMachines[i].Cursor = Cursors.Help;
                        break;
                    case 3:
                        terMachines[i].FlatAppearance.BorderColor = Color.Lime;
                        terMachines[i].ForeColor = Color.Lime;
                        terMachines[i].BackColor = Color.Green;
                        terMachines[i].FlatAppearance.MouseOverBackColor = Color.DarkGreen;
                        terMachines[i].FlatAppearance.MouseDownBackColor = Color.SeaGreen;
                        terMachines[i].Cursor = Cursors.Hand;
                        break;
                }
            }
            avgCPU /= nums;
            avgMEM /= nums;
            lblStatus.Text = string.Format("---[ {0}  {1}  可用：{2}台  CPU平均负载：{3}%  RAM平均负载:{4}% ]---", DateTime.Now.ToString("F"), frmLogin.msgChl, nums, avgCPU, avgMEM);
        }

        //性能分析核心算法
        private int perAnalysis(int cpu, int mem)
        {
            if (cpu > 95)
                return 1;
            else if (cpu > 85 || mem > 90)
                return 2;
            else
                return 3;
        }
    }
}
