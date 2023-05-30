using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Physics;
using System.Reflection;
using System.Collections;

namespace EngineForm
{
    public partial class Form1 : Form
    {

        public static Vector2 zero = new Vector2(0, 0);
        public static Vector2 mouse = new Vector2(0, 0);
        public bool up;
        public bool down;
        public bool left;
        public bool right;
        public bool moving;
        public bool rotatingRight;
        public bool rotatingLeft;
        public List<Physics.Body> bodies = new List<Physics.Body>();

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (up)
            {
                zero.Y += 3;
            }
            if (down)
            {
                zero.Y -= 3;
            }
            if (left)
            {
                zero.X += 3;
            }
            if (right)
            {
                zero.X -= 3;
            }
            for (int i = 0; i < bodies.Count; ++i)
            {
                if (bodies[i].GetType() == typeof(BoxBody))
                {
                    for (int j = i + 1; j < bodies.Count; ++j)
                    {
                        if (bodies[j].GetType() == typeof(BoxBody))
                        {
                            if (Collisions.IntersectPolygons(((BoxBody)bodies[j]).GetTransformedVerticies(), ((BoxBody)bodies[i]).GetTransformedVerticies()))
                            {
                                ((BoxBody)bodies[i]).color = Color.Red;
                                break;
                            }
                            else
                            {
                                ((BoxBody)bodies[i]).color = Color.Blue;
                            }
                        }
                        if (bodies[j].GetType() == typeof(CircleBody))
                        {
                            continue;
                        }
                    }
                }
                if (bodies[i].GetType() == typeof(CircleBody))
                {
                    for (int j = i + 1; j < bodies.Count; ++j)
                    {
                        if (bodies[j].GetType() == typeof(BoxBody))
                        {
                            continue;
                        }
                        if (bodies[j].GetType() == typeof(CircleBody))
                        {
                            Collisions.CirclesCollision((CircleBody)bodies[i], (CircleBody)bodies[j]);
                            continue;
                        }
                    }
                }
            }
            for (int i = 0; i < bodies.Count; ++i)
            {
                if (bodies[i].GetType() == typeof(BoxBody))
                {
                    if (((BoxBody)bodies[i]).IsInside(mouse - zero))
                    {
                        label2.Text = ((BoxBody)bodies[i]).GetTransformedVerticies()[0].ToString() + ((BoxBody)bodies[i]).GetTransformedVerticies()[1].ToString() + ((BoxBody)bodies[i]).GetTransformedVerticies()[2].ToString() + ((BoxBody)bodies[i]).GetTransformedVerticies()[3].ToString();

                        if (rotatingRight == true)
                        {
                            bodies[i].rotaion++;
                            break;
                        }
                        if (rotatingLeft == true)
                        {
                            bodies[i].rotaion--;
                            break;
                        }
                    }
                }
            }
            Refresh();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                up = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                down = true;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
            if (e.KeyCode == Keys.Space)
            {
                moving = true;
            }
            if (e.KeyCode == Keys.R)
            {
                rotatingRight = true;
            }
            if (e.KeyCode == Keys.E)
            {
                rotatingLeft = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                up = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                down = false;
            }
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
            if (e.KeyCode == Keys.Space)
            {
                moving = false;
            }
            if (e.KeyCode == Keys.R)
            {
                rotatingRight = false;
            }
            if (e.KeyCode == Keys.E)
            {
                rotatingLeft = false;
            }
        }

        private void MainScreen_Paint(object sender, PaintEventArgs e)
        {
            for(int i = bodies.Count() - 1; i >= 0; --i)
            {
                if (bodies[i].GetType() == typeof(BoxBody))
                {
                    Drawing.DrawBox(e, (BoxBody)bodies[i], zero);
                }
                if (bodies[i].GetType() == typeof(CircleBody))
                {
                    Drawing.DrawCircle(e, (CircleBody)bodies[i], zero);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bodies.Add(new BoxBody(1, new Vector2(100, 100), 1, 0, Color.Pink, 100, 100));
            bodies.Add(new BoxBody(1, new Vector2(300, 300), 1, 0, Color.Gray, 100, 100));
            bodies.Add(new BoxBody(1, new Vector2(500, 500), 1, 0, Color.Gray, 100, 100));
            //bodies.Add(new CircleBody(1, new Vector2(600, 300), 1, 0, Color.Blue, 50));
            //bodies.Add(new CircleBody(1, new Vector2(700, 300), 1, 0, Color.Red, 50));
            //bodies.Add(new CircleBody(1, new Vector2(800, 300), 1, 0, Color.Red, 50));
        }

        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.X; mouse.Y = e.Y;
            label1.Text = mouse.ToString();
            if (moving == true)
            {
                foreach (Physics.Body body in bodies)
                {
                    if (body.GetType() == typeof(BoxBody))
                    {
                        if (((BoxBody)body).IsInside(mouse - zero))
                        {
                            body.MoveTo(mouse - zero);
                            ToFront(bodies, body);
                            break;
                        }
                    }
                    if (body.GetType() == typeof(CircleBody))
                    {
                        if (((CircleBody)body).IsInside(mouse - zero))
                        {
                            body.MoveTo(mouse - zero);
                            ToFront(bodies, body);
                            break;
                        }
                    }
                }
            }
        }

        public void ToFront(List<Physics.Body> bodies, Physics.Body body)
        {
            int index = bodies.FindIndex(a => a == body);
            Physics.Body item = bodies[index];
            bodies.RemoveAt(index);
            bodies.Insert(0, item);
        }
    }
}
