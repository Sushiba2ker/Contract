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
    public partial class Form_AssignWork : Form
    {
        public string workId { get; set; }
        public string workTitle { get; set; }
        public string TsAmt { get; set; }
        DataAccess ds;
        public Form_AssignWork()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_AssignWork_Load(object sender, EventArgs e)
        {
            lblWorkTitle.Text = workTitle;
            lblTSAmt.Text = TsAmt;
            ds.fillcombobox("select name from tblContractors", cmbContractor);
            cmbYear.SelectedIndex = 0;
        }

        private void CmbContractor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string address = ds.getSingleValueSingleColum("select address from tblContractors where name = '"+cmbContractor.Text+"'",out address,0);
            lblContactorAddress.Text = address;
        }
        string ContractorId;
        private void CmbContractor_Leave(object sender, EventArgs e)
        {
            string address = ds.getSingleValueSingleColum("select address from tblContractors where name = '" + cmbContractor.Text + "'", out address, 0);
            lblContactorAddress.Text = address;

            ContractorId = ds.getSingleValueSingleColum("select id from tblContractors where name = '" + cmbContractor.Text + "' and address = '"+lblContactorAddress.Text+"'", out ContractorId, 0);
            if (ContractorId == null)
            {
               MessageBox.Show("Contractor Not Found.. Please Add Contractor First.. ","Contractor Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (isFormValid())
            {
                DialogResult dialog = MessageBox.Show("Are You Sure Want to Assign this Work?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    ds.InsertUpdateDeleteCreate("insert into tblWorkAssigned(WorkId,ContractorId,CACost,AssignDate,Period,AssignedBy) VALUES('" + workId + "','" + ContractorId + "','" + txtCACost.Text + "','" + txtDate.Text + "','" + cmbYear.Text + "',1)");
               
                    string workAssignId = ds.getSingleValueSingleColum("select max(id) from tblWorkAssigned", out workAssignId, 0);

                    ds.InsertUpdateDeleteCreate("insert into tblCalculations(WorkAssignId,WorkDone,NowToBePaid,WorkToBeDone,AmountPaid,BalanceAmount,UserId) VALUES('" + workAssignId + "',0,0,'" + txtCACost.Text + "',0,'" + txtCACost.Text + "',1)");
                    ds.InsertUpdateDeleteCreate("update tblWorks set isAssigned = 1 where id = '"+workId+"'");

                    MessageBox.Show("Work Assigned Successfully..!! :) :)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                
            }
        }

        private bool isFormValid()
        {
            if (ContractorId == null)
            {
                MessageBox.Show("Contractor Not Found.. Please Add contractor First..", "Unable to Save Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (cmbContractor.Text.Trim() == string.Empty || cmbYear.Text.Trim() == string.Empty || txtCACost.Text.Trim() == string.Empty ||txtDate.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill All Required Fileds..","Required Fields are empty",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            else if (double.Parse(txtCACost.Text) <= double.Parse(lblTSAmt.Text))
            {
                MessageBox.Show("Cannot Assign a Contract with a less CA Cost then TS Amount.","CA Amount is Less than TS Amount",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
