namespace DrawTools
{
    partial class DrawArea
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
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawArea));
            //this.SuspendLayout();
            // 
            // DrawArea
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.DoubleBuffered = true;
            this.Location = new System.Drawing.Point(100, 0);
            this.Name = "DrawArea";
            this.Size = new System.Drawing.Size(514, 341);
            this.Load += new System.EventHandler(this.DrawArea_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawArea_Paint);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseDown);
            this.MouseLeave += new System.EventHandler(this.DrawArea_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawArea_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnSmall;
        public System.Windows.Forms.Button btnLarge;
	}
}
