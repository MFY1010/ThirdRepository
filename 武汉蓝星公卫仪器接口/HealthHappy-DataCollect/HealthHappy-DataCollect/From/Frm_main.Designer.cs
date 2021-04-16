namespace HealthHappy_DataCollect.From
{
    partial class Frm_main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_main));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerSocket = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.labelConnectType = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Tb_Info = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "仪器数据采集";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // timerSocket
            // 
            this.timerSocket.Interval = 3000;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 18;
            this.label2.Text = "通讯类型:";
            // 
            // labelConnectType
            // 
            this.labelConnectType.AutoSize = true;
            this.labelConnectType.ForeColor = System.Drawing.Color.Coral;
            this.labelConnectType.Location = new System.Drawing.Point(12, 30);
            this.labelConnectType.Name = "labelConnectType";
            this.labelConnectType.Size = new System.Drawing.Size(29, 12);
            this.labelConnectType.TabIndex = 19;
            this.labelConnectType.Text = "none";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 21;
            this.label1.Text = "状态:";
            // 
            // Tb_Info
            // 
            this.Tb_Info.Location = new System.Drawing.Point(181, 30);
            this.Tb_Info.Multiline = true;
            this.Tb_Info.Name = "Tb_Info";
            this.Tb_Info.Size = new System.Drawing.Size(215, 204);
            this.Tb_Info.TabIndex = 20;
            // 
            // Frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 240);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tb_Info);
            this.Controls.Add(this.labelConnectType);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_main";
            this.Text = "数据采集程序";
            this.Deactivate += new System.EventHandler(this.Frm_main_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_main_FormClosing);
            this.Load += new System.EventHandler(this.Frm_main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer timerSocket;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelConnectType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Tb_Info;
    }
}