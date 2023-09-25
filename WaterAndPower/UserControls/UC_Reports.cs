using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WaterAndPower.Forms;

namespace WaterAndPower.UserControls
{
    public partial class UC_Reports : UserControl
    {
        public UC_Reports()
        {
            InitializeComponent();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            using (Form_SelectYear sw = new Form_SelectYear())
            {
                sw.ShowDialog();
            }
        }
    }
}
