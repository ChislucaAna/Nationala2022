using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace Nationala_2022
{
    public partial class Logare : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        StreamReader reader;
        SqlDataReader r;
        string val1, val2;
        public Logare()
        {
            InitializeComponent();
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\baza.mdf;Integrated Security=True");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //adaugam info in database si in combobox
            try
            {
                con.Open();
                reader = new StreamReader("Useri.txt");
                string nume, parola,line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] bucati = line.Split(' ');
                    nume = bucati[0];
                    parola = bucati[1];
                    comboBox1.Items.Add(nume);
                    cmd = new SqlCommand(String.Format("INSERT INTO Utilizatori VALUES ('{0}','{1}');", nume, parola), con);
                    cmd.ExecuteNonQuery();
                }
                reader.Close();
                reader = new StreamReader("Harta1.txt");
                while ((line = reader.ReadLine()) != null)
                {
                    string[] bucati = line.Split(' ');
                    cmd = new SqlCommand(String.Format("INSERT INTO Harti VALUES ('{0}',{1},{2});", bucati[0], bucati[1], bucati[2]), con);
                    cmd.ExecuteNonQuery();
                }
                reader.Close();
                reader = new StreamReader("Harta2.txt");
                while ((line = reader.ReadLine()) != null)
                {
                    string[] bucati = line.Split(' ');
                    cmd = new SqlCommand(String.Format("INSERT INTO Harti VALUES ('{0}',{1},{2});", bucati[0], bucati[1], bucati[2]), con);
                    cmd.ExecuteNonQuery();
                }
                reader.Close();
                reader = new StreamReader("Harta3.txt");
                while ((line = reader.ReadLine()) != null)
                {
                    string[] bucati = line.Split(' ');
                    cmd = new SqlCommand(String.Format("INSERT INTO Harti VALUES ('{0}',{1},{2});", bucati[0], bucati[1], bucati[2]), con);
                    cmd.ExecuteNonQuery();
                }
                reader.Close();
                con.Close();
            }
            catch(Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
             val2 = textBox1.Text;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(String.Format("SELECT * FROM Utilizatori WHERE nume='{0}' AND parola='{1}';", val1, val2), con);
                r = cmd.ExecuteReader();
                if (r.Read() == true)
                {
                    MessageBox.Show("AUTENTIFICARE REUSITA!");
                    this.Hide();
                    Interfeteeco callable = new Interfeteeco(val1, pictureBox3.Image);
                    callable.ShowDialog();
                }
                else
                {
                    MessageBox.Show("PAROLA GRESITA!");
                    textBox1.Text = "";
                }
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(String.Format("SELECT * FROM Utilizatori WHERE nume='{0}' AND parola='{1}';", val1, val2), con);
                r = cmd.ExecuteReader();
                if (r.Read() == true)
                {
                    MessageBox.Show("AUTENTIFICARE REUSITA!");
                    this.Hide();
                    Interfeteeco callable = new Interfeteeco(val1, pictureBox4.Image);
                    callable.ShowDialog();
                }
                else
                {
                    MessageBox.Show("PAROLA GRESITA!");
                    textBox1.Text = "";
                }
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(String.Format("SELECT * FROM Utilizatori WHERE nume='{0}' AND parola='{1}';", val1, val2), con);
                r = cmd.ExecuteReader();
                if (r.Read() == true)
                {
                    MessageBox.Show("AUTENTIFICARE REUSITA!");
                    this.Hide();
                    Interfeteeco callable = new Interfeteeco(val1, pictureBox5.Image);
                    callable.ShowDialog();
                }
                else
                {
                    MessageBox.Show("PAROLA GRESITA!");
                    textBox1.Text = "";
                }
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(String.Format("SELECT * FROM Utilizatori WHERE nume='{0}' AND parola='{1}';", val1, val2), con);
                r = cmd.ExecuteReader();
                if (r.Read() == true)
                {
                    MessageBox.Show("AUTENTIFICARE REUSITA!");
                    this.Hide();
                    Interfeteeco callable = new Interfeteeco(val1, pictureBox1.Image);
                    callable.ShowDialog();
                }
                else
                {
                    MessageBox.Show("PAROLA GRESITA!");
                    textBox1.Text = "";
                }
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand(String.Format("SELECT * FROM Utilizatori WHERE nume='{0}' AND parola='{1}';", val1, val2), con);
                r = cmd.ExecuteReader();
                if (r.Read() == true)
                {
                    MessageBox.Show("AUTENTIFICARE REUSITA!");
                    this.Hide();
                    Interfeteeco callable = new Interfeteeco(val1, pictureBox2.Image);
                    callable.ShowDialog();
                }
                else
                {
                    MessageBox.Show("PAROLA GRESITA!");
                    textBox1.Text = "";
                }
                con.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             val1 = comboBox1.SelectedItem.ToString();
        }
    }
}
