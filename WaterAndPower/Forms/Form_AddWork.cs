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
    public partial class Form_AddWork : Form
    {
        DataAccess ds;
        public Form_AddWork()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (isFormValid())
            {
                DialogResult dialog = MessageBox.Show("Are You Sure Want to add this Work?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    string hoaId = ds.getSingleValueSingleColum("Select id from tblHeadOfAccounts where AccountNo = '"+cmbHOA.Text+"'",out hoaId,0);
                    ds.InsertUpdateDeleteCreate("insert into tblWorks(title,TSNo,TSAmount,MBNo,PageNo,HOAId) VALUES('"+txtTitle.Text+"','"+txtTsNo.Text+ "','" + txtTsAmount.Text + "','" + txtMBNo.Text + "','" + txtPageNo.Text + "','" + hoaId + "')");
                    MessageBox.Show("Work Added Successfully..!!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
        }

        private bool isFormValid()
        {
            if (txtTitle.Text.Trim() == string.Empty || txtMBNo.Text.Trim() == string.Empty || txtPageNo.Text.Trim() == string.Empty || txtTsAmount.Text.Trim() == string.Empty || txtTsNo.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill all Required Fields...!!","Reuired Fields are Empty",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Form_AddWork_Load(object sender, EventArgs e)
        {
            ds.fillcombobox("select AccountNo from tblHeadOfAccounts", cmbHOA);
        }
    }
}
