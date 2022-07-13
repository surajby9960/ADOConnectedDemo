using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace ADOConnectedDemo
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        public DataSet GetAllEmp()
        {
            da = new SqlDataAdapter("select * from Employee", con);
            //assign PK to the col in the dataset..means local db.
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            //emp is the tablle name given which is in the dataset.
            da.Fill(ds, "emp");//fill method fire the select qry.
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["emp"].NewRow();
                row["Name"] = txtName.Text;
                row["Salary"] = txtSalary.Text;
                //attch the new row to the ds;
                ds.Tables["emp"].Rows.Add(row);
                //update will reflect the changes
                int result = da.Update(ds.Tables["emp"]);
                if (result == 1)
                {
                    MessageBox.Show("Succesful insrted");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["emp"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row["Name"] = txtName.Text;
                    row["Salary"] = txtSalary.Text;
                   
                    //update will reflect the changes
                    int result = da.Update(ds.Tables["emp"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Succesful updated");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["emp"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row.Delete();
                    //update will reflect the changes
                    int result = da.Update(ds.Tables["emp"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record Deleted");
                        txtID.Clear();
                        txtName.Clear();
                        txtSalary.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllEmp();
                DataRow row = ds.Tables["emp"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    txtName.Text = row["Name"].ToString();
                    txtSalary.Text = row["Salary"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ds = GetAllEmp();
            empGridView.DataSource = ds.Tables["emp"];
        }
    }
}
