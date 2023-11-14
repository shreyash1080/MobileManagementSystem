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
    public partial class SellForm : Form
    {
        public SellForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=ComanderX;Initial Catalog=MobileSoftDb;Integrated Security=True");
     

        private void populate()
        {
            Con.Open();
            string query = "select Mbrand,MModel,Mprice from MobileTbl ";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);

            var ds = new DataSet();
            da.Fill(ds);
            MobileDGV.DataSource = ds.Tables[0];

            Con.Close();
        }

        private void pupulateAccess()
        {
            Con.Open();
            string query = "select Abrand,AModel,Aprice from AccessorieTbl ";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);

            var ds = new DataSet();
            da.Fill(ds);
            AccessorieDGV.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void insertbill()
        {
            if (BillIdtb.Text == "" || clientnametb.Text == "" )
            {
                MessageBox.Show("Missing Information");

            }
            else
            {
                int amount = Convert.ToInt32(Amtlbls.Text);
                try
                {
                    Con.Open();
                    String sql = "insert into BillTbl values(" + BillIdtb.Text + " , '" +clientnametb.Text + "' , " +amount + " )";
                    SqlCommand cmd = new SqlCommand(sql, Con);
                    cmd.ExecuteNonQuery();
               

                    Con.Close();
              

                }
                catch (Exception Ex)
                {

                    MessageBox.Show(Ex.Message);
                }
            }

        }
        private void SellForm_Load(object sender, EventArgs e)
        {
            populate();
            pupulateAccess();
            Sum();
        }
        private void Sum()
        {
            string query = "select sum(Amt) from BillTbl";
            Con.Open();

            SqlDataAdapter sda = new SqlDataAdapter(query,Con);

            DataTable dt = new DataTable();
            sda.Fill(dt);
            SellAmtlbl.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MobileDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = MobileDGV.Rows[e.RowIndex];
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    ProductTb.Text = row.Cells[0].Value.ToString() + row.Cells[1].Value.ToString();
                    priceTb.Text = row.Cells[2].Value.ToString();
                }
            }
        }

        private void AccessorieDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = AccessorieDGV.Rows[e.RowIndex];
                if (row.Cells[0].Value != null && row.Cells[1].Value != null && row.Cells[2].Value != null)
                {
                    ProductTb.Text = row.Cells[0].Value.ToString() + row.Cells[1].Value.ToString();
                    priceTb.Text = row.Cells[2].Value.ToString();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        int n = 0,GrdTotal=0;
        private void button1_Click(object sender, EventArgs e)
        {
      
            if (QtyTb.Text == "" || priceTb.Text =="")
            {
                MessageBox.Show("Enter The Quantity");
            }
            else
            {
                int total = Convert.ToInt32(QtyTb.Text) * Convert.ToInt32(priceTb.Text);
                DataGridViewRow newRow = new DataGridViewRow();
                newRow.CreateCells(BillDGV);
                newRow.Cells[0].Value = n + 1;
                newRow.Cells[1].Value = ProductTb.Text;
                newRow.Cells[2].Value = priceTb.Text;
                newRow.Cells[3].Value = QtyTb.Text;
                newRow.Cells[4].Value = total;
                BillDGV.Rows.Add(newRow);

                n++;

                GrdTotal = GrdTotal + total;
                Amtlbls.Text = ""+GrdTotal;

            }
        }
        int prodid, prodqty, prodprice, tottal, pos = 60;
        string prodname;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("MOBISOFT 1.0 ", new Font("Century Gothic", 12, FontStyle.Bold),Brushes.Red, new Point(90,15));
            e.Graphics.DrawString("ID PRODUCT PRICE QUANTITY TOTAL ", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Red, new Point(26, 40));
            foreach(DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt32(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt32(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt32(row.Cells["Column4"].Value);
                tottal = Convert.ToInt32(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(26, pos));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(45, pos));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(120, pos));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(170, pos));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Blue, new Point(235, pos));
                pos = pos + 20;
            }

            e.Graphics.DrawString("Grand Total Rs" + GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(50, pos + 50));
            e.Graphics.DrawString("***************MobiSoft***************" + GrdTotal, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Crimson, new Point(10, pos + 85));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            GrdTotal = 0;
            n = 0;
            insertbill();
            Sum();
        }

        private void back_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Hide();
        }

        private void label13_Click_1(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm",285,600);
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }
    }
}
