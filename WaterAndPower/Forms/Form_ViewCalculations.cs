using Pharmonics19.DbFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterAndPower.Forms
{
    public partial class Form_ViewCalculations : Form
    {
        public string WorkAssignId { get; set; }
        public string WorkTitle { get; set; }
        DataAccess ds;
        public Form_ViewCalculations()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_ViewCalculations_Load(object sender, EventArgs e)
        {
            lblTitle.Text = WorkTitle;
            ds.fillgridView("select Date,WorkDone,NowToBePaid,WorkToBeDone,AmountPaid,BalanceAmount from tblCalculationsData where WorkAssignId = '"+WorkAssignId+"'",dataGridView1);
        }
    }
}
