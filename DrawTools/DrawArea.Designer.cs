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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawArea));
            this.btnSmall = new System.Windows.Forms.Button();
            this.btnLarge = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.�ձ���ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ʵʱ����ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.������ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.������ѯToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSmall
            // 
            this.btnSmall.BackColor = System.Drawing.Color.Transparent;
            this.btnSmall.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSmall.BackgroundImage")));
            this.btnSmall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSmall.FlatAppearance.BorderSize = 0;
            this.btnSmall.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSmall.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.btnSmall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSmall.Location = new System.Drawing.Point(38, 4);
            this.btnSmall.Name = "btnSmall";
            this.btnSmall.Size = new System.Drawing.Size(29, 30);
            this.btnSmall.TabIndex = 7;
            this.btnSmall.UseVisualStyleBackColor = false;
            this.btnSmall.Click += new System.EventHandler(this.btnSmall_Click);
            // 
            // btnLarge
            // 
            this.btnLarge.BackColor = System.Drawing.Color.Transparent;
            this.btnLarge.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLarge.BackgroundImage")));
            this.btnLarge.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLarge.FlatAppearance.BorderSize = 0;
            this.btnLarge.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLarge.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.btnLarge.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLarge.Location = new System.Drawing.Point(3, 3);
            this.btnLarge.Name = "btnLarge";
            this.btnLarge.Size = new System.Drawing.Size(29, 30);
            this.btnLarge.TabIndex = 6;
            this.btnLarge.UseVisualStyleBackColor = false;
            this.btnLarge.Click += new System.EventHandler(this.btnLarge_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.�ձ���ToolStripMenuItem,
            this.ʵʱ����ToolStripMenuItem,
            this.������ToolStripMenuItem,
            this.������ToolStripMenuItem,
            this.������ToolStripMenuItem,
            this.������ѯToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 136);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // �ձ���ToolStripMenuItem
            // 
            this.�ձ���ToolStripMenuItem.Name = "�ձ���ToolStripMenuItem";
            this.�ձ���ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.�ձ���ToolStripMenuItem.Text = "�ձ���";
            // 
            // ʵʱ����ToolStripMenuItem
            // 
            this.ʵʱ����ToolStripMenuItem.Name = "ʵʱ����ToolStripMenuItem";
            this.ʵʱ����ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.ʵʱ����ToolStripMenuItem.Text = "ʵʱ����";
            // 
            // ������ToolStripMenuItem
            // 
            this.������ToolStripMenuItem.Name = "������ToolStripMenuItem";
            this.������ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.������ToolStripMenuItem.Text = "������";
            // 
            // ������ToolStripMenuItem
            // 
            this.������ToolStripMenuItem.Name = "������ToolStripMenuItem";
            this.������ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.������ToolStripMenuItem.Text = "������";
            // 
            // ������ToolStripMenuItem
            // 
            this.������ToolStripMenuItem.Name = "������ToolStripMenuItem";
            this.������ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.������ToolStripMenuItem.Text = "������";
            // 
            // ������ѯToolStripMenuItem
            // 
            this.������ѯToolStripMenuItem.Name = "������ѯToolStripMenuItem";
            this.������ѯToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.������ѯToolStripMenuItem.Text = "������ѯ";
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
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnSmall;
        public System.Windows.Forms.Button btnLarge;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.ToolStripMenuItem �ձ���ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ʵʱ����ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ������ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ������ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem ������ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ������ѯToolStripMenuItem;
	}
}
