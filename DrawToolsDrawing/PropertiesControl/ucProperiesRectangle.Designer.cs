namespace DrawToolsDrawing.PropertiesControl
{
    partial class ucProperiesRectangle
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
            this.spinNewHeight = new DevExpress.XtraEditors.SpinEdit();
            this.spinOldHeight = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.cmbOldLineWdith = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbNewLineWdith = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cpOldLineColor = new DevExpress.XtraEditors.ColorPickEdit();
            this.cpNewLineColor = new DevExpress.XtraEditors.ColorPickEdit();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.spinOldWidth = new DevExpress.XtraEditors.SpinEdit();
            this.spinNewWidth = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl15 = new DevExpress.XtraEditors.LabelControl();
            this.cpOldFillColor = new DevExpress.XtraEditors.ColorPickEdit();
            this.cpNewFillColor = new DevExpress.XtraEditors.ColorPickEdit();
            this.chkFilled = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNewHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOldHeight.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOldLineWdith.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNewLineWdith.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpOldLineColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpNewLineColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOldWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNewWidth.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpOldFillColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpNewFillColor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilled.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // spinNewHeight
            // 
            this.spinNewHeight.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNewHeight.Location = new System.Drawing.Point(136, 67);
            this.spinNewHeight.Name = "spinNewHeight";
            this.spinNewHeight.Properties.DisplayFormat.FormatString = "0";
            this.spinNewHeight.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinNewHeight.Properties.IsFloatValue = false;
            this.spinNewHeight.Properties.Mask.EditMask = "N00";
            this.spinNewHeight.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spinNewHeight.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNewHeight.Size = new System.Drawing.Size(49, 20);
            this.spinNewHeight.TabIndex = 26;
            // 
            // spinOldHeight
            // 
            this.spinOldHeight.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinOldHeight.Location = new System.Drawing.Point(82, 67);
            this.spinOldHeight.Name = "spinOldHeight";
            this.spinOldHeight.Properties.IsFloatValue = false;
            this.spinOldHeight.Properties.Mask.EditMask = "N00";
            this.spinOldHeight.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spinOldHeight.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinOldHeight.Properties.ReadOnly = true;
            this.spinOldHeight.Size = new System.Drawing.Size(36, 20);
            this.spinOldHeight.TabIndex = 27;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(58, 70);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(12, 14);
            this.labelControl7.TabIndex = 25;
            this.labelControl7.Text = "高";
            // 
            // cmbOldLineWdith
            // 
            this.cmbOldLineWdith.Location = new System.Drawing.Point(82, 92);
            this.cmbOldLineWdith.Name = "cmbOldLineWdith";
            this.cmbOldLineWdith.Properties.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbOldLineWdith.Properties.ReadOnly = true;
            this.cmbOldLineWdith.Size = new System.Drawing.Size(36, 20);
            this.cmbOldLineWdith.TabIndex = 23;
            // 
            // cmbNewLineWdith
            // 
            this.cmbNewLineWdith.Location = new System.Drawing.Point(136, 93);
            this.cmbNewLineWdith.Name = "cmbNewLineWdith";
            this.cmbNewLineWdith.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbNewLineWdith.Properties.Items.AddRange(new object[] {
            "-1",
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cmbNewLineWdith.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbNewLineWdith.Size = new System.Drawing.Size(49, 20);
            this.cmbNewLineWdith.TabIndex = 24;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(46, 95);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(24, 14);
            this.labelControl6.TabIndex = 21;
            this.labelControl6.Text = "线宽";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(152, 21);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(12, 14);
            this.labelControl9.TabIndex = 18;
            this.labelControl9.Text = "新";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(94, 21);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(12, 14);
            this.labelControl8.TabIndex = 15;
            this.labelControl8.Text = "原";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(46, 121);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(24, 14);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "线色";
            // 
            // cpOldLineColor
            // 
            this.cpOldLineColor.EditValue = System.Drawing.Color.Empty;
            this.cpOldLineColor.Location = new System.Drawing.Point(82, 119);
            this.cpOldLineColor.Name = "cpOldLineColor";
            this.cpOldLineColor.Properties.ReadOnly = true;
            this.cpOldLineColor.Size = new System.Drawing.Size(36, 20);
            this.cpOldLineColor.TabIndex = 14;
            // 
            // cpNewLineColor
            // 
            this.cpNewLineColor.EditValue = System.Drawing.Color.Empty;
            this.cpNewLineColor.Location = new System.Drawing.Point(136, 119);
            this.cpNewLineColor.Name = "cpNewLineColor";
            this.cpNewLineColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cpNewLineColor.Size = new System.Drawing.Size(49, 20);
            this.cpNewLineColor.TabIndex = 13;
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(58, 44);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(12, 14);
            this.labelControl12.TabIndex = 25;
            this.labelControl12.Text = "宽";
            // 
            // spinOldWidth
            // 
            this.spinOldWidth.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinOldWidth.Location = new System.Drawing.Point(82, 41);
            this.spinOldWidth.Name = "spinOldWidth";
            this.spinOldWidth.Properties.IsFloatValue = false;
            this.spinOldWidth.Properties.Mask.EditMask = "N00";
            this.spinOldWidth.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spinOldWidth.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinOldWidth.Properties.ReadOnly = true;
            this.spinOldWidth.Size = new System.Drawing.Size(36, 20);
            this.spinOldWidth.TabIndex = 27;
            // 
            // spinNewWidth
            // 
            this.spinNewWidth.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNewWidth.Location = new System.Drawing.Point(136, 41);
            this.spinNewWidth.Name = "spinNewWidth";
            this.spinNewWidth.Properties.DisplayFormat.FormatString = "0";
            this.spinNewWidth.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinNewWidth.Properties.IsFloatValue = false;
            this.spinNewWidth.Properties.Mask.EditMask = "N00";
            this.spinNewWidth.Properties.MaxValue = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.spinNewWidth.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinNewWidth.Size = new System.Drawing.Size(49, 20);
            this.spinNewWidth.TabIndex = 26;
            // 
            // labelControl15
            // 
            this.labelControl15.Location = new System.Drawing.Point(34, 147);
            this.labelControl15.Name = "labelControl15";
            this.labelControl15.Size = new System.Drawing.Size(36, 14);
            this.labelControl15.TabIndex = 31;
            this.labelControl15.Text = "背景色";
            // 
            // cpOldFillColor
            // 
            this.cpOldFillColor.EditValue = System.Drawing.Color.Empty;
            this.cpOldFillColor.Location = new System.Drawing.Point(82, 145);
            this.cpOldFillColor.Name = "cpOldFillColor";
            this.cpOldFillColor.Properties.ReadOnly = true;
            this.cpOldFillColor.Size = new System.Drawing.Size(36, 20);
            this.cpOldFillColor.TabIndex = 29;
            // 
            // cpNewFillColor
            // 
            this.cpNewFillColor.EditValue = System.Drawing.Color.Empty;
            this.cpNewFillColor.Location = new System.Drawing.Point(136, 145);
            this.cpNewFillColor.Name = "cpNewFillColor";
            this.cpNewFillColor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cpNewFillColor.Size = new System.Drawing.Size(49, 20);
            this.cpNewFillColor.TabIndex = 28;
            // 
            // chkFilled
            // 
            this.chkFilled.Location = new System.Drawing.Point(24, 171);
            this.chkFilled.Name = "chkFilled";
            this.chkFilled.Properties.Caption = "是否填充";
            this.chkFilled.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.chkFilled.Size = new System.Drawing.Size(75, 19);
            this.chkFilled.TabIndex = 32;
            // 
            // ucProperiesRectangle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkFilled);
            this.Controls.Add(this.labelControl15);
            this.Controls.Add(this.cpOldFillColor);
            this.Controls.Add(this.cpNewFillColor);
            this.Controls.Add(this.spinNewWidth);
            this.Controls.Add(this.spinNewHeight);
            this.Controls.Add(this.spinOldWidth);
            this.Controls.Add(this.spinOldHeight);
            this.Controls.Add(this.labelControl12);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.cmbOldLineWdith);
            this.Controls.Add(this.cmbNewLineWdith);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl9);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cpOldLineColor);
            this.Controls.Add(this.cpNewLineColor);
            this.Name = "ucProperiesRectangle";
            this.Size = new System.Drawing.Size(233, 213);
            ((System.ComponentModel.ISupportInitialize)(this.spinNewHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOldHeight.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOldLineWdith.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbNewLineWdith.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpOldLineColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpNewLineColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinOldWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinNewWidth.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpOldFillColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cpNewFillColor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkFilled.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SpinEdit spinNewHeight;
        private DevExpress.XtraEditors.SpinEdit spinOldHeight;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOldLineWdith;
        private DevExpress.XtraEditors.ComboBoxEdit cmbNewLineWdith;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ColorPickEdit cpOldLineColor;
        private DevExpress.XtraEditors.ColorPickEdit cpNewLineColor;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.SpinEdit spinOldWidth;
        private DevExpress.XtraEditors.SpinEdit spinNewWidth;
        private DevExpress.XtraEditors.LabelControl labelControl15;
        private DevExpress.XtraEditors.ColorPickEdit cpOldFillColor;
        private DevExpress.XtraEditors.ColorPickEdit cpNewFillColor;
        private DevExpress.XtraEditors.CheckEdit chkFilled;
    }
}
