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
    public partial class Form_PayAmount : Form
    {
        public string WorkAssignId { get; set; }
        public string WorkTitle { get; set; }
        public string ContractorName { get; set; }
        public string CACost { get; set; }

        DataAccess ds;
        public Form_PayAmount()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string[] calcData;
        private void Form_PayAmount_Load(object sender, EventArgs e)
        {
            calcData = ds.getArray("select id,WorkDone,NowToBePaid,WorkToBeDone,AmountPaid,BalanceAmount from tblCalculations where workAssignId = '"+WorkAssignId+"'",6);
            lblJobTitle.Text = WorkTitle;
            lblContractorName.Text = ContractorName;
            lblCACost.Text = CACost;
            lblWorkDone.Text = calcData[1];
            lblToBePaid.Text = calcData[2];
            lblWorkToBeDone.Text = calcData[3];
            lblBalanceAmt.Text = calcData[5];
            ToBePaid = lblToBePaid.Text;
        }
        string ToBePaid;

        private void TxtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtPaidAmount.Text.Length > 0)
            {
                lblToBePaid.Text = (double.Parse(ToBePaid) - double.Parse(txtPaidAmount.Text)).ToString();
                if (double.Parse(lblToBePaid.Text) < 0)
                {
                    MessageBox.Show("Cannot Pay Amount Greater then Amount to be Paid.. Amount to be Paid is "+ToBePaid,"Payment Amount is Greater then Amount to be Paid",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtPaidAmount.Text = "";
                    lblToBePaid.Text = ToBePaid;
                }
            }
            else
            {
                lblToBePaid.Text = ToBePaid;
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (txtPaidAmount.Text.Trim() == string.Empty || double.Parse(lblToBePaid.Text) < 0 || double.Parse(txtPaidAmount.Text) <=0)
            {
                MessageBox.Show("Cannot Save Record... Please Check your Amount Paid and Amount to Be Paid..","Unable to save Records",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Are You Sure Want to save this record?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    ds.InsertUpdateDeleteCreate("insert into tblCalculationsData(workAssignId,WorkDone,NowToBePaid,WorkToBeDone,AmountPaid,BalanceAmount,UserId) VALUES('" + WorkAssignId + "','" + lblWorkDone.Text + "','" + lblToBePaid.Text + "','" + lblWorkToBeDone.Text + "','" + txtPaidAmount.Text + "','" + lblBalanceAmt.Text + "',1)");
                    double paidAmt = double.Parse(calcData[4]) + double.Parse(txtPaidAmount.Text);
                 
                    if (lblToBePaid.Text == "0" && lblCACost.Text == lblWorkDone.Text)
                    {
                        ds.InsertUpdateDeleteCreate("update tblWorkAssigned set isCompleted = 1 where id = '"+WorkAssignId+"'");
                    }

                    ds.InsertUpdateDeleteCreate("update tblCalculations set AmountPaid = '" + paidAmt + "', NowToBePaid ='" + lblToBePaid.Text + "' where WorkAssignId = '" + WorkAssignId + "'");
                   
                    MessageBox.Show("Payment Added Successfully... :) :)", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
                
            }
        }
    }
}
