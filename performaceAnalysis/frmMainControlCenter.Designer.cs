namespace performaceAnalysis
{
    partial class frmMainControlCenter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMainControlCenter));
            this.btnOpeStart = new System.Windows.Forms.Button();
            this.lblOpeTerminal = new System.Windows.Forms.Label();
            this.lblOpeClient = new System.Windows.Forms.Label();
            this.tmrMainControl = new System.Windows.Forms.Timer(this.components);
            this.grpOpe = new System.Windows.Forms.GroupBox();
            this.lblOpeAvgPerformance = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.grpDev = new System.Windows.Forms.GroupBox();
            this.lblDevAvgPerformance = new System.Windows.Forms.Label();
            this.lblDevClient = new System.Windows.Forms.Label();
            this.lblDevTerminal = new System.Windows.Forms.Label();
            this.btnDevStart = new System.Windows.Forms.Button();
            this.grpSys = new System.Windows.Forms.GroupBox();
            this.lblSysAvgPerformance = new System.Windows.Forms.Label();
            this.lblSysClient = new System.Windows.Forms.Label();
            this.lblSysTerminal = new System.Windows.Forms.Label();
            this.btnSysStart = new System.Windows.Forms.Button();
            this.grpRet = new System.Windows.Forms.GroupBox();
            this.lblRetAvgPerformance = new System.Windows.Forms.Label();
            this.lblRetClient = new System.Windows.Forms.Label();
            this.lblRetTerminal = new System.Windows.Forms.Label();
            this.btnRetStart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.grpOpe.SuspendLayout();
            this.grpDev.SuspendLayout();
            this.grpSys.SuspendLayout();
            this.grpRet.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpeStart
            // 
            this.btnOpeStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpeStart.Location = new System.Drawing.Point(340, 55);
            this.btnOpeStart.Name = "btnOpeStart";
            this.btnOpeStart.Size = new System.Drawing.Size(85, 35);
            this.btnOpeStart.TabIndex = 12;
            this.btnOpeStart.Text = "开启服务";
            this.btnOpeStart.UseVisualStyleBackColor = true;
            this.btnOpeStart.Click += new System.EventHandler(this.btnOpeStart_Click);
            // 
            // lblOpeTerminal
            // 
            this.lblOpeTerminal.AutoSize = true;
            this.lblOpeTerminal.ForeColor = System.Drawing.Color.White;
            this.lblOpeTerminal.Location = new System.Drawing.Point(24, 94);
            this.lblOpeTerminal.Name = "lblOpeTerminal";
            this.lblOpeTerminal.Size = new System.Drawing.Size(201, 20);
            this.lblOpeTerminal.TabIndex = 16;
            this.lblOpeTerminal.Text = "已连接的Terminal数量：0台";
            // 
            // lblOpeClient
            // 
            this.lblOpeClient.AutoSize = true;
            this.lblOpeClient.ForeColor = System.Drawing.Color.White;
            this.lblOpeClient.Location = new System.Drawing.Point(24, 62);
            this.lblOpeClient.Name = "lblOpeClient";
            this.lblOpeClient.Size = new System.Drawing.Size(180, 20);
            this.lblOpeClient.TabIndex = 17;
            this.lblOpeClient.Text = "已连接的Client数量：0台";
            // 
            // tmrMainControl
            // 
            this.tmrMainControl.Interval = 1000;
            this.tmrMainControl.Tick += new System.EventHandler(this.tmrMainControl_Tick);
            // 
            // grpOpe
            // 
            this.grpOpe.Controls.Add(this.lblOpeAvgPerformance);
            this.grpOpe.Controls.Add(this.lblOpeClient);
            this.grpOpe.Controls.Add(this.lblOpeTerminal);
            this.grpOpe.Controls.Add(this.btnOpeStart);
            this.grpOpe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpOpe.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpOpe.ForeColor = System.Drawing.Color.White;
            this.grpOpe.Location = new System.Drawing.Point(77, 161);
            this.grpOpe.Name = "grpOpe";
            this.grpOpe.Size = new System.Drawing.Size(468, 136);
            this.grpOpe.TabIndex = 18;
            this.grpOpe.TabStop = false;
            this.grpOpe.Text = "应用（服务未开启）";
            // 
            // lblOpeAvgPerformance
            // 
            this.lblOpeAvgPerformance.AutoSize = true;
            this.lblOpeAvgPerformance.ForeColor = System.Drawing.Color.White;
            this.lblOpeAvgPerformance.Location = new System.Drawing.Point(24, 30);
            this.lblOpeAvgPerformance.Name = "lblOpeAvgPerformance";
            this.lblOpeAvgPerformance.Size = new System.Drawing.Size(277, 20);
            this.lblOpeAvgPerformance.TabIndex = 18;
            this.lblOpeAvgPerformance.Text = "CPU平均负载：0%  RAM平均负载：0%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(124, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(287, 38);
            this.label1.TabIndex = 19;
            this.label1.Text = "冰鱼终端系统 服务端";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(133, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(245, 28);
            this.label2.TabIndex = 20;
            this.label2.Text = "Terminal Access Server";
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.SystemColors.Window;
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPwd.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPwd.ForeColor = System.Drawing.Color.Teal;
            this.txtPwd.Location = new System.Drawing.Point(966, 50);
            this.txtPwd.MaxLength = 12;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(81, 26);
            this.txtPwd.TabIndex = 21;
            this.txtPwd.Text = "管理员密码";
            this.txtPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
            this.txtPwd.Enter += new System.EventHandler(this.txtPwd_Enter);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(782, 48);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(129, 25);
            this.lblTime.TabIndex = 22;
            this.lblTime.Text = "操作剩余33秒";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SkyBlue;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.CadetBlue;
            this.btnExit.Location = new System.Drawing.Point(1142, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 40);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // grpDev
            // 
            this.grpDev.Controls.Add(this.lblDevAvgPerformance);
            this.grpDev.Controls.Add(this.lblDevClient);
            this.grpDev.Controls.Add(this.lblDevTerminal);
            this.grpDev.Controls.Add(this.btnDevStart);
            this.grpDev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpDev.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpDev.ForeColor = System.Drawing.Color.White;
            this.grpDev.Location = new System.Drawing.Point(626, 161);
            this.grpDev.Name = "grpDev";
            this.grpDev.Size = new System.Drawing.Size(468, 136);
            this.grpDev.TabIndex = 24;
            this.grpDev.TabStop = false;
            this.grpDev.Text = "开发（服务未开启）";
            // 
            // lblDevAvgPerformance
            // 
            this.lblDevAvgPerformance.AutoSize = true;
            this.lblDevAvgPerformance.ForeColor = System.Drawing.Color.White;
            this.lblDevAvgPerformance.Location = new System.Drawing.Point(24, 30);
            this.lblDevAvgPerformance.Name = "lblDevAvgPerformance";
            this.lblDevAvgPerformance.Size = new System.Drawing.Size(277, 20);
            this.lblDevAvgPerformance.TabIndex = 18;
            this.lblDevAvgPerformance.Text = "CPU平均负载：0%  RAM平均负载：0%";
            // 
            // lblDevClient
            // 
            this.lblDevClient.AutoSize = true;
            this.lblDevClient.ForeColor = System.Drawing.Color.White;
            this.lblDevClient.Location = new System.Drawing.Point(24, 62);
            this.lblDevClient.Name = "lblDevClient";
            this.lblDevClient.Size = new System.Drawing.Size(180, 20);
            this.lblDevClient.TabIndex = 17;
            this.lblDevClient.Text = "已连接的Client数量：0台";
            // 
            // lblDevTerminal
            // 
            this.lblDevTerminal.AutoSize = true;
            this.lblDevTerminal.ForeColor = System.Drawing.Color.White;
            this.lblDevTerminal.Location = new System.Drawing.Point(24, 94);
            this.lblDevTerminal.Name = "lblDevTerminal";
            this.lblDevTerminal.Size = new System.Drawing.Size(201, 20);
            this.lblDevTerminal.TabIndex = 16;
            this.lblDevTerminal.Text = "已连接的Terminal数量：0台";
            // 
            // btnDevStart
            // 
            this.btnDevStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDevStart.Location = new System.Drawing.Point(340, 55);
            this.btnDevStart.Name = "btnDevStart";
            this.btnDevStart.Size = new System.Drawing.Size(85, 35);
            this.btnDevStart.TabIndex = 12;
            this.btnDevStart.Text = "开启服务";
            this.btnDevStart.UseVisualStyleBackColor = true;
            this.btnDevStart.Click += new System.EventHandler(this.btnDevStart_Click);
            // 
            // grpSys
            // 
            this.grpSys.Controls.Add(this.lblSysAvgPerformance);
            this.grpSys.Controls.Add(this.lblSysClient);
            this.grpSys.Controls.Add(this.lblSysTerminal);
            this.grpSys.Controls.Add(this.btnSysStart);
            this.grpSys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpSys.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpSys.ForeColor = System.Drawing.Color.White;
            this.grpSys.Location = new System.Drawing.Point(77, 356);
            this.grpSys.Name = "grpSys";
            this.grpSys.Size = new System.Drawing.Size(468, 136);
            this.grpSys.TabIndex = 25;
            this.grpSys.TabStop = false;
            this.grpSys.Text = "系统（服务未开启）";
            // 
            // lblSysAvgPerformance
            // 
            this.lblSysAvgPerformance.AutoSize = true;
            this.lblSysAvgPerformance.ForeColor = System.Drawing.Color.White;
            this.lblSysAvgPerformance.Location = new System.Drawing.Point(24, 30);
            this.lblSysAvgPerformance.Name = "lblSysAvgPerformance";
            this.lblSysAvgPerformance.Size = new System.Drawing.Size(277, 20);
            this.lblSysAvgPerformance.TabIndex = 18;
            this.lblSysAvgPerformance.Text = "CPU平均负载：0%  RAM平均负载：0%";
            // 
            // lblSysClient
            // 
            this.lblSysClient.AutoSize = true;
            this.lblSysClient.ForeColor = System.Drawing.Color.White;
            this.lblSysClient.Location = new System.Drawing.Point(24, 62);
            this.lblSysClient.Name = "lblSysClient";
            this.lblSysClient.Size = new System.Drawing.Size(180, 20);
            this.lblSysClient.TabIndex = 17;
            this.lblSysClient.Text = "已连接的Client数量：0台";
            // 
            // lblSysTerminal
            // 
            this.lblSysTerminal.AutoSize = true;
            this.lblSysTerminal.ForeColor = System.Drawing.Color.White;
            this.lblSysTerminal.Location = new System.Drawing.Point(24, 94);
            this.lblSysTerminal.Name = "lblSysTerminal";
            this.lblSysTerminal.Size = new System.Drawing.Size(201, 20);
            this.lblSysTerminal.TabIndex = 16;
            this.lblSysTerminal.Text = "已连接的Terminal数量：0台";
            // 
            // btnSysStart
            // 
            this.btnSysStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSysStart.Location = new System.Drawing.Point(340, 55);
            this.btnSysStart.Name = "btnSysStart";
            this.btnSysStart.Size = new System.Drawing.Size(85, 35);
            this.btnSysStart.TabIndex = 12;
            this.btnSysStart.Text = "开启服务";
            this.btnSysStart.UseVisualStyleBackColor = true;
            this.btnSysStart.Click += new System.EventHandler(this.btnSysStart_Click);
            // 
            // grpRet
            // 
            this.grpRet.Controls.Add(this.lblRetAvgPerformance);
            this.grpRet.Controls.Add(this.lblRetClient);
            this.grpRet.Controls.Add(this.lblRetTerminal);
            this.grpRet.Controls.Add(this.btnRetStart);
            this.grpRet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpRet.Font = new System.Drawing.Font("微软雅黑", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.grpRet.ForeColor = System.Drawing.Color.White;
            this.grpRet.Location = new System.Drawing.Point(626, 356);
            this.grpRet.Name = "grpRet";
            this.grpRet.Size = new System.Drawing.Size(468, 136);
            this.grpRet.TabIndex = 26;
            this.grpRet.TabStop = false;
            this.grpRet.Text = "其它（服务未开启）";
            // 
            // lblRetAvgPerformance
            // 
            this.lblRetAvgPerformance.AutoSize = true;
            this.lblRetAvgPerformance.ForeColor = System.Drawing.Color.White;
            this.lblRetAvgPerformance.Location = new System.Drawing.Point(24, 30);
            this.lblRetAvgPerformance.Name = "lblRetAvgPerformance";
            this.lblRetAvgPerformance.Size = new System.Drawing.Size(277, 20);
            this.lblRetAvgPerformance.TabIndex = 18;
            this.lblRetAvgPerformance.Text = "CPU平均负载：0%  RAM平均负载：0%";
            // 
            // lblRetClient
            // 
            this.lblRetClient.AutoSize = true;
            this.lblRetClient.ForeColor = System.Drawing.Color.White;
            this.lblRetClient.Location = new System.Drawing.Point(24, 62);
            this.lblRetClient.Name = "lblRetClient";
            this.lblRetClient.Size = new System.Drawing.Size(180, 20);
            this.lblRetClient.TabIndex = 17;
            this.lblRetClient.Text = "已连接的Client数量：0台";
            // 
            // lblRetTerminal
            // 
            this.lblRetTerminal.AutoSize = true;
            this.lblRetTerminal.ForeColor = System.Drawing.Color.White;
            this.lblRetTerminal.Location = new System.Drawing.Point(24, 94);
            this.lblRetTerminal.Name = "lblRetTerminal";
            this.lblRetTerminal.Size = new System.Drawing.Size(201, 20);
            this.lblRetTerminal.TabIndex = 16;
            this.lblRetTerminal.Text = "已连接的Terminal数量：0台";
            // 
            // btnRetStart
            // 
            this.btnRetStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetStart.Location = new System.Drawing.Point(340, 55);
            this.btnRetStart.Name = "btnRetStart";
            this.btnRetStart.Size = new System.Drawing.Size(85, 35);
            this.btnRetStart.TabIndex = 12;
            this.btnRetStart.Text = "开启服务";
            this.btnRetStart.UseVisualStyleBackColor = true;
            this.btnRetStart.Click += new System.EventHandler(this.btnRetStart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(463, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 21);
            this.label3.TabIndex = 27;
            this.label3.Text = "Preview 2020";
            // 
            // rtxtLog
            // 
            this.rtxtLog.BackColor = System.Drawing.Color.Black;
            this.rtxtLog.ForeColor = System.Drawing.Color.Lime;
            this.rtxtLog.Location = new System.Drawing.Point(12, 528);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.Size = new System.Drawing.Size(1158, 227);
            this.rtxtLog.TabIndex = 28;
            this.rtxtLog.Text = "";
            // 
            // frmMainControlCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1182, 767);
            this.Controls.Add(this.rtxtLog);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.grpRet);
            this.Controls.Add(this.grpSys);
            this.Controls.Add(this.grpDev);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpOpe);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMainControlCenter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "冰鱼终端主控中心面板";
            this.Load += new System.EventHandler(this.frmMainControlCenter_Load);
            this.grpOpe.ResumeLayout(false);
            this.grpOpe.PerformLayout();
            this.grpDev.ResumeLayout(false);
            this.grpDev.PerformLayout();
            this.grpSys.ResumeLayout(false);
            this.grpSys.PerformLayout();
            this.grpRet.ResumeLayout(false);
            this.grpRet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnOpeStart;
        private System.Windows.Forms.Label lblOpeTerminal;
        private System.Windows.Forms.Label lblOpeClient;
        private System.Windows.Forms.Timer tmrMainControl;
        private System.Windows.Forms.GroupBox grpOpe;
        private System.Windows.Forms.Label lblOpeAvgPerformance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.GroupBox grpDev;
        private System.Windows.Forms.Label lblDevAvgPerformance;
        private System.Windows.Forms.Label lblDevClient;
        private System.Windows.Forms.Label lblDevTerminal;
        private System.Windows.Forms.Button btnDevStart;
        private System.Windows.Forms.GroupBox grpSys;
        private System.Windows.Forms.Label lblSysAvgPerformance;
        private System.Windows.Forms.Label lblSysClient;
        private System.Windows.Forms.Label lblSysTerminal;
        private System.Windows.Forms.Button btnSysStart;
        private System.Windows.Forms.GroupBox grpRet;
        private System.Windows.Forms.Label lblRetAvgPerformance;
        private System.Windows.Forms.Label lblRetClient;
        private System.Windows.Forms.Label lblRetTerminal;
        private System.Windows.Forms.Button btnRetStart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtxtLog;
    }
}