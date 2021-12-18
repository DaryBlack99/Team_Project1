using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        private static string connect = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Data.mdb;";
        private OleDbConnection connection = new OleDbConnection(connect);
        private OleDbDataAdapter adapter = null;
        private DataSet dataSet = null;
        private DataTable table = null;
        int id=1;
        public Form2()
        {
            InitializeComponent();
            connection.Open();
        }

        private void dataup()
        {
            dataSet.Tables["Студенты"].Clear();
            adapter.Fill(dataSet, "Студенты");
        }
        private void Initialization()
        {
            try
            {
                textBox1.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
                textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
                textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
                textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[4].Value);
                id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                textBox5.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);
            }
            catch
            {
                MessageBox.Show("Данного элемента нет!");
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            adapter = new OleDbDataAdapter("SELECT * FROM Студенты", connection);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "Студенты");
            table = dataSet.Tables["Студенты"];
            dataGridView1.DataSource = table;
            Initialization();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Initialization();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string name = textBox1.Text.ToString();
                int teacher = int.Parse(textBox2.Text);
                int group = int.Parse(textBox3.Text);
                int points = int.Parse(textBox4.Text);
                OleDbCommand update = connection.CreateCommand();
                update.CommandText = $"UPDATE [Студенты] SET [ФИО]='{name}' ,[ID Преподавателя]='{teacher}' ,[ID Группы]='{group}' ,[Балл]='{points}' WHERE ID={id}";
                update.ExecuteNonQuery();
                dataup();
            }
            catch
            {
                MessageBox.Show("Неверный формат данных!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OleDbCommand delete = connection.CreateCommand();
            delete.CommandText = $"DELETE FROM [Студенты] WHERE [ID] = {id}";
            delete.ExecuteNonQuery();
            dataup();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int ID = dataGridView1.Rows.Count;
                string name = textBox6.Text.ToString();
                int teacher = int.Parse(textBox7.Text);
                int group = int.Parse(textBox8.Text);
                int points = int.Parse(textBox9.Text);
                OleDbCommand Insert = connection.CreateCommand();
                Insert.CommandText = $"INSERT INTO [Студенты] ([ID],[ФИО],[ID Преподавателя],[ID Группы],[Балл]) VALUES('{ID}','{name}','{teacher}','{group}','{points}')";
                Insert.ExecuteNonQuery();
                dataup();
                textBox6.Text ="";
                textBox7.Text ="";
                textBox8.Text ="";
                textBox9.Text ="";
            }
            catch
            {
                MessageBox.Show("Неверный формат данных!");
            }
        }
    }
}
