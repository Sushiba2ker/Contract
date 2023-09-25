using Pharmonics19._1.Helpers;
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
using WaterAndPower.UserControls;

namespace WaterAndPower.Forms
{
    public partial class Form_Dashboard : Form
    {
        int panelWidth;
        bool Hidden;
        DataAccess ds;
        public Form_Dashboard()
        {
            InitializeComponent();
            ds = new DataAccess();
            panelWidth = panelLeft.Width;
            Hidden = false;
            UC_Dashboard uC = new UC_Dashboard();
            addControls(uC);
        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void slidePanel(Button btn)
        {
            panelSide.Height = btn.Height;
            panelSide.Top = btn.Top;
        }

        private void addControls(UserControl uc)
        {
            panelContainer.Controls.Clear();
            uc.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(uc);
            uc.BringToFront();
        }


        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Dashboard";
            slidePanel(btnDashboard);
            UC_Dashboard uC = new UC_Dashboard();
            addControls(uC);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Hidden)
            {
                panelLeft.Width = panelLeft.Width + 10;
                if (panelLeft.Width >= panelWidth)
                {
                    timer1.Stop();
                    Hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                panelLeft.Width = panelLeft.Width - 10;
                if (panelLeft.Width <= 55)
                {
                    timer1.Stop();
                    Hidden = true;
                    this.Refresh();
                }
            }
        }

        private void BtnJobs_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Assigned Jobs";
            slidePanel(btnJobs);
            UC_AssignedJobs ass = new UC_AssignedJobs();
            addControls(ass);
        }

        private void BtnAboutUs_Click(object sender, EventArgs e)
        {
            using (Form_AboutUs uu = new Form_AboutUs())
            {
                uu.ShowDialog();
            }
        }

        private void BtnContractors_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Manage Contractors";
            slidePanel(btnContractors);
            UC_Contractors ucc = new UC_Contractors();
            addControls(ucc);
        }

        private void BtnWorks_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Manage Works";
            slidePanel(btnWorks);
            UC_Jobs ab = new UC_Jobs();
            addControls(ab);
        }

        private void BtnAnalytics_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Analytics";
            slidePanel(btnAnalytics);
            UC_Analytics anl = new UC_Analytics();
            addControls(anl);
          
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Reports";
            slidePanel(btnReports);
            UC_Reports anl = new UC_Reports();
            addControls(anl);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "Settings";
            slidePanel(btnSettings);
            UC_Settings anl = new UC_Settings();
            addControls(anl);
        }

        private void Form_Dashboard_Load(object sender, EventArgs e)
        {
            lblUserName.Text = Helper.UserData[1];
            string RoleName = ds.getSingleValueSingleColum("select Description from tblRoles where id = '" + Helper.UserData[2] + "'", out RoleName, 0);
            lblRole.Text = RoleName;
        }
    }
}
