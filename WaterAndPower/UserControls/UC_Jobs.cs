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
using Pharmonics19.DbFiles;

namespace WaterAndPower.UserControls
{
    public partial class UC_Jobs : UserControl
    {
        DataAccess ds;
        public UC_Jobs()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (workId != null)
            {
                using (Form_AssignWork aw = new Form_AssignWork())
                {
                    aw.workId = workId;
                    aw.TsAmt = TsAmt;
                    aw.workTitle = title;
                    aw.ShowDialog();
                    this.OnLoad(e);
                }
            }
            
        }

        private void BtnUsers_Click(object sender, EventArgs e)
        {
            using (Form_AddWork aw = new Form_AddWork())
            {
                aw.ShowDialog();
                this.OnLoad(e);
            }
        }

        private void UC_Jobs_Load(object sender, EventArgs e)
        {
            ds.fillgridView("select * from tblWorks where isAssigned = 0", dataGridView1);
        }

        string workId,title,TsAmt;
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow rowUpdate in dataGridView1.SelectedRows)
            {
                workId = rowUpdate.Cells[0].Value.ToString();
                title = rowUpdate.Cells[1].Value.ToString();
                TsAmt = rowUpdate.Cells[3].Value.ToString();
            }
        }
    }
}
