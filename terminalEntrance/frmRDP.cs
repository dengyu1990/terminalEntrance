using System;
using System.Drawing;
using System.Windows.Forms;

namespace terminalEntrance
{
    public partial class frmRDP : Form
    {
        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");
            dllName = dllName.Replace(".", "_");
            if (dllName.EndsWith("_resources")) return null;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());
            byte[] bytes = (byte[])rm.GetObject(dllName);
            return System.Reflection.Assembly.Load(bytes);
        }   
        public frmRDP()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            InitializeComponent();
        }
        bool flag = false;
        AxMSTSCLib.AxMsRdpClient6 rdp;
        private void frmRDP_Load(object sender, EventArgs e)
        {
            //Terminal版本为6.1.7601.17514
            rdp = new AxMSTSCLib.AxMsRdpClient6();
            //布局初始化
            rdp.SetBounds(0, 1, this.Size.Width, this.Size.Height - 1);
            this.Controls.Add(rdp); //将新的控件加入到该集合中（显示）
            pnlMonitor.BackColor = Color.FromArgb(57, 109, 165); //神之混合（与背景色完全一致）

            pnlMonitor.Left = this.Size.Width  - 620; //500
            pnlMonitor.Top = 0;

            lblDisplay.Left = 3;
            lblDisplay.Top = 5;
                        
            btnSwitch.Left = pnlMonitor.Size.Width - 200;
            btnSwitch.Top = 4;

            btnBack.Left = pnlMonitor.Size.Width - 120;
            btnBack.Top = 4;

            btnLock.Left = pnlMonitor.Size.Width - 60;
            btnLock.Top = 4;

            pnlMonitor.Visible = false;
            if (frmLogin.emc)
            {               
                btnSwitch.Visible = false;
                btnBack.Text = "隐藏";
                btnLock.Text = "退出";
                lblDisplay.Text = "您当前为应急模式  " + frmLogin.emcIP;
                RefreshStyle(5);
                initRDP(frmLogin.emcIP, 3389, frmLogin.terUser, frmLogin.terPwd);
                return;
            }
            if (frmTerEnc.offline)
            {
                btnLock.Text = "隐藏";
                btnSwitch.Visible = false;                
                lblDisplay.Text = "您当前已处于离线模式";
                RefreshStyle(4);
            }
            else {
                //定时器开启（实时刷新开始）
                tmrRefresh.Enabled = true;
            }           
            initRDP(frmTerEnc.terIP, 3389, frmLogin.terUser, frmLogin.terPwd);

        }
        private void initRDP(string ip, int port, string user, string pwd)
        {
            rdp.Server = ip;
            rdp.AdvancedSettings2.RDPPort = port;
            rdp.UserName = "ifc\\" + user;
            rdp.AdvancedSettings2.ClearTextPassword = pwd;          
            try {
                rdp.Connect();
                rdp.FullScreenTitle = this.Text;
            }
            catch
            {
                MessageBox.Show("该网络无法连接");
            }            
        }

        private void frmRDP_MouseEnter(object sender, EventArgs e)
        {
            pnlMonitor.Visible = true;
        }

        //一键切换(切换到最闲的机器上去)
        private void btnSwitch_Click(object sender, EventArgs e)
        {
            if(frmTerEnc.terIP == frmTerEnc.minIP)
            {
                rdp.Focus();
                return;
            }           
            rdp.Disconnect();
            rdp.Dispose();
            rdp = new AxMSTSCLib.AxMsRdpClient6();
            rdp.SetBounds(0, 1, this.Size.Width, this.Size.Height - 1);
            this.Controls.Add(rdp); //将新的控件加入到该集合中（显示）
            initRDP(frmTerEnc.minIP, 3389, frmLogin.terUser, frmLogin.terPwd);
            frmTerEnc.terIP = frmTerEnc.minIP;
        }
        //返回（需关闭当前的RDP连接切换回去哦）
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (frmLogin.emc)
            {
                pnlMonitor.Visible = false;
                rdp.Focus();
                return;
            }
            rdp.Disconnect();
            rdp.Dispose();
            this.Close();
            foreach (Form fm in Application.OpenForms)
            {    //千万不要在这个遍历循环里改变集合本身
                if(fm.Name == "frmTerEnc")
                {                   
                    fm.Visible = true;
                }
           }
        }
        //锁定
        int count; //锁定时长（秒）
        int hides = 8; //显示时长（初始默认8秒）
        bool terLock = false;
        private void btnLock_Click(object sender, EventArgs e)
        {
            if (frmLogin.emc)
            {
                DialogResult result = MessageBox.Show("确定要退出当前机器吗？\n", "关闭提醒", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                {
                    Application.Exit();
                    return;
                }
                else
                {                   
                    rdp.Focus();
                    return;
                }
            }
            if (frmTerEnc.offline)
            {
                pnlMonitor.Visible = false;
                rdp.Focus();
                return;
            }
            count = 120;
            terLock = true;
            pnlLock.Visible = true;
            pnlLock.SetBounds(0, 0, this.Size.Width, this.Size.Height);          
        }

        private void txtUnlock_Enter(object sender, EventArgs e)
        {
            txtUnlock.Clear();
            txtUnlock.PasswordChar = '*';
        }

        private void txtUnlock_TextChanged(object sender, EventArgs e)
        {
            //动态验证密码是否正确
            if (txtUnlock.Text == frmLogin.terPwd)
            {
                count = 120;
                terLock = false;
                pnlLock.Visible = false;
                pnlMonitor.Visible = false;
                txtUnlock.PasswordChar = '\0';
                txtUnlock.Text = "输入域帐号密码解锁";
                rdp.Focus();               
            }
        }
        //在锁定面板重绘时控件元素布局
        private void pnlLock_Paint(object sender, PaintEventArgs e)
        {
            lblTime.Left = Convert.ToInt32(this.Size.Width * 0.74);
            lblTime.Top = 300;
            lblName.Left = Convert.ToInt32(this.Size.Width * 0.75);
            lblName.Top = this.Size.Height - 300;
            lblName.Text = frmLogin.cnName;
            txtUnlock.Left = Convert.ToInt32(this.Size.Width * 0.75);
            txtUnlock.Top = this.Size.Height - 270;
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            if (frmTerEnc.offline)
            {
                btnLock.Text = "隐藏";
                btnSwitch.Visible = false;
                lblDisplay.Text = "您当前已处于离线模式";
                RefreshStyle(4);
                tmrRefresh.Enabled = false;
                return;
            }

            if (pnlMonitor.Visible)
            {
                hides--;
                if (hides == 0)
                {
                    pnlMonitor.Visible = false;
                    hides = 7; //七秒钟的记忆
                }
            }

            //判断是否有锁定标记并开始计时
            if (terLock)
            {
                count--;
                if (count == 0)
                {
                    Application.Restart();
                    return;   
                }
                else
                {
                    lblTime.Text = count.ToString();
                }
            }

            if (flag)
            {
                RefreshPanel(2, null);
                return;
            }
            //定制消息及其它东西
            if (DateTime.Now.ToLongTimeString() == "11:50:03")
                flag = true;
            RefreshPanel(1, null);
        }

        public void RefreshPanel(int type, string msg)
        {
            switch (type)
            {
                case 1:
                    lblDisplay.Text = string.Format("{0}  CPU {1}% RAM {2}%", frmTerEnc.terIP, frmTerEnc.curCPU, frmTerEnc.curMEM);
                    RefreshStyle(frmTerEnc.curType);
                    break;
                case 2:
                    pnlMonitor.Visible = true;
                    lblDisplay.Text = "温馨提示：该吃饭了~";
                    lblDisplay.ForeColor = Color.Lime;
                    //btnBack.Text = "好的";
                    btnSwitch.Visible = false;
                    btnBack.Visible = false;
                    btnLock.Visible = false;
                    break;
                case 3:
                    lblDisplay.Text = string.Format("Current Machine: CPU {0}% RAM {1}%", frmTerEnc.curCPU, frmTerEnc.curMEM);
                    break;
            }
        }

        public void RefreshStyle(int type)
        {
            switch (type)
            {
                case 1:
                    pnlMonitor.BackColor = Color.Red;
                    lblDisplay.ForeColor = Color.White;
                    redrawButton(btnSwitch, type);
                    redrawButton(btnBack, type);
                    redrawButton(btnLock, type);
                    break;
                case 2:
                    pnlMonitor.BackColor = Color.Chocolate;
                    lblDisplay.ForeColor = Color.Gold;
                    redrawButton(btnSwitch, type);
                    redrawButton(btnBack, type);
                    redrawButton(btnLock, type);
                    break;
                case 3:
                    pnlMonitor.BackColor = Color.Green;
                    lblDisplay.ForeColor = Color.Lime;
                    redrawButton(btnSwitch, type);
                    redrawButton(btnBack, type);
                    redrawButton(btnLock, type);
                    break;
                case 4:
                    pnlMonitor.BackColor = Color.DimGray;
                    lblDisplay.ForeColor = Color.LightGray;
                    redrawButton(btnBack, type);
                    redrawButton(btnLock, type);
                    break;
                case 5:
                    pnlMonitor.BackColor = Color.Black;
                    lblDisplay.ForeColor = Color.Yellow;
                    redrawButton(btnBack, type);
                    redrawButton(btnLock, type);
                    break;
            }
        }

        public void redrawButton(Button btn,int style)
        {
            switch (style)
            {
                case 1:
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = Color.White;
                    btn.FlatAppearance.MouseOverBackColor = Color.Firebrick;
                    btn.FlatAppearance.MouseDownBackColor = Color.LightCoral;
                    break;
                case 2:
                    btn.ForeColor = Color.Gold;
                    btn.FlatAppearance.BorderColor = Color.Gold;
                    btn.FlatAppearance.MouseOverBackColor = Color.Sienna;
                    btn.FlatAppearance.MouseDownBackColor = Color.DarkOrange;
                    break;
                case 3:
                    btn.ForeColor = Color.Lime;
                    btn.FlatAppearance.BorderColor = Color.Lime;
                    btn.FlatAppearance.MouseOverBackColor = Color.DarkGreen;
                    btn.FlatAppearance.MouseDownBackColor = Color.SeaGreen;
                    break;
                case 4:
                    btn.ForeColor = Color.LightGray;
                    btn.FlatAppearance.BorderColor = Color.LightGray;
                    btn.FlatAppearance.MouseOverBackColor = Color.SlateGray;
                    btn.FlatAppearance.MouseDownBackColor = Color.LightSlateGray;
                    break;
                case 5:
                    btn.ForeColor = Color.Yellow;
                    btn.FlatAppearance.BorderColor = Color.Yellow;
                    btn.FlatAppearance.MouseOverBackColor = Color.Maroon;
                    btn.FlatAppearance.MouseDownBackColor = Color.Brown;
                    break;
            }
        }
    }
}
