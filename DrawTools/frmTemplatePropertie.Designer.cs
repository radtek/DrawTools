namespace DrawTools
{
    partial class frmTemplatePropertie
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTemplatePropertie));
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnCheck = new DevExpress.XtraEditors.SimpleButton();
            this.plTitle = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtTemplateText = new DevExpress.XtraEditors.TextEdit();
            this.cmbTemplateMenu = new DevExpress.XtraEditors.LookUpEdit();
            this.txtTemplateMenu = new DevExpress.XtraEditors.TextEdit();
            this.btnAdd = new DevExpress.XtraEditors.SimpleButton();
            this.btnBack = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.plTitle)).BeginInit();
            this.plTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateText.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplateMenu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateMenu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(146, 169);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(48, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "取消";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(66, 169);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(48, 23);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "确定";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
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
            this.plTitle.Size = new System.Drawing.Size(258, 27);
            this.plTitle.TabIndex = 2;
            this.plTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.plTitle_MouseDown);
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Location = new System.Drawing.Point(0, 0);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(86, 27);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "  模板属性设置";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(40, 72);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 8;
            this.labelControl2.Text = "类别";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(40, 111);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 9;
            this.labelControl3.Text = "名称";
            // 
            // txtTemplateText
            // 
            this.txtTemplateText.Location = new System.Drawing.Point(76, 108);
            this.txtTemplateText.Name = "txtTemplateText";
            this.txtTemplateText.Size = new System.Drawing.Size(131, 20);
            this.txtTemplateText.TabIndex = 1;
            // 
            // cmbTemplateMenu
            // 
            this.cmbTemplateMenu.Location = new System.Drawing.Point(76, 69);
            this.cmbTemplateMenu.Name = "cmbTemplateMenu";
            this.cmbTemplateMenu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTemplateMenu.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Value", "类别")});
            this.cmbTemplateMenu.Properties.DisplayMember = "Value";
            this.cmbTemplateMenu.Properties.NullText = "";
            this.cmbTemplateMenu.Properties.PopupSizeable = false;
            this.cmbTemplateMenu.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.cmbTemplateMenu.Properties.ValueMember = "Key";
            this.cmbTemplateMenu.Size = new System.Drawing.Size(131, 20);
            this.cmbTemplateMenu.TabIndex = 0;
            // 
            // txtTemplateMenu
            // 
            this.txtTemplateMenu.Location = new System.Drawing.Point(76, 69);
            this.txtTemplateMenu.Name = "txtTemplateMenu";
            this.txtTemplateMenu.Size = new System.Drawing.Size(131, 20);
            this.txtTemplateMenu.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(211, 68);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23, 23);
            this.btnAdd.TabIndex = 14;
            this.btnAdd.Text = "simpleButton1";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnBack
            // 
            this.btnBack.Image = ((System.Drawing.Image)(resources.GetObject("btnBack.Image")));
            this.btnBack.Location = new System.Drawing.Point(211, 68);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(23, 23);
            this.btnBack.TabIndex = 15;
            this.btnBack.Text = "simpleButton2";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // frmTemplatePropertie
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(258, 229);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtTemplateMenu);
            this.Controls.Add(this.txtTemplateText);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.btnCheck);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.plTitle);
            this.Controls.Add(this.cmbTemplateMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTemplatePropertie";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmTemplatePropertie_Load);
            ((System.ComponentModel.ISupportInitialize)(this.plTitle)).EndInit();
            this.plTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateText.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTemplateMenu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTemplateMenu.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnCheck;
        private DevExpress.XtraEditors.PanelControl plTitle;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtTemplateText;
        private DevExpress.XtraEditors.LookUpEdit cmbTemplateMenu;
        private DevExpress.XtraEditors.TextEdit txtTemplateMenu;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnBack;
    }
}