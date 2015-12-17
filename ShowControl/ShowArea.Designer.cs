namespace ShowControl
{
    partial class ShowArea
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ztbcZoom = new DevExpress.XtraEditors.ZoomTrackBarControl();
            this.lbZoom = new DevExpress.XtraEditors.LabelControl();
            this.btnRestoreZoom = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ztbcZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ztbcZoom.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // ztbcZoom
            // 
            this.ztbcZoom.EditValue = 100;
            this.ztbcZoom.Location = new System.Drawing.Point(3, 17);
            this.ztbcZoom.Name = "ztbcZoom";
            this.ztbcZoom.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.ztbcZoom.Properties.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.ztbcZoom.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.ztbcZoom.Properties.Appearance.Options.UseBackColor = true;
            this.ztbcZoom.Properties.Appearance.Options.UseBorderColor = true;
            this.ztbcZoom.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Transparent;
            this.ztbcZoom.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.ztbcZoom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.ztbcZoom.Properties.LargeChange = 10;
            this.ztbcZoom.Properties.Maximum = 500;
            this.ztbcZoom.Properties.Middle = 5;
            this.ztbcZoom.Properties.Minimum = 10;
            this.ztbcZoom.Properties.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.ztbcZoom.Properties.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.Bar;
            this.ztbcZoom.Properties.ShowValueToolTip = true;
            this.ztbcZoom.Properties.SmallChange = 10;
            this.ztbcZoom.Size = new System.Drawing.Size(23, 90);
            this.ztbcZoom.TabIndex = 1;
            this.ztbcZoom.Value = 100;
            this.ztbcZoom.ValueChanged += new System.EventHandler(this.ztbcZoom_ValueChanged);
            // 
            // lbZoom
            // 
            this.lbZoom.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lbZoom.Location = new System.Drawing.Point(3, 3);
            this.lbZoom.Name = "lbZoom";
            this.lbZoom.Size = new System.Drawing.Size(29, 13);
            this.lbZoom.TabIndex = 2;
            this.lbZoom.Text = "100%";
            // 
            // btnRestoreZoom
            // 
            this.btnRestoreZoom.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRestoreZoom.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.btnRestoreZoom.Image = global::ShowControl.Properties.Resources.zoom100_16x16;
            this.btnRestoreZoom.ImageLocation = DevExpress.XtraEditors.ImageLocation.BottomRight;
            this.btnRestoreZoom.Location = new System.Drawing.Point(3, 113);
            this.btnRestoreZoom.Name = "btnRestoreZoom";
            this.btnRestoreZoom.Size = new System.Drawing.Size(22, 18);
            this.btnRestoreZoom.TabIndex = 3;
            this.btnRestoreZoom.Click += new System.EventHandler(this.btnRestoreZoom_Click);
            // 
            // ShowArea
            // 
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.btnRestoreZoom);
            this.Controls.Add(this.lbZoom);
            this.Controls.Add(this.ztbcZoom);
            this.DoubleBuffered = true;
            this.Location = new System.Drawing.Point(100, 0);
            this.Name = "ShowArea";
            this.Size = new System.Drawing.Size(514, 341);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ShowArea_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ShowArea_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowArea_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ShowArea_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ShowArea_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.ztbcZoom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ztbcZoom)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private DevExpress.XtraEditors.ZoomTrackBarControl ztbcZoom;
        private DevExpress.XtraEditors.LabelControl lbZoom;
        private DevExpress.XtraEditors.SimpleButton btnRestoreZoom;

    }
}
