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
    public partial class UC_Contractors : UserControl
    {
        DataAccess ds;
        public UC_Contractors()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void Panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            
            if (txtName.Text.Trim() == string.Empty && txtAddress.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill all required Fields","Required Fields are Empty",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Are you sure Want to add this Contractor?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    ds.InsertUpdateDeleteCreate("insert into tblContractors(Name,Address) VALUES('" + txtName.Text + "','" + txtAddress.Text + "')");
                    MessageBox.Show("Contractor Added Successfully... :) :)", "Contractor Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAddress.Text = "";
                    txtName.Text = "";
                    this.OnLoad(e);
                }
                
            }
        }

        private void UC_Contractors_Load(object sender, EventArgs e)
        {
            ds.fillgridView("select * from tblContractors", dataGridView1);
            lblNo.Text = dataGridView1.Rows.Count.ToString();
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            if (cmbSearchType.Text == "Contractor Id")
            {
                ds.fillgridView("select * from tblContractors where id = '"+txtSearch.Text+"'", dataGridView1);
            }
            else if (cmbSearchType.Text == "Name")
            {
                ds.fillgridView("select * from tblContractors where Name like '%" + txtSearch.Text + "%'", dataGridView1);
            }
            else if (cmbSearchType.Text == "Address")
            {
                ds.fillgridView("select * from tblContractors where address like '%" + txtSearch.Text + "%'", dataGridView1);
            }
            else
            {
                this.OnLoad(e);
            }
            if (txtSearch.Text.Length <= 0)
            {
                this.OnLoad(e);
            }
        }
    }
}
