using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Physics;

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
        public float scale = 1;
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
                            if (Collisions.IntersectPolygons(
                                ((BoxBody)bodies[i]).GetTransformedVerticies(),
                                ((BoxBody)bodies[j]).GetTransformedVerticies(),
                                out Vector2 normal,
                                out float depth))
                            {
                                ((BoxBody)bodies[i]).Move(-normal.Scale((depth / 2.0)));
                                ((BoxBody)bodies[j]).Move(normal.Scale((depth / 2.0)));
                            }
                        }
                        if (bodies[j].GetType() == typeof(CircleBody))
                        {
                            if (Collisions.IntersectCirclePolygon(
                                ((CircleBody)bodies[j]).position,
                                (float)((CircleBody)bodies[j]).diameter / 2,
                                ((BoxBody)bodies[i]).GetTransformedVerticies(),
                                out Vector2 normal,
                                out float depth))
                            {
                                ((BoxBody)bodies[i]).Move(normal.Scale((depth / 2.0)));
                                ((CircleBody)bodies[j]).Move(-normal.Scale((depth / 2.0)));
                            }
                        }
                        if (bodies[j].GetType() == typeof(PolygonBody))
                        {
                            if (Collisions.IntersectPolygons(
                                ((BoxBody)bodies[i]).GetTransformedVerticies(),
                                ((PolygonBody)bodies[j]).GetTransformedVerticies(),
                                out Vector2 normal,
                                out float depth))
                            {
                                ((BoxBody)bodies[i]).Move(-normal.Scale((depth / 2.0)));
                                ((PolygonBody)bodies[j]).Move(normal.Scale((depth / 2.0)));
                            }
                        }
                    }
                }
                if (bodies[i].GetType() == typeof(CircleBody))
                {
                    for (int j = i + 1; j < bodies.Count; ++j)
                    {
                        if (bodies[j].GetType() == typeof(BoxBody))
                        {
                            if (Collisions.IntersectCirclePolygon(
                                ((CircleBody)bodies[i]).position,
                                (float)((CircleBody)bodies[i]).diameter / 2,
                                ((BoxBody)bodies[j]).GetTransformedVerticies(),
                                out Vector2 normal,
                                out float depth))
                            {
                                ((CircleBody)bodies[i]).Move(normal.Scale((depth / 2.0)));
                                ((BoxBody)bodies[j]).Move(-normal.Scale((depth / 2.0)));
                            }
                        }
                        if (bodies[j].GetType() == typeof(CircleBody))
                        {
                            Collisions.CirclesCollision((CircleBody)bodies[i], (CircleBody)bodies[j]);
                        }
                        if (bodies[j].GetType() == typeof(PolygonBody))
                        {
                            if (Collisions.IntersectCirclePolygon(
                                ((CircleBody)bodies[i]).position,
                                (float)((CircleBody)bodies[i]).diameter / 2,
                                ((PolygonBody)bodies[j]).GetTransformedVerticies(),
                                out Vector2 normal,
                                out float depth))
                            {
                                ((CircleBody)bodies[i]).Move(-normal.Scale((depth / 2.0)));
                                ((PolygonBody)bodies[j]).Move(normal.Scale((depth / 2.0)));
                            }
                        }
                    }
                }
                if (bodies[i].GetType() == typeof(PolygonBody))
                {
                    for (int j = i + 1; j < bodies.Count; ++j)
                    {
                        if (bodies[j].GetType() == typeof(BoxBody))
                        {
                            if (Collisions.IntersectPolygons(
                                ((BoxBody)bodies[j]).GetTransformedVerticies(),
                                ((PolygonBody)bodies[i]).GetTransformedVerticies(),
                                out Vector2 normal,
                                out float depth))
                            {
                                ((BoxBody)bodies[j]).Move(-normal.Scale((depth / 2.0)));
                                ((PolygonBody)bodies[i]).Move(normal.Scale((depth / 2.0)));
                            }
                        }
                        if (bodies[j].GetType() == typeof(CircleBody))
                        {
                            if (Collisions.IntersectCirclePolygon(
                                ((CircleBody)bodies[j]).position,
                                (float)((CircleBody)bodies[j]).diameter / 2,
                                ((PolygonBody)bodies[i]).GetTransformedVerticies(),
                                out Vector2 normal,
                                out float depth))
                            {
                                ((CircleBody)bodies[j]).Move(-normal.Scale((depth / 2.0)));
                                ((PolygonBody)bodies[i]).Move(normal.Scale((depth / 2.0)));
                            }
                        }
                        if (bodies[j].GetType() == typeof(PolygonBody))
                        {
                            if (Collisions.IntersectPolygons(
                                ((PolygonBody)bodies[i]).GetTransformedVerticies(),
                                ((PolygonBody)bodies[j]).GetTransformedVerticies(),
                                out Vector2 normal,
                                out float depth))
                            {
                                ((PolygonBody)bodies[i]).Move(-normal.Scale((depth / 2.0)));
                                ((PolygonBody)bodies[j]).Move(normal.Scale((depth / 2.0)));
                            }
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
                if (bodies[i].GetType() == typeof(PolygonBody))
                {
                    if (((PolygonBody)bodies[i]).IsInside(mouse - zero))
                    {
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
            if (e.KeyCode == Keys.Q)
            {
                scale += (float)0.1;
            }
            if (e.KeyCode == Keys.A)
            {
                scale -= (float)0.1;
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
            for (int i = 0; i < bodies.Count; ++i)
            {
                if (bodies[i].GetType() == typeof(BoxBody))
                {
                    Drawing.DrawBox(e, (BoxBody)bodies[i], zero, scale);
                }
                if (bodies[i].GetType() == typeof(CircleBody))
                {
                    Drawing.DrawCircle(e, (CircleBody)bodies[i], zero, scale);
                }
                if (bodies[i].GetType() == typeof(PolygonBody))
                {
                    label3.Text = ((PolygonBody)bodies[i]).VertciesToString();
                    Drawing.DrawPolygon(e, (PolygonBody)bodies[i], zero, scale);
                }
            }
            Physics.Drawing.DrawLine(e, new Vector2(0, 0), new Vector2(100, 100), new Pen(Color.White), zero, scale);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Vector2[] v1 = {
                new Vector2(100, 100),
                new Vector2(200, 100),
                new Vector2(250, 150),
                new Vector2(200, 200),
                new Vector2(100, 200),
            };
            Vector2[] v2 = {
                new Vector2(100, 400),
                new Vector2(200, 400),
                new Vector2(150, 500),
            };
            bodies.Add(new PolygonBody(1, new Vector2(100, 100), 1, 0, Color.Pink, v1));
            bodies.Add(new BoxBody(1, new Vector2(500, 100), 1, 0, Color.Pink, 100, 100));
            bodies.Add(new BoxBody(1, new Vector2(700, 100), 1, 0, Color.Gray, 100, 100));
            bodies.Add(new CircleBody(1, new Vector2(600, 300), 1, 0, Color.Pink, 50));
            bodies.Add(new CircleBody(1, new Vector2(700, 300), 1, 0, Color.Gray, 50));
            bodies.Add(new PolygonBody(1, new Vector2(100, 400), 1, 0, Color.Gray, v2));
        }

        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.X / scale; mouse.Y = e.Y / scale;
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
                            break;
                        }
                    }
                    if (body.GetType() == typeof(CircleBody))
                    {
                        if (((CircleBody)body).IsInside(mouse - zero))
                        {
                            body.MoveTo(mouse - zero);
                            break;
                        }
                    }
                    if (body.GetType() == typeof(PolygonBody))
                    {
                        if (((PolygonBody)body).IsInside(mouse - zero))
                        {
                            body.MoveTo(mouse - zero);
                            break;
                        }
                    }
                }
            }
        }
    }
}
