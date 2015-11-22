namespace DrawToolsDrawing.PropertiesControl
{
    partial class ucProperiesText
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProperiesText));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtNote = new DevExpress.XtraEditors.MemoEdit();
            this.cmbFont = new DevExpress.XtraEditors.FontEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtStyle = new DevExpress.XtraEditors.TextEdit();
            this.txtSize = new DevExpress.XtraEditors.TextEdit();
            this.btnSetFont = new DevExpress.XtraEditors.SimpleButton();
            this.chkUnderline = new DevExpress.XtraEditors.CheckEdit();
            this.chkDeleteLine = new DevExpress.XtraEditors.CheckEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.cpFontColor = new DevExpress.XtraEditors.ColorPickEdit();
            this.plFontInfo = new DevExpress.XtraEditors.PanelControl();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.cpFillBackGroundColor = new DevExpress.XtraEditors.ColorPickEdit();
            this.chkFilledColor = new DevExpress.XtraEditors.CheckEdit();
            this.chkVerticalText = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFont.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyle.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUnderline.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDeleteLine.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpFontColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.plFontInfo)).BeginInit();
            this.plFontInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpFillBackGroundColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilledColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVerticalText.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(27, 25);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "文本";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(15, 45);
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(291, 69);
            this.txtNote.TabIndex = 1;
            this.txtNote.UseOptimizedRendering = true;
            // 
            // cmbFont
            // 
            this.cmbFont.Location = new System.Drawing.Point(42, 12);
            this.cmbFont.Name = "cmbFont";
            this.cmbFont.Properties.ReadOnly = true;
            this.cmbFont.Size = new System.Drawing.Size(95, 20);
            this.cmbFont.TabIndex = 2;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(24, 14);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "字体";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(178, 18);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(24, 14);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "大小";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 57);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(24, 14);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "字形";
            // 
            // txtStyle
            // 
            this.txtStyle.Location = new System.Drawing.Point(42, 54);
            this.txtStyle.Name = "txtStyle";
            this.txtStyle.Properties.ReadOnly = true;
            this.txtStyle.Size = new System.Drawing.Size(95, 20);
            this.txtStyle.TabIndex = 4;
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(215, 12);
            this.txtSize.Name = "txtSize";
            this.txtSize.Properties.ReadOnly = true;
            this.txtSize.Size = new System.Drawing.Size(57, 20);
            this.txtSize.TabIndex = 4;
            // 
            // btnSetFont
            // 
            this.btnSetFont.Image = ((System.Drawing.Image)(resources.GetObject("btnSetFont.Image")));
            this.btnSetFont.Location = new System.Drawing.Point(15, 125);
            this.btnSetFont.Name = "btnSetFont";
            this.btnSetFont.Size = new System.Drawing.Size(75, 23);
            this.btnSetFont.TabIndex = 5;
            this.btnSetFont.Text = "设置字体";
            this.btnSetFont.Click += new System.EventHandler(this.btnSetFont_Click);
            // 
            // chkUnderline
            // 
            this.chkUnderline.Location = new System.Drawing.Point(40, 104);
            this.chkUnderline.Name = "chkUnderline";
            this.chkUnderline.Properties.Caption = "下划线";
            this.chkUnderline.Properties.ReadOnly = true;
            this.chkUnderline.Size = new System.Drawing.Size(71, 19);
            this.chkUnderline.TabIndex = 6;
            // 
            // chkDeleteLine
            // 
            this.chkDeleteLine.Location = new System.Drawing.Point(152, 104);
            this.chkDeleteLine.Name = "chkDeleteLine";
            this.chkDeleteLine.Properties.Caption = "删除线";
            this.chkDeleteLine.Properties.ReadOnly = true;
            this.chkDeleteLine.Size = new System.Drawing.Size(71, 19);
            this.chkDeleteLine.TabIndex = 8;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(154, 57);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(48, 14);
            this.labelControl5.TabIndex = 9;
            this.labelControl5.Text = "字体颜色";
            // 
            // cpFontColor
            // 
            this.cpFontColor.EditValue = System.Drawing.Color.Empty;
            this.cpFontColor.Location = new System.Drawing.Point(215, 54);
            this.cpFontColor.Name = "cpFontColor";
            this.cpFontColor.Properties.ReadOnly = true;
            this.cpFontColor.Size = new System.Drawing.Size(34, 20);
            this.cpFontColor.TabIndex = 10;
            // 
            // plFontInfo
            // 
            this.plFontInfo.Controls.Add(this.txtStyle);
            this.plFontInfo.Controls.Add(this.cpFontColor);
            this.plFontInfo.Controls.Add(this.cmbFont);
            this.plFontInfo.Controls.Add(this.labelControl5);
            this.plFontInfo.Controls.Add(this.labelControl2);
            this.plFontInfo.Controls.Add(this.labelControl3);
            this.plFontInfo.Controls.Add(this.chkDeleteLine);
            this.plFontInfo.Controls.Add(this.labelControl4);
            this.plFontInfo.Controls.Add(this.chkUnderline);
            this.plFontInfo.Controls.Add(this.txtSize);
            this.plFontInfo.Location = new System.Drawing.Point(15, 195);
            this.plFontInfo.Name = "plFontInfo";
            this.plFontInfo.Size = new System.Drawing.Size(291, 148);
            this.plFontInfo.TabIndex = 11;
            // 
            // cpFillBackGroundColor
            // 
            this.cpFillBackGroundColor.EditValue = System.Drawing.Color.Empty;
            this.cpFillBackGroundColor.Location = new System.Drawing.Point(105, 161);
            this.cpFillBackGroundColor.Name = "cpFillBackGroundColor";
            this.cpFillBackGroundColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cpFillBackGroundColor.Size = new System.Drawing.Size(47, 20);
            this.cpFillBackGroundColor.TabIndex = 29;
            this.cpFillBackGroundColor.EditValueChanged += new System.EventHandler(this.cpFillBackGroundColor_EditValueChanged);
            // 
            // chkFilledColor
            // 
            this.chkFilledColor.Location = new System.Drawing.Point(13, 159);
            this.chkFilledColor.Name = "chkFilledColor";
            this.chkFilledColor.Properties.Caption = "填充背景色";
            this.chkFilledColor.Size = new System.Drawing.Size(86, 19);
            this.chkFilledColor.TabIndex = 30;
            this.chkFilledColor.CheckedChanged += new System.EventHandler(this.chkFilledColor_CheckedChanged);
            // 
            // chkVerticalText
            // 
            this.chkVerticalText.Location = new System.Drawing.Point(174, 159);
            this.chkVerticalText.Name = "chkVerticalText";
            this.chkVerticalText.Properties.Caption = "竖向文本";
            this.chkVerticalText.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkVerticalText.Size = new System.Drawing.Size(73, 19);
            this.chkVerticalText.TabIndex = 30;
            this.chkVerticalText.CheckedChanged += new System.EventHandler(this.chkFilledColor_CheckedChanged);
            // 
            // ucProperiesText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkVerticalText);
            this.Controls.Add(this.chkFilledColor);
            this.Controls.Add(this.cpFillBackGroundColor);
            this.Controls.Add(this.plFontInfo);
            this.Controls.Add(this.btnSetFont);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.labelControl1);
            this.Name = "ucProperiesText";
            this.Size = new System.Drawing.Size(326, 365);
            ((System.ComponentModel.ISupportInitialize)(this.txtNote.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFont.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStyle.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSize.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkUnderline.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkDeleteLine.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpFontColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.plFontInfo)).EndInit();
            this.plFontInfo.ResumeLayout(false);
            this.plFontInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cpFillBackGroundColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilledColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkVerticalText.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.MemoEdit txtNote;
        private DevExpress.XtraEditors.FontEdit cmbFont;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtStyle;
        private DevExpress.XtraEditors.TextEdit txtSize;
        private DevExpress.XtraEditors.SimpleButton btnSetFont;
        private DevExpress.XtraEditors.CheckEdit chkUnderline;
        private DevExpress.XtraEditors.CheckEdit chkDeleteLine;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ColorPickEdit cpFontColor;
        private DevExpress.XtraEditors.PanelControl plFontInfo;
        private System.Windows.Forms.FontDialog fontDialog;
        private DevExpress.XtraEditors.ColorPickEdit cpFillBackGroundColor;
        private DevExpress.XtraEditors.CheckEdit chkFilledColor;
        private DevExpress.XtraEditors.CheckEdit chkVerticalText;
    }
}
