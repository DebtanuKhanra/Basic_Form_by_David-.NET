using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace Basic_Form_by_David
{
    public partial class Form1 : Form
    {
        public void mustinput() // By this operation we cann't skip any Input box
        {
            if (textJobid.Text == "")
            {
                MessageBox.Show("Please Type  JobId");
                textJobid.Focus();
            }
            else if (textFname.Text == "")
            {
                MessageBox.Show("Please Type  First Name");
                textFname.Focus();
            }
            else if (textLname.Text == "")
            {
                MessageBox.Show("Please Type  Last Name");
                textLname.Focus();
            }
            else if (textEmail.Text == "")
            {
                MessageBox.Show("Please Type  Email");
                textEmail.Focus();
            }
            else if (textSal.Text == "")
            {
                MessageBox.Show("Please Type  Salary");
                textSal.Focus();
            }
        }



        public void scommand(SqlCommand sqlcmd)
        {
            sqlcmd.Parameters.AddWithValue("@jobid",textJobid.Text);
            sqlcmd.Parameters.AddWithValue("@fname",textFname.Text);
            sqlcmd.Parameters.AddWithValue("@lname",textLname.Text);
            sqlcmd.Parameters.AddWithValue("@email",textEmail.Text);
            sqlcmd.Parameters.AddWithValue("@sal",textSal.Text);
        }



        public void emptyrecord() //After datas are successfully updated or inserted or deleted, all the text boxes empty automatically
        {
            textJobid.Text = "";
            textFname.Text = "";
            textLname.Text = "";
            textEmail.Text = "";
            textSal.Text = "";
        }



        public Form1()
        {
            InitializeComponent();
        }

        private void textJobid_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            string strcon= ConfigurationManager.ConnectionStrings["first_config"].ConnectionString;
            SqlConnection conn1 = new SqlConnection(strcon);


            if (textJobid.Text.Length == 0 || textFname.Text.Length == 0 || textLname.Text.Length == 0 || textEmail.Text.Length == 0 || textSal.Text.Length == 0)
            {
                mustinput();
            }
            else
            {
                try
                {
                    conn1.Open();

                    SqlCommand checkexistdata = new SqlCommand("select JobId from table1 where JobId='" + textJobid.Text + "'", conn1); // To check Input data is already exsist or not.
                    SqlDataAdapter adapter = new SqlDataAdapter(checkexistdata);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show("This Job ID already exist.\nEnter Your Job ID");
                        textJobid.Text = "";
                    }
                    else
                    
                    
                    
                    {

                        string query = "insert into table1 values(@jobid,@fname,@lname,@email,@sal)";
                        SqlCommand cmd = new SqlCommand(query, conn1);
                        scommand(cmd);

                        int i = cmd.ExecuteNonQuery();


                        conn1.Close();

                        if (i > 0) // if insert operation executed successfully then it return 1 else 0
                        {
                            MessageBox.Show("Data Inserted Successfully");
                            emptyrecord(); //After datas are successfully insertedted, all the text boxes empty automatically

                            /*textJobid.Text = ""; //we can use it directly and also can use it as make a function for more use
                            textFname.Text = "";
                            textLname.Text = "";
                            textEmail.Text = "";
                            textSal.Text = "";*/
                        }
                        else
                        {
                            MessageBox.Show("Data not Inserted");
                        }
                    }
                }
                catch (Exception em)
                {
                    MessageBox.Show(em.Message.ToString());
                }
            }

        }





        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string strcon = ConfigurationManager.ConnectionStrings["first_config"].ConnectionString;
            SqlConnection conn2=new SqlConnection(strcon);


            if (textJobid.Text.Length == 0 || textFname.Text.Length == 0 || textLname.Text.Length == 0 || textEmail.Text.Length == 0 || textSal.Text.Length == 0)
            {
                mustinput();
            }
            else
            {

                try
                {
                    conn2.Open();
                    String query = "update table1 set FirstName=@fname,LastName=@lname,Email=@email,Salary=@sal where JobId=@jobid";
                    SqlCommand cmd = new SqlCommand(query, conn2);
                    scommand(cmd);

                    int u = cmd.ExecuteNonQuery();
                    conn2.Close();

                    if (u > 0) // if update operation executed successfully then it return 1 else 0
                    {
                        MessageBox.Show("Data Updated Successfully");
                        emptyrecord(); //After datas are successfully updated, all the text boxes empty automatically
                    }
                    else
                    {
                        MessageBox.Show("Data Not Updated Successfully");
                    }

                }
                catch (Exception em)
                {
                    MessageBox.Show(em.Message.ToString());
                }
            }
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            string strcon = ConfigurationManager.ConnectionStrings["first_config"].ConnectionString;
            SqlConnection conn3 = new SqlConnection(strcon);



            if (textJobid.Text == "") {
                MessageBox.Show("Please Enter Job ID.");
                textJobid.Focus();
            }
            else
            {
                try
                { 
                    conn3.Open();

                    String query = "delete from table1 where JobId=@jobid";
                    SqlCommand cmd=new SqlCommand(query, conn3);
                    cmd.Parameters.AddWithValue("@jobid",textJobid.Text);
                    int d=cmd.ExecuteNonQuery();

                    conn3.Close();

                    if (d > 0) // if update operation executed successfully then it return 1 else 0
                    {
                        MessageBox.Show("Data Deleted Successfully");
                        emptyrecord(); //After datas are successfully deleted, all the text boxes empty automatically
                    }
                    else
                    {
                        MessageBox.Show("Data Not Deleted Successfully");
                    }

                }
                catch (Exception em)
                {
                    MessageBox.Show(em.Message.ToString());
                }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            String strcon = ConfigurationManager.ConnectionStrings["first_config"].ConnectionString;
            SqlConnection conn4=new SqlConnection(strcon);

            conn4.Open();

            SqlCommand cmd=new SqlCommand("select*from table1",conn4);

            SqlDataAdapter adapter=new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource= dt;

            conn4.Close();
        }
    }
}
