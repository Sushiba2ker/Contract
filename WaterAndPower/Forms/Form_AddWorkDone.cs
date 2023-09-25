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
    public partial class Form_AddWorkDone : Form
    {
        public string WorkAssgnId { get; set; }
        public string WorkTitle { get; set; }
        public string ContractorName { get; set; }
        public string CACost { get; set; }

        DataAccess ds;
        public Form_AddWorkDone()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Form_AddWorkDone_Load(object sender, EventArgs e)
        {
            lblTitle.Text = WorkTitle;
            lblName.Text = ContractorName;
            string workToBeDone = ds.getSingleValueSingleColum("select WorkToBeDone from tblCalculations where WorkAssignId = '" + WorkAssgnId + "'", out workToBeDone, 0);
            lblToBeDone.Text = workToBeDone;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (txtWorkDone.Text.Trim() == string.Empty || double.Parse(txtWorkDone.Text) == 0)
            {
                MessageBox.Show("Please add Work Done by Contractor","Work Done Empty",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if(double.Parse(txtWorkDone.Text) > double.Parse(lblToBeDone.Text))
            {
                MessageBox.Show("Cannot add Work Done Greater than Work to be Done..!","Work Done is Greater than Work to be Done",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                string WorkDone = ds.getSingleValueSingleColum("select WorkDone from tblCalculations where WorkAssignId = '"+WorkAssgnId+"'", out WorkDone,0);
                double newWorkDone = double.Parse(txtWorkDone.Text) + double.Parse(WorkDone);

                double balance = double.Parse(CACost) - newWorkDone;

                string tobePaid = ds.getSingleValueSingleColum("select NowToBePaid from tblCalculations where WorkAssignId = '" + WorkAssgnId + "'", out WorkDone, 0);
                double newtoBePaid = double.Parse(txtWorkDone.Text) + double.Parse(tobePaid);

                double WorkToBeDone = double.Parse(lblToBeDone.Text) - double.Parse(txtWorkDone.Text);

                ds.InsertUpdateDeleteCreate("insert into tblCalculationsData(WorkAssignId,WorkDone,NowToBePaid,WorkToBeDone,AmountPaid,BalanceAmount,UserId) VALUES('" + WorkAssgnId + "','"+newWorkDone+"','"+newtoBePaid+"','" + WorkToBeDone + "',0,'" + balance + "',1)");

                ds.InsertUpdateDeleteCreate("update tblCalculations set WorkDone = '"+newWorkDone+ "', NowToBePaid = '"+ newtoBePaid + "',WorkToBeDone = '"+WorkToBeDone+ "', BalanceAmount='"+balance+ "' where WorkAssignId = '" + WorkAssgnId + "'");

                MessageBox.Show("Record Added Successfully... :)","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Dispose();
            }
        }
    }
}
