using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MobileManagementSystem
{
    public partial class Accessories : Form
    {
        public Accessories()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=ComanderX;Initial Catalog=MobileSoftDb;Integrated Security=True");

        private void populate()
        {
            Con.Open();
            string query = "select * from AccessorieTbl ";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);

            var ds = new DataSet();
            da.Fill(ds);
            AccessorieDGV.DataSource = ds.Tables[0];

            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (AidTb.Text == "" || AbrandTb.Text == "" || ApriceTb.Text == "" || AmodelTb.Text == "" || AstockTb.Text == "" )
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {
                    Con.Open();
                    String sql = "insert into AccessorieTbl values(" + AidTb.Text + " , '" + AbrandTb.Text + "' , '" + AmodelTb.Text + "' , " + AstockTb.Text + " , " + ApriceTb.Text + " )";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Accessories Added Successfully");

                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Accessories_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AidTb.Text = "";
            AbrandTb.Text = "";
            AmodelTb.Text = "";
            ApriceTb.Text = "";
            AstockTb.Text = ""; 


        }

        private void AccessorieDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (AccessorieDGV.SelectedRows.Count > 0)
            {
                AidTb.Text = AccessorieDGV.SelectedRows[0].Cells[0].Value.ToString();
                AbrandTb.Text = AccessorieDGV.SelectedRows[0].Cells[1].Value.ToString();
                AmodelTb.Text = AccessorieDGV.SelectedRows[0].Cells[2].Value.ToString();
                ApriceTb.Text = AccessorieDGV.SelectedRows[0].Cells[3].Value.ToString();
                AstockTb.Text = AccessorieDGV.SelectedRows[0].Cells[4].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (AidTb.Text == "")
            {
                MessageBox.Show("Enter The Mobile to be deleted");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from AccessorieTbl where AId =" + AidTb.Text + " ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Accessorie Deleted");
                    Con.Close();
                    populate();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (AidTb.Text == "" || AbrandTb.Text == "" || AmodelTb.Text == "" || AstockTb.Text == "" || ApriceTb.Text == "" ) 
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {
                    Con.Open();
                    String sql = "update AccessorieTbl set Abrand='" + AbrandTb.Text + "', AModel='" + AmodelTb.Text + "',APrice=" + ApriceTb.Text + ", AStock = " + AstockTb.Text + " where AId=" + AidTb.Text + "; ";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Accessorie updated Successfully");

                    Con.Close();
                    populate();
                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void back_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void AbrandTb_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
