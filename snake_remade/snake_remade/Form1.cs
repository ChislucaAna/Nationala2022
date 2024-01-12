using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace snake_remade
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        public class piece
        {
            public int x;
            public int y;
            public int dir;

            public piece(int pozx, int pozy, int direction)
            {
                x = pozx;
                y = pozy;
                dir = direction;
            }
        }

        Random rnd = new Random();
        piece[] snake = new piece[100];
        int cnt = 0;

        public class food
        {
            public int x;
            public int y;

            public food(int pozx, int pozy)
            {
                x = pozx;
                y = pozy;
            }
        }

        food ball;

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.W) //AICI AI RAMAS
            {
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //generate your first snake piece
            int pozx = rnd.Next(30, this.Width - 30);
            int pozy = rnd.Next(30, this.Width - 30);
            int direction = rnd.Next(0, 4);
            piece aux = new piece(pozx, pozy, direction);
            snake[0] = aux;
            cnt++;

            //generate food
            int x = rnd.Next(30, this.Width - 30);
            int y = rnd.Next(30, this.Width - 30);
            ball = new food(x, y);

            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //paint food 
            Pen pen = new Pen(Color.Black);
            SolidBrush red = new SolidBrush(Color.Red);
            SolidBrush green = new SolidBrush(Color.Green);
            Rectangle food = new Rectangle(ball.x, ball.y, 15, 15);

            e.Graphics.DrawEllipse(pen, food);
            e.Graphics.FillEllipse(red, food);
            //pictezi snakeul
            for (int i = 0; i < cnt; i++)
            {
                if (snake[0] != null)
                {
                    Rectangle ball = new Rectangle(snake[i].x, snake[i].y, 15, 15);
                    e.Graphics.DrawEllipse(pen, ball);
                    e.Graphics.FillEllipse(green, ball);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //verifici daca snake=ul intersecteaza unul din peretii formului
            for (int i = 0; i < cnt; i++)
            {
                if (snake[0] != null)
                {
                    if (snake[i].x < 10 || snake[i].x > Form1.ActiveForm.Width - 10 || snake[i].y < 10 || snake[i].x > Form1.ActiveForm.Height - 10)
                    {
                        MessageBox.Show("AI PIERDUT");
                        Thread.Sleep(200);
                        Application.Exit();
                    }
                }
            }
            //verifici daca snake-ul intersecteaza mancarea cu capul
            Rectangle cap = new Rectangle(snake[0].x, snake[0].y, 15, 15);
            Rectangle food = new Rectangle(ball.x, ball.y, 15, 15);
            if(cap.IntersectsWith(food))
            {
                //regenerezi mancarea int-un loc random
                int x = rnd.Next(30, this.Width - 30);
                int y = rnd.Next(30, this.Width - 30);
                ball = new food(x, y);

                //ADAUGI INCA O BUCATA LA CAPATUL SNAKEULUI,la anumite coordonate in functie de directia sa
                //dir==0 => se misca in jos
                //dir==1 => se misca in sus
                //dir==2 => se misca spre dreapta
                //dir==3 => se misca spre stanga
                if (snake[cnt-1].dir==0)
                {
                    piece val = new piece(snake[cnt - 1].x, snake[cnt - 1].y - 15, snake[cnt-1].dir);
                    snake[cnt] = val;
                    cnt++;
                }
                if (snake[cnt - 1].dir == 1)
                {
                    piece val = new piece(snake[cnt - 1].x, snake[cnt - 1].y + 15, snake[cnt - 1].dir);
                    snake[cnt] = val;
                    cnt++;
                }
                if (snake[cnt - 1].dir == 2)
                {
                    piece val = new piece(snake[cnt - 1].x-15, snake[cnt - 1].y, snake[cnt - 1].dir);
                    snake[cnt] = val;
                    cnt++;
                }
                if (snake[cnt - 1].dir == 3)
                {
                    piece val = new piece(snake[cnt - 1].x+15, snake[cnt - 1].y, snake[cnt - 1].dir);
                    snake[cnt] = val;
                    cnt++;
                }
            }
            pictureBox1.Refresh();
        }

    }
}
