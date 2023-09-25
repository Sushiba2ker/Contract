using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pharmonics19.DbFiles;

namespace WaterAndPower.UserControls
{
    public partial class UC_Dashboard : UserControl
    {
        DataAccess ds;
        public UC_Dashboard()
        {
            InitializeComponent();
            ds = new DataAccess();
        }
        string contractors, noOfJobs, AssignedJobs, completedJobs, WorkDoneAmt, workToBeDone, TotalPaid;
        private void UC_Dashboard_Load(object sender, EventArgs e)
        {
           contractors = ds.getSingleValueSingleColum("select count(id) from tblContractors",out contractors,0);
           noOfJobs = ds.getSingleValueSingleColum("select count(id) from tblWorks",out noOfJobs,0);
           AssignedJobs = ds.getSingleValueSingleColum("select count(id) from tblWorkAssigned",out AssignedJobs,0);
           completedJobs = ds.getSingleValueSingleColum("select count(id) from tblWorkAssigned where isCompleted = 1", out completedJobs,0);

           WorkDoneAmt = ds.getSingleValueSingleColum("select sum(WorkDone) from tblCalculations", out WorkDoneAmt,0);
           workToBeDone = ds.getSingleValueSingleColum("select sum(WorkToBeDone) from tblCalculations", out workToBeDone,0);
            TotalPaid = ds.getSingleValueSingleColum("select sum(AmountPaid) from tblCalculations", out TotalPaid, 0);


            lblContractors.Text = contractors;
            lblJobs.Text = noOfJobs;
            blAssignedJobs.Text = AssignedJobs;
            blCompletedJobs.Text = completedJobs;
            blWorkDone.Text = WorkDoneAmt;
            blWorkToBeDone.Text = workToBeDone;
            lblTotalPaid.Text = TotalPaid;

        }
    }
}
