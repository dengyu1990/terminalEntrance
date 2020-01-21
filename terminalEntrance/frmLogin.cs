using System;
using System.Drawing;
using System.Windows.Forms;
using System.DirectoryServices;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;

namespace terminalEntrance
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        frmTerEnc terEnc = new frmTerEnc();
        public static bool emc = false;
        public static string emcIP = "";
        public static string terUser = "";
        public static string terPwd = "";
        public static string cnName = "";
        public static int channel = -1;
        public static string msgChl = "应用";
        public const string serverIP = "10.30.1.12"; //主服务器配置修改点
        private void btnLogin_Click(object sender, EventArgs e)
        {    
            if (emc)
            {
                btnLogin.Enabled = false;
                string ip = txtUsername.Text.Trim();
                string regformat = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
                Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
                if (!string.IsNullOrEmpty(ip) && ip.Length >= 7 && ip.Length <= 15 && regex.IsMatch(ip))
                {
                    emcIP = txtUsername.Text;
                    frmRDP rdpFrm = new frmRDP();
                    rdpFrm.SetBounds(0, 0, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                    rdpFrm.Show();
                    this.Hide();
                    return;
                }
                else
                {
                    MessageBox.Show("您输入的IP地址不规范，请重新输入\n", "验证失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnLogin.Enabled = true;
                    txtUsername.Text = "";
                    return;
                }
            }                     
            terUser = txtUsername.Text.Trim();
            terPwd = txtPassword.Text.Trim();
            btnLogin.Visible = false;
            lblStatus.Visible = true;
            lblStatus.Text = "验证中...";
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            string dePath = @"LDAP://10.22.12.10/DC=IFC,DC=local";
            PAIC:
            using (DirectoryEntry deUser = new DirectoryEntry(dePath, terUser, terPwd))
            {
                DirectorySearcher src = new DirectorySearcher(deUser);
                src.Filter = "(&(&(objectCategory=person)(objectClass=user))(sAMAccountName=" + terUser + "))";
                src.PropertiesToLoad.Add("cn");
                src.SearchRoot = deUser;
                src.SearchScope = SearchScope.Subtree;
                SearchResult result;
                try
                {
                    result = src.FindOne();
                }
                catch
                {
                    lblStatus.Text = "密码错误";
                    lblStatus.ForeColor = Color.Red;
                    lblStatus.BackColor = Color.Yellow;
                    txtPassword.Enabled = true;
                    txtPassword.Text = "";
                    return;
                }
                if (result != null)
                {
                    DirectoryEntry de = result.GetDirectoryEntry();
                    string cn = de.Name;
                    cnName = cn.Substring(3);
                    channel = cboChannel.SelectedIndex;
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    frmTerEnc.ClientSocket = socket;                 
                    try                 
                    {   
                        switch (channel)
                        {
                            case 0:
                                socket.Connect(IPAddress.Parse(serverIP), 3301);
                                break;
                            case 1:
                                socket.Connect(IPAddress.Parse(serverIP), 3302);
                                msgChl = "开发";
                                break;
                            case 2:
                                socket.Connect(IPAddress.Parse(serverIP), 3303);
                                msgChl = "系统";
                                break;
                            case 3:
                                socket.Connect(IPAddress.Parse(serverIP), 3304);
                                msgChl = "其它";
                                break;
                        }

                    }
                    catch
                    {                     
                        EmergencyMode();
                        return;
                    }
                    terEnc.Show();
                    this.Hide();
                }
                else
                {
                    dePath = @"LDAP://10.1.33.12/DC=IFC,DC=local";
                    goto PAIC;
                }
            }
        }

        public void EmergencyMode()
        {
            emc = true;
            lblVersion.Text = "应急模式（无性能数据）";
            lblUser.Text = "访问IP";
            lblUser.ForeColor = Color.Red;
            lblPwd.Text = "请直接输入IP即可访问";
            lblPwd.ForeColor = Color.Red;
            lblStatus.Visible = false;
            lblRight.ForeColor = Color.Black;
            txtUsername.Text = "";
            txtUsername.Enabled = true;
            txtUsername.ForeColor = Color.Red;
            txtUsername.BackColor = Color.Yellow;
            txtPassword.Visible = false;
            btnLogin.Text = "访 问";
            btnLogin.Visible = true;
            btnLogin.FlatAppearance.MouseOverBackColor = Color.Red;
            this.BackColor = Color.Black;
            lblTitle.ForeColor = Color.Yellow;
            lblVersion.ForeColor = Color.Yellow;
            cboChannel.Visible = false;           
        }


        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            btnLogin.Visible = (txtUsername.Text.Trim().Length > 2 && txtPassword.Text.Trim().Length > 3) ? true : false;
            lblStatus.Visible = !btnLogin.Visible;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            cboChannel.SelectedItem = cboChannel.Items[0];                  
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cboChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboChannel.SelectedIndex)
            {
                case 0:
                    this.BackColor = Color.SteelBlue;
                    lblVersion.Text = "IFC Terminal Access";
                    break;
                case 1:
                    this.BackColor = Color.DarkSlateGray;
                    lblVersion.Text = "开发";
                    break;
                case 2:
                    this.BackColor = Color.DarkSlateBlue;
                    lblVersion.Text = "系统";
                    break;
                case 3:
                    this.BackColor = Color.SaddleBrown;
                    lblVersion.Text = "其它";
                    break;
            }
        }
    }
}
