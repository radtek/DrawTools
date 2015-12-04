namespace DrawToolsDrawing.PropertiesControl
{
    partial class MainPropertie
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
            this.plCommon = new DevExpress.XtraEditors.PanelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnCheck = new DevExpress.XtraEditors.SimpleButton();
            this.plProperties = new DevExpress.XtraEditors.PanelControl();
            this.plTitle = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.plCommon)).BeginInit();
            this.plCommon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plTitle)).BeginInit();
            this.plTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // plCommon
            // 
            this.plCommon.Controls.Add(this.btnClose);
            this.plCommon.Controls.Add(this.btnCheck);
            this.plCommon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.plCommon.Location = new System.Drawing.Point(0, 263);
            this.plCommon.Name = "plCommon";
            this.plCommon.Size = new System.Drawing.Size(292, 62);
            this.plCommon.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(167, 17);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(48, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCheck.Location = new System.Drawing.Point(66, 17);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(48, 23);
            this.btnCheck.TabIndex = 0;
            this.btnCheck.Text = "确定";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // plProperties
            // 
            this.plProperties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plProperties.Location = new System.Drawing.Point(0, 27);
            this.plProperties.Name = "plProperties";
            this.plProperties.Size = new System.Drawing.Size(292, 236);
            this.plProperties.TabIndex = 1;
            // 
            // plTitle
            // 
            this.plTitle.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.plTitle.Appearance.Options.UseBackColor = true;
            this.plTitle.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.plTitle.Controls.Add(this.labelControl1);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle.Location = new System.Drawing.Point(0, 0);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(292, 27);
            this.plTitle.TabIndex = 2;
            this.plTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.plTitle_MouseDown);
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(66, 27);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "  属性设置";
            // 
            // MainPropertie
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(292, 325);
            this.Controls.Add(this.plProperties);
            this.Controls.Add(this.plCommon);
            this.Controls.Add(this.plTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainPropertie";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)(this.plCommon)).EndInit();
            this.plCommon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plTitle)).EndInit();
            this.plTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl plCommon;
        private DevExpress.XtraEditors.PanelControl plProperties;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnCheck;
        private DevExpress.XtraEditors.PanelControl plTitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}