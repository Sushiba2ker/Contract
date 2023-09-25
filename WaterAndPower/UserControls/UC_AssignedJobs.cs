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
    public partial class UC_AssignedJobs : UserControl
    {
        DataAccess ds;
        public UC_AssignedJobs()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (workAssignId !=null && !completed)
            {
                using (Form_PayAmount py = new Form_PayAmount())
                {
                    py.WorkAssignId = workAssignId;
                    py.WorkTitle = title;
                    py.ContractorName = ContractorName;
                    py.CACost = CACost;
                    py.ShowDialog();
                    this.OnLoad(e);
                }
            }
            
        }

        private void BtnUsers_Click(object sender, EventArgs e)
        {
            if (workAssignId != null && !completed)
            {
                using (Form_AddWorkDone wd = new Form_AddWorkDone())
                {
                    wd.WorkTitle = title;
                    wd.ContractorName = ContractorName;
                    wd.WorkAssgnId = workAssignId;
                    wd.CACost = CACost;
                    wd.ShowDialog();
                }
            }
            
        }

        private bool completed;
        private void UC_AssignedJobs_Load(object sender, EventArgs e)
        {
            ds.fillgridView("select wa.id,w.title as 'Work Title',c.name as 'Contractor Name',wa.CACost,wa.AssignDate,wa.Period from tblWorkAssigned as wa inner join tblWorks as w ON wa.WorkId = w.id inner join tblContractors as c ON wa.ContractorId = c.id where w.isAssigned = 1 and wa.isCompleted = 0", dataGridView1);
            completed = false;
        }

        string workAssignId, title, ContractorName,CACost;

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            using (Form_ViewCalculations vc = new Form_ViewCalculations())
            {
                vc.WorkAssignId = workAssignId;
                vc.WorkTitle = title;
                vc.ShowDialog();
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!completed)
            {
                ds.fillgridView("select wa.id,w.title as 'Work Title',c.name as 'Contractor Name',wa.CACost,wa.AssignDate,wa.Period from tblWorkAssigned as wa inner join tblWorks as w ON wa.WorkId = w.id inner join tblContractors as c ON wa.ContractorId = c.id where w.isAssigned = 1 and wa.isCompleted = 1", dataGridView1);
                completed = true;
                btnEdit.Text = "   Not Completed";
            }
            else
            {
                btnEdit.Text = "   Completed Jobs";
                this.OnLoad(e);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow rowUpdate in dataGridView1.SelectedRows)
            {
                workAssignId = rowUpdate.Cells[0].Value.ToString();
                title = rowUpdate.Cells[1].Value.ToString();
                ContractorName = rowUpdate.Cells[2].Value.ToString();
                CACost = rowUpdate.Cells[3].Value.ToString();
            }
        }
    }
}
