using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmonics19.DbFiles
{
    class DataAccess
    {
        static string _ConnectionString = @"Data Source=DESKTOP-D65NCRR\SQLEXPRESS;Initial Catalog=ContractDb;Integrated Security=True";
        SqlCommand cmd_;
        SqlConnection conn_;
        SqlDataAdapter adptr_;
        SqlDataReader reader_;
        DataTable dtable_;

        public string getmessage { get; set; }

        public DataAccess()
        {
            conn_ = new SqlConnection(_ConnectionString);
            cmd_ = new SqlCommand();
            dtable_ = new DataTable();
            adptr_ = new SqlDataAdapter();


        }

        public bool connect()
        {
            try
            {
                conn_.Open();
                getmessage = "successfully connected";
                return true;

            }
            catch (Exception ex)
            {
                getmessage = "connection error" + ex.Message;
                return false;
            }

        }
        public bool disconnect()
        {
            try
            {
                conn_.Close();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public string getSingleValueSingleColum(string query, out string columnData, int index)
        {
            string ret = null;
            try
            {
                cmd_.Connection = conn_;
                cmd_.CommandText = query;
                connect();
                reader_ = cmd_.ExecuteReader();
                while (reader_.Read())
                {
                    ret = reader_[index].ToString();
                }
                //ret = "successfull";
                getmessage = "successfull get value";
            }
            catch (Exception e)
            {

                getmessage = "error" + e.Message;
            }
            finally
            {
                disconnect();
            }
            columnData = ret;
            return ret;
        }

        public string custominsertupdateDelete(SqlCommand cmd2parameterizednoconnectionNeeded)
        {
            string ret = "";
            string allqueries = cmd2parameterizednoconnectionNeeded.CommandText.ToLower();
            try
            {
                cmd2parameterizednoconnectionNeeded.Connection = conn_;
                connect();

                cmd2parameterizednoconnectionNeeded.ExecuteNonQuery();
                if (allqueries.Contains("insert into "))
                {
                    ret = getmessage = "inserted Successfully!";
                }
                else if (allqueries.Contains("delete from "))
                {
                    ret = getmessage = "Deleted Successfully!";
                }
                else if (allqueries.Contains("create table "))
                {
                    ret = getmessage = "Table Created Successfully!";
                }
                else if (allqueries.Contains("update  ") && allqueries.Contains("set="))
                {
                    ret = getmessage = "Updated Successfully";
                }


            }
            catch (Exception exp)
            {

                ret = getmessage = "Failed to execute " + cmd2parameterizednoconnectionNeeded.CommandText + " \n Reason : " + exp.Message;
            }
            finally { disconnect(); }
            return ret;
        }


        public string InsertUpdateDeleteCreate(string query)
        {
            string ret = "";
            string allquerys = query.ToLower();

            try
            {
                cmd_.CommandText = query;
                cmd_.Connection = conn_;
                connect();
                cmd_.ExecuteNonQuery();

                if (allquerys.ToLower().Contains("insert into"))
                {
                    ret = getmessage = (" inseteted successfully ");
                }
                else if (allquerys.Contains("delete form"))
                {
                    ret = getmessage = ("delete successfull");
                }
                else if (allquerys.Contains("Update into") && allquerys.Contains("set"))
                {
                    ret = getmessage = ("update successfull");
                }
                else if (allquerys.Contains("Creat table"))
                {
                    ret = getmessage = ("create table successful");
                }

            }
            catch (Exception exp)
            {

                ret = getmessage = "failed to execute" + query + "\n resoin :" + exp.Message;
            }
            finally { disconnect(); }
            return ret;
        }

        public string fillListView(string query, System.Windows.Forms.ListView dgv)
        {
            dtable_ = new System.Data.DataTable();
            string stret;
            try
            {
                cmd_.Connection = conn_;
                cmd_.CommandText = query;
                connect();
                adptr_.SelectCommand = cmd_;

                adptr_.Fill(dtable_);

                for (int i = 0; i < dtable_.Rows.Count; i++)
                {
                    DataRow dr = dtable_.Rows[i];
                    ListViewItem listitem = new ListViewItem(dr["Id"].ToString());
                    listitem.SubItems.Add(dr["Name"].ToString());
                    listitem.SubItems.Add(dr["Quantity"].ToString());
                    listitem.SubItems.Add(dr["PerUnitPrice"].ToString());
                    listitem.SubItems.Add(dr["Net Amount"].ToString());
                    dgv.Items.Add(listitem);
                }
                dgv.Refresh();

                stret = "Code Executed Successfully (fillListView()=> datalayer.cs)";

            }
            catch (Exception exp)
            {

                stret = "Failed (filldatagridView()=> datalayer.cs) : " + exp.Message;

            }
            finally
            {
                disconnect();
                dtable_ = null;
            }
            return stret;

        }

        public string fillgridView(string query, System.Windows.Forms.DataGridView dgv)
        {
            dtable_ = new System.Data.DataTable();
            string stret;
            try
            {
                cmd_.Connection = conn_;
                cmd_.CommandText = query;
                connect();
                adptr_.SelectCommand = cmd_;

                adptr_.Fill(dtable_);

                dgv.DataSource = dtable_;
                dgv.Refresh();

                stret = "Code Executed Successfully (filldatagridView()=> datalayer.cs)";

            }
            catch (Exception exp)
            {

                stret = "Failed (filldatagridView()=> datalayer.cs) : " + exp.Message;

            }
            finally
            {
                disconnect();
                dtable_ = null;
            }
            return stret;

        }

        public string getSingleValueAsArraybyIndex(string query, out List<string> columndata, int index)

        {
            List<string> data = new List<string>();
            string ret;

            try
            {

                cmd_.Connection = conn_;
                cmd_.CommandText = query.ToLower();

                connect();
                reader_ = cmd_.ExecuteReader();

                while (reader_.Read())
                {

                    data.Add(reader_[index].ToString());

                }


                ret = "Operation Successfull!";
                getmessage = "values successfully got from getSingleValueAsArrayByindex() function";
            }
            catch (Exception exp)
            {
                ret = "Error in datalayer -> getSingleValueAsArrayByIndex() Reason:" + exp.Message;
                getmessage = "Error in datalayer getSingleValueAsArrayByIndex() for reader_ \n" + exp.Message;
                data.Clear();
            }
            finally
            {
                disconnect();
            }

            columndata = data;
            return ret;

        }


        public void fillcombobox(string qry, System.Windows.Forms.ComboBox cmd)

        {
            int i = 0;
            List<string> lst = new List<string>();
            getSingleValueAsArraybyIndex(qry, out lst, 0);

            foreach (string val in lst)
            {
                if (val.Length > 0)
                {
                    cmd.Items.Add(val);
                    i++;
                }

            }
            if (i > 0)
            {
                cmd.SelectedIndex = 0;
            }

        }

        public string[] getArray(string query, int length)
        {
            string[] ret = new string[length];
            try
            {
                cmd_.Connection = conn_;
                cmd_.CommandText = query;
                connect();
                reader_ = cmd_.ExecuteReader();
                while (reader_.Read())
                {
                    for (int i = 0; i <= reader_.FieldCount; i++)
                    {
                        ret[i] = reader_[i].ToString();
                    }
                    //    ret[0] = reader_[0].ToString(); 
                    //    ret[1] = reader_[1].ToString(); 
                    //    ret[2] = reader_[2].ToString(); 
                    //    ret[3] = reader_[3].ToString(); 
                    //    ret[4] = reader_[4].ToString();
                    //    ret[5] = reader_[5].ToString();
                    //    ret[6] = reader_[6].ToString();
                    //    ret[7] = reader_[7].ToString();
                }
                //ret = "successfull";
                //getmessage = "successfull get value";
            }
            catch (Exception e)
            {

                // getmessage = "error" + e.Message;
            }
            finally
            {
                disconnect();
            }

            return ret;

        }
    }
}
