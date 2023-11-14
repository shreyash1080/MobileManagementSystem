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

namespace MobileManagementSystem
{
    public partial class Mobile : Form
    {
        public Mobile()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=ComanderX;Initial Catalog=MobileSoftDb;Integrated Security=True");
        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from MobileTbl ";
            SqlDataAdapter da = new SqlDataAdapter(query,Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);

            var ds = new DataSet();
            da.Fill(ds);
            MobileDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Mobidtb.Text=="" || brandtb.Text=="" || modeletb.Text=="" || pricetb.Text=="" || stocktb.Text=="" || cameratb.Text == "")
            {
                MessageBox.Show("Missing Information");

            }else
            {
                try
                {
                    Con.Open();
                    String sql = "insert into MobileTbl values("+Mobidtb.Text+" , '"+brandtb.Text+"' , '" + modeletb.Text+"' , "+pricetb.Text+" , "+stocktb.Text+" , "+ramcb.SelectedItem.ToString()+" , "+romcb.SelectedItem.ToString()+" , "+cameratb.Text+")";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Mobile Added Successfully");

                    Con.Close();
                    populate();
                 }
                catch(Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void Mobile_Load(object sender, EventArgs e)
        {
            populate();
            //Display info in grid by sac
        }

        private void MobileDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = MobileDGV.Rows[e.RowIndex];
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null && row.Cells[3].Value != null && row.Cells[4].Value != null  &&
                    row.Cells[5].Value != null &&
                        row.Cells[6].Value != null  &&  row.Cells[7].Value != null)
                {
                    Mobidtb.Text = row.Cells[0].Value.ToString();
                    brandtb.Text = row.Cells[1].Value.ToString();
                    modeletb.Text = row.Cells[2].Value.ToString();
                    pricetb.Text = row.Cells[3].Value.ToString();
                    stocktb.Text = row.Cells[4].Value.ToString();
                    ramcb.Text = row.Cells[5].Value.ToString();
                    romcb.Text = row.Cells[6].Value.ToString();
                    cameratb.Text = row.Cells[7].Value.ToString();

                }
            }


            if (MobileDGV.SelectedRows.Count > 0)
            {
                DataGridViewRow row = MobileDGV.SelectedRows[0];

                Mobidtb.Text = row.Cells[0].Value.ToString();
                brandtb.Text = row.Cells[1].Value.ToString();
                modeletb.Text = row.Cells[2].Value.ToString();
                pricetb.Text = row.Cells[3].Value.ToString();
                stocktb.Text = row.Cells[4].Value.ToString();
                ramcb.SelectedItem = row.Cells[5].Value.ToString();
                romcb.SelectedItem = row.Cells[6].Value.ToString();
                cameratb.Text = row.Cells[7].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Mobidtb.Text      = "";
            brandtb.Text      = "";
            modeletb.Text     = "";
            pricetb.Text      = "";
            stocktb.Text      = "";
            ramcb.Text        = "";
            romcb.Text        = "";
            cameratb.Text     = "";


        }

        private void button3_Click(object sender, EventArgs e)
        {
             if(Mobidtb.Text == "")
                {
                MessageBox.Show("Enter The Mobile to be deleted");
                }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from MobileTbl where MobId =" + Mobidtb.Text + " ";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Mobile Deleted");
                    Con.Close();
                    populate();

                }
                catch(Exception Ex)
                {

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Mobidtb.Text == "" || brandtb.Text == "" || modeletb.Text == "" || pricetb.Text == "" || stocktb.Text == "" || cameratb.Text == "")
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                try
                {
                    Con.Open();
                    String sql = "update MobileTbl set Mbrand='" + brandtb.Text+ "', MModel='" + modeletb.Text + "',MPrice=" + pricetb.Text + ", MStock = "+stocktb.Text+", MRam = "+ramcb.SelectedItem.ToString()+", MRom="+romcb.SelectedItem.ToString()+", MCam = "+cameratb.Text+" where MobId="+Mobidtb.Text+"; ";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Mobile updated Successfully");

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
    }
}
