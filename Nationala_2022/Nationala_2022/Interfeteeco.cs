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
using System.Threading;

namespace Nationala_2022
{
    public partial class Interfeteeco : Form
    {
        public Interfeteeco(string nume, Image image)
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.Text += "-";
            this.Text += nume;
            pictureBox1.Image = image;
            copy = image;
        }

        Image copy;
        int grid = 0;
        int l,i;
        int x,y;
        int pozx, pozy;
        string file;
        string line;
        int[,] m = new int[21, 11];
        int triunghi = 0;
        int selectat = 0;
        PictureBox hoover;
        int xr, yr; //coordonate robot
        int started = 0;
        int dir;
        StreamReader reader;

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Image current = pictureBox2.Image;
            hoover = new PictureBox();
            Interfeteeco.ActiveForm.Controls.Add(hoover);
            hoover.Image = current;
            hoover.Width = l;
            hoover.Height = i;
            hoover.SizeMode = PictureBoxSizeMode.StretchImage;
            hoover.BringToFront();

            Interfeteeco.ActiveForm.Refresh();
            selectat = 1;
            /*while (selectat == 1) //HOOVER
            {
                hoover.Location = new Point(e.X,e.Y);
                Interfeteeco.ActiveForm.Refresh();
                Thread.Sleep(2000);
            }*/
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            //adaugi imaginea in celula respectiva DACA E LIBERA .selectat devine iar 0
            if (selectat == 1)
            {
                int px = e.X / l + 1;
                int py = e.Y / i + 1;
                if (m[px, py] == 0)
                {
                    //ai nevoie de o aproximare ca deflectorul sa-ti pcie fix in celula
                    hoover.Location = new Point((px-1)*l, (py-1)*i);
                    selectat = 0;
                    if (triunghi == 0)
                    {
                        m[px, py] = 2;
                    }
                    if (triunghi == 1)
                    {
                        m[px, py] = 3;
                    }
                    if (triunghi == 2)
                    {
                        m[px, py] = 4;
                    }
                    if (triunghi == 3)
                    {
                        m[px, py] = 5;
                    }
                }
            }
        }

        private void Interfeteeco_Load(object sender, EventArgs e)
        {
            l = pictureBox1.Width / 20;
            x = l;
            i = pictureBox1.Height / 10;
            y = i;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(triunghi==0)
            {
                triunghi = 1;
                pictureBox2.Image = Image.FromFile("Dreapta-Sus.png");
            }
            else
            {
                if(triunghi==1)
                {
                    triunghi = 2;
                    pictureBox2.Image = Image.FromFile("Stanga-Jos.png");
                }
                else
                {
                    if (triunghi == 2)
                    {
                        triunghi = 3;
                        pictureBox2.Image = Image.FromFile("Stanga-Sus.png");
                    }
                    else
                    {
                        triunghi = 0;
                        pictureBox2.Image = Image.FromFile("Dreapta-Jos.png");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Text = "stop";
            MessageBox.Show("apasati una din tastele WASD pentru a selecta directia de pornire a robotului");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(dir==1)
            {
                yr -= i;
            }
            if(dir==2)
            {
                xr -= l;
            }
            if(dir==-1)
            {
                yr += i;
            }
            if(dir==-2)
            {
                xr += l;
            }
            //verifici daca acesta intersecteaza vreun deflector
            int px = xr / l + 1;
            int py = yr / i + 1;
            if(m[px,py]==2) //dreapta jos
            {
                if(dir==-1)
                {
                    dir = -2;
                }
                else
                {
                    if(dir==-2)
                    {
                        dir = -1;
                    }
                    else //ricoseu
                    {
                        dir = (-1) * dir;
                    }
                }
            }
            if(m[px,py]==3) //dreapta sus
            {
                if (dir == -2)
                {
                    dir = 1;
                }
                else
                {
                    if (dir == 1)
                    {
                        dir = -2;
                    }
                    else //ricoseu
                    {
                        dir = (-1) * dir;
                    }
                }
            }
            if(m[px,py]==4) //stanga jos
            {
                if (dir == -1)
                {
                    dir = 2;
                }
                else
                {
                    if (dir == 2)
                    {
                        dir = -1;
                    }
                    else //ricoseu
                    {
                        dir = (-1) * dir;
                    }
                }
            } //stanga sus
            else
            {
                if (dir == 1)
                {
                    dir = 2;
                }
                else
                {
                    if (dir == 2)
                    {
                        dir = 1;
                    }
                    else //ricoseu
                    {
                        dir = (-1) * dir;
                    }
                }
            }
        }


        private void Interfeteeco_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Start();
            if (e.KeyCode == Keys.W && started ==0)
            {
                started = 1;
                dir = 1; //sus
               // MessageBox.Show("sUS");
            }
            if (e.KeyCode == Keys.A && started == 0)
            {
                started = 1;
                dir = 2; //stanga
                //MessageBox.Show("sTANGA");
            }
            if (e.KeyCode == Keys.S && started == 0)
            {
                started = 1;
                dir = -1; //jos
                //MessageBox.Show("jos");
            }
            if (e.KeyCode == Keys.D && started == 0)
            {
                started = 1;
                dir = -2; //dreapta
                //MessageBox.Show("dreapta");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Title = "SELECTATI UNUL DIN FISIERRELE CREEATE DE ECOLOGISTI";
            d.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (d.ShowDialog() == DialogResult.OK)
            {
                file = d.FileName;
                //stream reader
                try
                {
                    reader = new StreamReader(file);
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] bucati = line.Split(' ');
                        PictureBox pic = new PictureBox();
                        Interfeteeco.ActiveForm.Controls.Add(pic);
                        string path = Path.GetFullPath(bucati[0]);
                        path += ".png";
                        pic.Image = Image.FromFile(path);

                        pic.Width = l;
                        pic.Height = i;
                        pic.SizeMode = PictureBoxSizeMode.StretchImage;
                        pozx = Convert.ToInt32(bucati[1]);
                        pozy = Convert.ToInt32(bucati[2]);
                        pic.Location = new Point((pozx-1)*l, (pozy-1)*i);
                        m[pozx, pozy] = 1;
                        pic.BringToFront();

                        Interfeteeco.ActiveForm.Refresh();
                        if(String.Compare(bucati[0],"Robot")==0)
                        {
                            xr = pozx;
                            yr = pozy;
                        }
                    }
                }
                catch(Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //matriciile au dimensiune de 20x10
            if(checkBox1.Checked==true) //afisezi linii de grid
            {
                
                grid = 1;
                
            }
            else //stergi linii de grid
            {
                grid = 0;
            }
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if(grid==1)
            {
                x = 0;
                y = 0;
                //imparti latimea in 20
                for (int cnt = 1; cnt <= 20; cnt++)
                {
                    Pen blackPen = new Pen(Color.White, 3);

                    // Create points that define line.
                    PointF point1 = new PointF(x, 0);
                    PointF point2 = new PointF(x, pictureBox1.Height);
                    x += l;

                    e.Graphics.DrawLine(blackPen, point1, point2);
                }
                //imparti inaltimea in 10
                for (int cnt = 1; cnt <= 10; cnt++)
                {
                    Pen blackPen = new Pen(Color.White, 3);

                    // Create points that define line.
                    PointF point1 = new PointF(0,y);
                    PointF point2 = new PointF(pictureBox1.Width,y);
                    y += i;

                    e.Graphics.DrawLine(blackPen, point1, point2);
                }
            }
            else
            {
                //e.Graphics.Clear(Color.White);
                pictureBox1.Image = copy;
            }
        }
    }
}
