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
    public partial class UC_Settings : UserControl
    {
        DataAccess ds;
        public UC_Settings()
        {
            InitializeComponent();
            ds = new DataAccess();
        }

        private void UC_Settings_Load(object sender, EventArgs e)
        {
            ds.fillgridView("select * from tblHeadOfAccounts",dataGridView1);
            ds.fillgridView("select * from tblDesignation",dataGridView3);
            ds.fillcombobox("select title from tblDesignation",cmbDesignation);
            ds.fillcombobox("select description from tblRoles",cmbRole);
            ds.fillgridView("select id,FullName,FathersName,CellNo,UserName from tblUsers",dataGridView2);
        }

        private void BtnItemType_Click(object sender, EventArgs e)
        {
            if (txtHoa.Text.Trim() == string.Empty || txtTitle.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please fill All Required Fields...","Required Fields are Missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Are you sure want to add this Head of Accounts?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    ds.InsertUpdateDeleteCreate("insert into tblHeadOfAccounts(Name,AccountNo) VALUES('" + txtTitle.Text + "','" + txtHoa.Text + "')");
                    MessageBox.Show("Head of Accounts Save Successfully..", "Item Type Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtHoa.Text = "";
                    txtTitle.Text = "";
                    this.OnLoad(e);
                }
                
            }
        }

        private void TabPage2_Click(object sender, EventArgs e)
        {

        }
        private bool isFormValid()
        {
            if (txtFullName.Text.Trim() == string.Empty || txtFatherName.Text.Trim() == string.Empty || txtCNIC.Text.Trim() == string.Empty || txtCellNo.Text.Trim() == string.Empty || txtUserName.Text.Trim() == string.Empty || txtPassword.Text.Trim() == string.Empty || txtAddress.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Fill All Fields.", "All Fields are Required", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        string desId, RoleId;

        private void Button1_Click(object sender, EventArgs e)
        {
            if (txtDesignation.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Please Enter a Designation First...","Designation Field is Required",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialog = MessageBox.Show("Are you sure want to add this Designation?","Confirmation",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dialog == DialogResult.Yes)
                {
                    ds.InsertUpdateDeleteCreate("insert into tblDesignation(Title) VALUES('"+txtDesignation.Text+"')");
                    MessageBox.Show("Designation Added Successfully... :) :)","Added Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                this.OnLoad(e);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (isFormValid())
            {
                desId = ds.getSingleValueSingleColum("select id from tblDesignations where name = '" + cmbDesignation.Text + "'", out desId, 0);
                RoleId = ds.getSingleValueSingleColum("select id from tblRoles where description = '" + cmbRole.Text + "'", out RoleId, 0);

                ds.InsertUpdateDeleteCreate("insert into tblUsers(FullName,FathersName,CNIC,CellNo,UserName,Password,address,DesignationId,RoleId) VALUES ('" + txtFullName.Text + "','" + txtFatherName.Text + "','" + txtCNIC.Text + "','" + txtCellNo.Text + "','" + txtUserName.Text + "','" + txtPassword.Text + "','" + txtAddress.Text + "','" + desId + "','" + RoleId + "')");
                MessageBox.Show("User Added Successfully... :) :)", "User Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.OnLoad(e);
            }
        }
    }
}
