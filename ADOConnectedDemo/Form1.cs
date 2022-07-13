using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace ADOConnectedDemo
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;



        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnection"].ConnectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "insert into Employee values(@name,@salary)";
            try
            {
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                
                    MessageBox.Show("Record inserted Succesfully");


                
            }
            catch(SystemException ex)
            {
                MessageBox.Show(ex. Message);
            }
            finally
            {
                con.Close();
            }
    }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string qry = "select * from employee where Id=@id";
            try
            {
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtName.Text = dr["Name"].ToString();
                        txtSalary.Text = dr["Salary"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("No Record");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string qry = "update Employee set Name=@name ,Salary=@salary where Id=@id";
            try
            {
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)

                    MessageBox.Show("Record Updated Succesfully");



            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string qry = "delete from Employee where Id=@id";
            try
            {
                cmd = new SqlCommand(qry, con);
                
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtID.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)

                    MessageBox.Show("Record deleted Succesfully");
                txtID.Clear();
                txtName.Clear();
                txtSalary.Clear();



            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            string qry = "select * from Employee";
            try
            {
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(dr);//its load the tbl format in dt.
                    empGridView.DataSource = dt;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
