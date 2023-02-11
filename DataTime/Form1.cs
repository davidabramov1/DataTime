using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;



namespace DataTime
{
    public partial class Form1 : Form 
    {

        public Form1()
        {
            InitializeComponent();
            select();
        }
        string connStr = "server=192.168.25.23;port=33333;user=st_1_20_1;database=is_1_20_st1_KURS;password=56064531;";
        MySqlConnection conn;
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new MySqlConnection(connStr);
            select();
        }

        public void select()
        {
            DataSet ds;
            ds = new DataSet();
            string connectionString = "server=192.168.25.23;port=33333;user=st_1_20_1;database=is_1_20_st1_KURS;password=56064531";
            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand command = new MySqlCommand();
            string commandString = "SELECT * FROM T_DataTime";
            command.CommandText = commandString;
            command.Connection = connection;
            MySqlDataReader reader;
            try
            {
                command.Connection.Open();
                reader = command.ExecuteReader();
                this.dataGridView1.Columns.Add("ID", "Номер");
                this.dataGridView1.Columns["ID"].Width = 90;
                this.dataGridView1.Columns.Add("Name", "Название");
                this.dataGridView1.Columns["Name"].Width = 165;
                this.dataGridView1.Columns.Add("Price", "Цена");
                this.dataGridView1.Columns["Price"].Width = 80;
                this.dataGridView1.Columns.Add("Data", "Срок годности");
                this.dataGridView1.Columns["Data"].Width = 80;
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader["ID"].ToString(), reader["Name"].ToString(), reader["Price"].ToString(), reader["Data"].ToString());
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: \r\n{0}", ex.ToString());
            }
            finally
            {
                command.Connection.Close();
            }
        
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "INSERT INTO `T_DataTime`(`ID, `Name`,`Price`, `Data`)";
            if (textBox3.Text == "")
            {
                MessageBox.Show("Введите номер", "Ошибка");
                return;

            }

            if (textBox2.Text == "")

                if (textBox1.Text == "")
            {
                MessageBox.Show("Введите Имя", "Ошибка");
                return;

            }
            if (textBox2.Text == "")
            {
                MessageBox.Show("Введите Цену", "Ошибка");
                return;

            }
            if (dateTimePicker1.Text == "")
            {
                MessageBox.Show("Введите срок годности", "Ошибка");
                return;

            }

            /*
            if (isUsersExists())
            {
                return;
            }
            */
            //   DataTable table = new DataTable();
            //  MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("INSERT INTO `T_DataTime`(`ID`,`Name`, `Price`,`Data`) VALUES (@ID, @Name, @Price, @Data)", conn);

            conn.Open();
            command.Parameters.Add("@ID", MySqlDbType.Int32, 25).Value = textBox3.Text;
            command.Parameters.Add("@Name", MySqlDbType.VarChar, 25).Value = textBox1.Text;
            command.Parameters.Add("@Price", MySqlDbType.VarChar, 25).Value = textBox2.Text;
            command.Parameters.Add("@Data", MySqlDbType.VarChar, 25).Value = dateTimePicker1.Text;
            //     adapter.SelectCommand = command;
            //  adapter.Fill(table);
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Вы успешно Добавили");
            }
            else
            {
                MessageBox.Show("Произошла ошибка, аккаунт не был создан");
            }
            conn.Close();
        }
    }
}
