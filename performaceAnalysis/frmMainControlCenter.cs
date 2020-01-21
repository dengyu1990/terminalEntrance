using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace performaceAnalysis
{
    public partial class frmMainControlCenter : Form
    {
        public frmMainControlCenter()
        {
            InitializeComponent();
        }
        //前缀：Operation应用，Development开发，System系统，Detail其它
        //保存所有连接上来的Terminal终端
        List<Socket> opeTerminalProxSocketList = new List<Socket>();
        List<Socket> devTerminalProxSocketList = new List<Socket>();
        List<Socket> sysTerminalProxSocketList = new List<Socket>();
        List<Socket> retTerminalProxSocketList = new List<Socket>();
        //保存所有连接上的Client终端
        List<Socket> opeClientProxSocketList = new List<Socket>();
        List<Socket> devClientProxSocketList = new List<Socket>();
        List<Socket> sysClientProxSocketList = new List<Socket>();
        List<Socket> retClientProxSocketList = new List<Socket>();

        public const string serverIP = "10.15.32.33"; //主服务器配置修改点

        //存储性能数据（以ip作为区分粒度）
        Dictionary<string, string> opeData = new Dictionary<string, string>();
        Dictionary<string, string> devData = new Dictionary<string, string>();
        Dictionary<string, string> sysData = new Dictionary<string, string>();
        Dictionary<string, string> retData = new Dictionary<string, string>();

        //应用
        private void btnOpeStart_Click(object sender, EventArgs e)
        {
            InitSockets(3331, 3301, 1);
            grpOpe.Text = "应用（服务运行中）";
            btnOpeStart.Visible = false;
        }
        //开发
        private void btnDevStart_Click(object sender, EventArgs e)
        {
            InitSockets(3332, 3302, 2);
            grpDev.Text = "开发（服务运行中）";
            btnDevStart.Visible = false;
        }
        //系统
        private void btnSysStart_Click(object sender, EventArgs e)
        {
            InitSockets(3333, 3303, 3);
            grpSys.Text = "系统（服务运行中）";
            btnSysStart.Visible = false;
        }
        //其它
        private void btnRetStart_Click(object sender, EventArgs e)
        {
            InitSockets(3334, 3304, 4);
            grpRet.Text = "其它（服务运行中）";
            btnRetStart.Visible = false;
        }
        //备用端口（管理：3300/3330，备用：3305/3335）

        public class sockCategory
        {
            public Socket sockIns;
            public List<Socket> sockList;
            public Dictionary<string, string> perData;
        }

        public void InitSockets(int recvPort, int sendPort, int type)
        {
            //创建Socket对象（分拆成Terminal和Client端）
            Socket terminalSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //绑定IP和端口
            terminalSocket.Bind(new IPEndPoint(IPAddress.Parse(serverIP), recvPort));
            clientSocket.Bind(new IPEndPoint(IPAddress.Parse(serverIP), sendPort));
            //开启侦听
            terminalSocket.Listen(72); //连接等待队列（同时来了72个连接，只能处理一个，队列里10个等待，其余返回错误）
            clientSocket.Listen(33); //连接等待队列（同时来了33个连接，只能处理一个，队列里10个等待，其余返回错误）
            //开始接受客户端的连接
            switch (type)
            {
                case 1:
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptTerminalConnect), new sockCategory { sockIns = terminalSocket, sockList = opeTerminalProxSocketList, perData = opeData });
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptClientConnect), new sockCategory { sockIns = clientSocket, sockList = opeClientProxSocketList });
                    break;
                case 2:
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptTerminalConnect), new sockCategory { sockIns = terminalSocket, sockList = devTerminalProxSocketList, perData = devData });
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptClientConnect), new sockCategory { sockIns = clientSocket, sockList = devClientProxSocketList });
                    break;
                case 3:
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptTerminalConnect), new sockCategory { sockIns = terminalSocket, sockList = sysTerminalProxSocketList, perData = sysData });
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptClientConnect), new sockCategory { sockIns = clientSocket, sockList = sysClientProxSocketList });
                    break;
                case 4:
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptTerminalConnect), new sockCategory { sockIns = terminalSocket, sockList = retTerminalProxSocketList, perData = retData });
                    ThreadPool.QueueUserWorkItem(new WaitCallback(this.AcceptClientConnect), new sockCategory { sockIns = clientSocket, sockList = retClientProxSocketList });
                    break;                                  
            }            
                 
        }

        //接受Termianl的连接
        public void AcceptTerminalConnect(object varSoket)
        {
            sockCategory sc = varSoket as sockCategory;
            this.AppendToLOG("主控中心开始接受所有Terminal端性能数据传输...");
            while (true)
            {
                var proxSocket = sc.sockIns.Accept();
                sc.sockList.Add(proxSocket);
                this.AppendToLOG(string.Format("Terminal端{0}已连接性能数据接受链路...", proxSocket.RemoteEndPoint.ToString()));
                //不停地接受当前连接的客户端发送来的消息
                ThreadPool.QueueUserWorkItem(new WaitCallback(ReceiveData), new sockCategory { sockIns = proxSocket, sockList = sc.sockList, perData = sc.perData });
            }
        }

        //接受Client的连接
        public void AcceptClientConnect(object varSoket)
        {
            sockCategory sc = varSoket as sockCategory;
            this.AppendToLOG("主控中心开始接受所有Client端性能数据传输...");
            while (true)
            {
                var proxSocket = sc.sockIns.Accept();
                sc.sockList.Add(proxSocket);
                this.AppendToLOG(string.Format("Client端{0}已连接性能数据发送链路...", proxSocket.RemoteEndPoint.ToString()));
            }
        }

        public void ReceiveData(object varSoket)
        {
            sockCategory sc = varSoket as sockCategory;
            string tmp = sc.sockIns.RemoteEndPoint.ToString(); //超隐蔽BUG "10.14.206.176:55778"
            string terminalIP = tmp.Substring(0, tmp.IndexOf(':'));
            byte[] data = new byte[1024 * 1024];  
            while (true)
            {
                int len = 0;
                try
                {
                    len = sc.sockIns.Receive(data, 0, data.Length, SocketFlags.None);
                }
                catch(Exception ex)
                {
                    //Terminal端异常退出      
                    StopConnect(sc.sockIns);
                    sc.sockList.Remove(sc.sockIns);                                      
                    sc.perData.Remove(terminalIP);//性能数据里移除掉该terminal                   
                    AppendToLOG(string.Format("Terminal端：{0}异常退出,{1}", terminalIP, ex.ToString()));
                    return;

                }
                if (len <= 0)
                {
                    //Terminal端正常退出                                        
                    StopConnect(sc.sockIns);
                    sc.sockList.Remove(sc.sockIns);
                    sc.perData.Remove(terminalIP);//性能数据里移除掉该terminal
                    AppendToLOG(string.Format("Terminal端：{0}正常退出", terminalIP));
                    return; //让方法结束，终结当前接受客户端数据的异步线程
                }
                //把接受到的数据放到文本框输出上
                string[] strPer = (terminalIP + Encoding.Default.GetString(data, 0, len)).Split(':');
                string ip = strPer[0];
                string per = strPer[1] + "-" + strPer[2];
                //性能数据存储
                if (sc.perData.ContainsKey(ip))
                {
                    sc.perData[ip] = per;
                }else {
                    sc.perData.Add(ip, per);
                }
                //AppendToLOG(string.Format("{0}{1}", ip, per));
            }
        }

        private void StopConnect(Socket proxSocket)
        {
            try
            {
                if (proxSocket.Connected)
                {
                    proxSocket.Shutdown(SocketShutdown.Both);
                    proxSocket.Close(100);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("执行StopConnect()出现异常：" + ex.ToString());
            }
        }

        //往日志输出框里追加数据的 
        public void AppendToLOG(string str)
        {
            if (rtxtLog.InvokeRequired)  //
            {
                rtxtLog.Invoke(new Action<string>(s =>   //去找文本框所在线程去执行相应方法(主力执行)
                {
                    this.rtxtLog.Text = string.Format("{0}\r\n{1}", s, rtxtLog.Text);
                }), str);
            }
            else
            {
                this.rtxtLog.Text = string.Format("{0}\r\n{1}", str, rtxtLog.Text);
            }
        }

        public void avgPerformace()
        {
            avgCalculate(opeTerminalProxSocketList, opeClientProxSocketList, new Dictionary<string, string>(opeData), lblOpeTerminal, lblOpeClient, lblOpeAvgPerformance);
            avgCalculate(devTerminalProxSocketList, devClientProxSocketList, new Dictionary<string, string>(devData), lblDevTerminal, lblDevClient, lblDevAvgPerformance);
            avgCalculate(sysTerminalProxSocketList, sysClientProxSocketList, new Dictionary<string, string>(sysData), lblSysTerminal, lblSysClient, lblSysAvgPerformance);
            avgCalculate(retTerminalProxSocketList, retClientProxSocketList, new Dictionary<string, string>(retData), lblRetTerminal, lblRetClient, lblRetAvgPerformance);
        }

        public void avgCalculate(List<Socket> terminals, List<Socket> clients, Dictionary<string, string> perData, Label ter, Label clt, Label avg)
        {
            int avgCPU = 0;
            int avgMEM = 0;
            ter.Text = "已连接的Terminal数量：" + terminals.Count + "台";
            clt.Text = "已连接的Client数量：" + clients.Count + "台";
            if (perData.Count == 0)
            {
                avg.Text = "CPU平均负载：0%  RAM平均负载：0%";
                return;
            }
            try
            {
                foreach (string per in perData.Values)
                {
                    string[] cms = per.Split('-');
                    avgCPU += Convert.ToInt32(cms[0]);
                    avgMEM += Convert.ToInt32(cms[1]);
                }
            }
            catch (Exception ex)
            {
                AppendToLOG("执行avgCalculate->foreach()出现异常：" + ex.ToString());
            }

            
            avgCPU /= perData.Count;
            avgMEM /= perData.Count;
            avg.Text = string.Format("CPU平均负载：{0}%  RAM平均负载：{1}%", avgCPU, avgMEM);
        }

        public void SendData()
        {
            prepareData(opeClientProxSocketList, new Dictionary<string, string>(opeData));
            prepareData(devClientProxSocketList, new Dictionary<string, string>(devData));
            prepareData(sysClientProxSocketList, new Dictionary<string, string>(sysData));
            prepareData(retClientProxSocketList, new Dictionary<string, string>(retData));
        }

        public void prepareData(List<Socket> clients, Dictionary<string, string> perData)
        {
            foreach (var proxSocket in clients)
            {
                if (proxSocket!=null && proxSocket.Connected) //需要前端建立连接之后才会发送数据（执行下列代码）
                {
                    string strData = null;
                    string clientIP = proxSocket.RemoteEndPoint.ToString();
                    try
                    {
                        foreach (string ip in perData.Keys)    //超隐蔽BUG:集合已修改，无法执行枚举操作
                        {
                            strData = ip + "-" + perData[ip] + ";" + strData;
                        }
                    }
                    catch (Exception ex)
                    {
                        AppendToLOG("执行prepareData->foreach()出现异常：" + ex.ToString());
                    }
                    //AppendToLOG(strData); 为什么断掉一个Terminal连接之后仍然保存着这个数据呢
                    if (strData == null)
                        return;
                    byte[] data = Encoding.Default.GetBytes(strData.TrimEnd(';'));
                    try
                    {
                        proxSocket.Send(data, 0, data.Length, SocketFlags.None);
                    }
                    catch
                    {
                        //发送失败（客户端已经关闭）
                        clients.Remove(proxSocket);
                        StopConnect(proxSocket);
                        //AppendToLOG("执行prepareData->proxSocket.Send()出现异常：" + ex.ToString());
                        return;
                    }
                }
            }
        }

        int count = 33;
        bool manaMode = true;
        #region 主控中心
        //发送消息
        private void tmrMainControl_Tick(object sender, EventArgs e)
        {
            //判断是否有锁定标记并开始计时
            if (manaMode)
            {
                count--;
                lblTime.Text = "操作剩余" + count + "秒";
                if (count == 0)
                {
                    manaMode = false;
                    btnOpeStart.Enabled = false;
                    btnDevStart.Enabled = false;
                    btnSysStart.Enabled = false;
                    btnRetStart.Enabled = false;
                    btnExit.Enabled = false;
                    lblTime.Visible = false;
                    txtPwd.Visible = true;
                }
            }
            //主执行方法（每1000毫秒执行一次哦）
            try
            {
                SendData();              
            }
            catch(Exception ex)
            {
                AppendToLOG("执行Tick里的SendData出现异常：" + ex.ToString());
            }

            try
            {
                avgPerformace();
            }
            catch (Exception ex)
            {
                AppendToLOG("执行Tick里的avgPerformace出现异常：" + ex.ToString());
            }  
        }
        #endregion

        private void txtPwd_Enter(object sender, EventArgs e)
        {
            txtPwd.Clear();
            txtPwd.PasswordChar = '*';
        }

        private void txtPwd_TextChanged(object sender, EventArgs e)
        {
            if (txtPwd.Text == "3333")
            {
                manaMode = true;
                txtPwd.Visible = false;
                txtPwd.PasswordChar = '\0';
                txtPwd.Text = "管理员密码";
                count = 33;
                btnOpeStart.Enabled = true;
                btnDevStart.Enabled = true;
                btnSysStart.Enabled = true;
                btnRetStart.Enabled = true;
                btnExit.Enabled = true;
                lblTime.Visible = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("确定要停止所有服务 然后退出？\n", "Terminal Access Server", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (exit == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                return;
            }
        }

        private void frmMainControlCenter_Load(object sender, EventArgs e)
        {
            txtPwd.Visible = false;
            lblTime.Focus();
            //开启主控定时任务           
            tmrMainControl.Enabled = true;
        }
    }
}
