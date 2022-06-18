using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List
{
    public partial class dela : Form
    {
        string strConn = @"Data Source=desktop-kh25c9u\sqlexpress;Initial Catalog=msdb;Integrated Security=True";
        string id;
        public Button but1;
        public Button but2;
        public Button but3;
        public Button but4;


        public dela()
        {
            InitializeComponent();
            dataGridView1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void Clean3bt()
        {
            button5.Visible = false;
            button6.Visible = false;
            button3.Visible = false;
        }
        private void PopulateGrid(string del)
        { // наполнение сетки DataGridView данными
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                try
                {

                    DataTable table = new DataTable();
                    string command = "select ЗАДАЧА, ДАТА, ГОТОВО from " + del;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command, conn);
                    dataAdapter.Fill(table);
                    dataGridView1.DataSource = table.DefaultView;

                }
                catch (Exception) { }
            }

        }
        private void dela_Load(object sender, EventArgs e)
        {
            //PopulateGrid("d1");
            //b8 = true;
            //button8.BackColor = Color.LightCoral;
        }

        private void button8_Click(object sender, EventArgs e)
        {

            button8.BackColor = Color.LightCoral;
            button9.BackColor = Color.Thistle;
            button10.BackColor = Color.Thistle;
            button11.BackColor = Color.Thistle;
            Clean3bt();
            PopulateGrid("d1");
            dataGridView1.Enabled = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {

            button9.BackColor = Color.LightGreen;
            button8.BackColor = Color.Thistle;
            button10.BackColor = Color.Thistle;
            button11.BackColor = Color.Thistle;
            Clean3bt();
            PopulateGrid("d2");
            dataGridView1.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {

            button11.BackColor = Color.SandyBrown;
            button9.BackColor = Color.Thistle;
            button10.BackColor = Color.Thistle;
            button8.BackColor = Color.Thistle;
            Clean3bt();
            PopulateGrid("d3");
            dataGridView1.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {

            button10.BackColor = Color.LightSteelBlue;
            button9.BackColor = Color.Thistle;
            button8.BackColor = Color.Thistle;
            button11.BackColor = Color.Thistle;
            Clean3bt();
            PopulateGrid("d4");
            dataGridView1.Enabled = true;
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string s = "";
                conn.Open();
                if (checkBox1.Checked == true)
                    label3.Text = "да";
                else
                    label3.Text = "нет";
                if (button8.BackColor == Color.LightCoral)
                    s = "d1";
                if (button9.BackColor == Color.LightGreen)
                    s = "d2";
                if (button11.BackColor == Color.SandyBrown)
                    s = "d3";
                if (button10.BackColor == Color.LightSteelBlue)
                    s = "d4";
                string sql = " INSERT INTO "+s+" VALUES('" + textBox1.Text + "','" + dateTimePicker1.Text + "','" + label3.Text + " ')"; 
                SqlCommand cmd = new SqlCommand(sql, conn);
                try
                {
                    if (textBox1.Text == "" )
                    {
                        button2.Enabled = false;
                        label1.Text = "нужно добавить задачу";
                    }
                    else
                        cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    MessageBox.Show("Вы не можете добавить задачу, если она пустая или не выбрана категория");
                }
                PopulateGrid(s);
                button2.Enabled = true;
                textBox1.Text = "";
                dateTimePicker1.Text = "";
                checkBox1.Checked = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Clean3bt();
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();        
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string s = "";
                conn.Open();
                if (button8.BackColor == Color.LightCoral)
                    s = "d1";
                if (button9.BackColor == Color.LightGreen)
                    s = "d2";
                if (button11.BackColor == Color.SandyBrown)
                    s = "d3";
                if (button10.BackColor == Color.LightSteelBlue)
                    s = "d4";
                string sql = " DELETE FROM "+s+" WHERE [ЗАДАЧА] = '" + id + "'";
                SqlCommand comm = new SqlCommand(sql, conn);
                try
                {
                        comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                        MessageBox.Show(ex.Message);
                }
                PopulateGrid(s);
            }
            
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            if (Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value) == "да")
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
            button4.Visible = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string s = "";
                conn.Open();
                if (button8.BackColor == Color.LightCoral)
                    s = "d1";
                if (button9.BackColor == Color.LightGreen)
                    s = "d2";
                if (button11.BackColor == Color.SandyBrown)
                    s = "d3";
                if (button10.BackColor == Color.LightSteelBlue)
                    s = "d4";
                if (checkBox1.Checked == true)
                    label3.Text = "да";
                else
                    label3.Text = "нет";
                string sql = "update " + s + " set ЗАДАЧА = '" + textBox1.Text + "' ," + " ДАТА = '" + dateTimePicker1.Text + "'," + "ГОТОВО = '" + label3.Text + "' WHERE [ЗАДАЧА] = '" + id+"'";
                SqlCommand comm = new SqlCommand(sql, conn);
                try
                {
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                PopulateGrid(s);
                button4.Visible = false;
                textBox1.Text = "";
                dateTimePicker1.Text = "";
                checkBox1.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string s = "";
                conn.Open();
                if (button8.BackColor == Color.LightCoral)
                    s = "d1";
                if (button9.BackColor == Color.LightGreen)
                    s = "d2";
                if (button11.BackColor == Color.SandyBrown)
                    s = "d3";
                if (button10.BackColor == Color.LightSteelBlue)
                    s = "d4";
                string sql =" update " + s + " set ГОТОВО = 'да' WHERE [ЗАДАЧА] = '" + id+"'";
                SqlCommand comm = new SqlCommand(sql, conn);
                try
                {
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                PopulateGrid(s);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dateTimePicker1.Text = "";
            checkBox1.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string s = "";
                conn.Open();
                if (button8.BackColor == Color.LightCoral)
                    s = "d1";
                if (button9.BackColor == Color.LightGreen)
                    s = "d2";
                if (button11.BackColor == Color.SandyBrown)
                    s = "d3";
                if (button10.BackColor == Color.LightSteelBlue)
                    s = "d4";
                string sql =" update " + s + " set ГОТОВО = 'нет' WHERE [ЗАДАЧА] = '" + id+"'";
                SqlCommand comm = new SqlCommand(sql, conn);
                try
                {
                    comm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                PopulateGrid(s);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            button5.Visible = true;
            button6.Visible = true;
            button3.Visible = true;
        }


        ToolTip t = new ToolTip();
        private void button8_MouseHover(object sender, EventArgs e)
        {
            t.SetToolTip(button8, "кризисные ситуации");
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            t.SetToolTip(button9, "работа на перспективу");
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            t.SetToolTip(button11, "помехи, чужие дела");
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            t.SetToolTip(button10, "мелочи, отнимающие время");
        }
    }
}
