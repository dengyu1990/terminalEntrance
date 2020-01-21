namespace terminalEntrance
{
    partial class frmRDP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRDP));
            this.pnlMonitor = new System.Windows.Forms.Panel();
            this.btnLock = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.pnlLock = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.txtUnlock = new System.Windows.Forms.TextBox();
            this.tmrRefresh = new System.Windows.Forms.Timer(this.components);
            this.pnlMonitor.SuspendLayout();
            this.pnlLock.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMonitor
            // 
            this.pnlMonitor.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlMonitor.Controls.Add(this.btnLock);
            this.pnlMonitor.Controls.Add(this.btnBack);
            this.pnlMonitor.Controls.Add(this.lblDisplay);
            this.pnlMonitor.Controls.Add(this.btnSwitch);
            this.pnlMonitor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlMonitor.Location = new System.Drawing.Point(159, 49);
            this.pnlMonitor.Name = "pnlMonitor";
            this.pnlMonitor.Size = new System.Drawing.Size(500, 33);
            this.pnlMonitor.TabIndex = 1;
            // 
            // btnLock
            // 
            this.btnLock.BackColor = System.Drawing.Color.Transparent;
            this.btnLock.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnLock.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SteelBlue;
            this.btnLock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLock.ForeColor = System.Drawing.Color.SkyBlue;
            this.btnLock.Location = new System.Drawing.Point(731, 5);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(50, 25);
            this.btnLock.TabIndex = 3;
            this.btnLock.Text = "锁定";
            this.btnLock.UseVisualStyleBackColor = false;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SteelBlue;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.ForeColor = System.Drawing.Color.SkyBlue;
            this.btnBack.Location = new System.Drawing.Point(675, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(50, 25);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "返回";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.BackColor = System.Drawing.Color.Transparent;
            this.lblDisplay.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDisplay.ForeColor = System.Drawing.Color.SkyBlue;
            this.lblDisplay.Location = new System.Drawing.Point(19, 3);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(249, 21);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "[ Performance data initializing ]";
            // 
            // btnSwitch
            // 
            this.btnSwitch.BackColor = System.Drawing.Color.Transparent;
            this.btnSwitch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSwitch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.SteelBlue;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.ForeColor = System.Drawing.Color.SkyBlue;
            this.btnSwitch.Location = new System.Drawing.Point(599, 3);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(70, 25);
            this.btnSwitch.TabIndex = 1;
            this.btnSwitch.Text = "一键切换";
            this.btnSwitch.UseVisualStyleBackColor = false;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // pnlLock
            // 
            this.pnlLock.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlLock.BackgroundImage = global::terminalEntrance.Properties.Resources.fish;
            this.pnlLock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlLock.Controls.Add(this.lblName);
            this.pnlLock.Controls.Add(this.lblTime);
            this.pnlLock.Controls.Add(this.txtUnlock);
            this.pnlLock.Location = new System.Drawing.Point(71, 98);
            this.pnlLock.Name = "pnlLock";
            this.pnlLock.Size = new System.Drawing.Size(973, 512);
            this.pnlLock.TabIndex = 2;
            this.pnlLock.Visible = false;
            this.pnlLock.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlLock_Paint);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.ForeColor = System.Drawing.Color.SkyBlue;
            this.lblName.Location = new System.Drawing.Point(733, 438);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(90, 21);
            this.lblName.TabIndex = 8;
            this.lblName.Text = "未注册用户";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("微软雅黑", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.Transparent;
            this.lblTime.Location = new System.Drawing.Point(660, 126);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(283, 86);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "锁定中...";
            // 
            // txtUnlock
            // 
            this.txtUnlock.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUnlock.ForeColor = System.Drawing.Color.SteelBlue;
            this.txtUnlock.Location = new System.Drawing.Point(736, 477);
            this.txtUnlock.MaxLength = 18;
            this.txtUnlock.Name = "txtUnlock";
            this.txtUnlock.Size = new System.Drawing.Size(155, 29);
            this.txtUnlock.TabIndex = 3;
            this.txtUnlock.Text = "输入域帐号密码解锁";
            this.txtUnlock.TextChanged += new System.EventHandler(this.txtUnlock_TextChanged);
            this.txtUnlock.Enter += new System.EventHandler(this.txtUnlock_Enter);
            // 
            // tmrRefresh
            // 
            this.tmrRefresh.Interval = 1000;
            this.tmrRefresh.Tick += new System.EventHandler(this.tmrRefresh_Tick);
            // 
            // frmRDP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1366, 622);
            this.Controls.Add(this.pnlLock);
            this.Controls.Add(this.pnlMonitor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRDP";
            this.ShowInTaskbar = false;
            this.Text = "frmRDP";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmRDP_Load);
            this.MouseEnter += new System.EventHandler(this.frmRDP_MouseEnter);
            this.pnlMonitor.ResumeLayout(false);
            this.pnlMonitor.PerformLayout();
            this.pnlLock.ResumeLayout(false);
            this.pnlLock.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlMonitor;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.Panel pnlLock;
        private System.Windows.Forms.TextBox txtUnlock;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Timer tmrRefresh;
    }
}