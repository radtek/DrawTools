using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowControl
{
    public partial class FromShow : Form
    {
        public FromShow()
        {
            InitializeComponent();
        }

        private void FromShow_Load(object sender, EventArgs e)
        {
            ShowArea showArea = new ShowArea();
            showArea.Dock = DockStyle.Fill;
            showArea.Owner = this;
            this.Controls.Add(showArea);
            showArea.SetDataSource(@"C:\Users\Icelove\Desktop\Untitled1.dtl");
        }
    }
}
