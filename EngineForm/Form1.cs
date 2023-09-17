using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Physics;
using Physics.Bodies;
using System.Runtime.InteropServices;

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
        public List<Body> bodies = new List<Body>();
        public World world = new World();
        public Vector2 force = new Vector2(0, 0);

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
            if(force.X != 0 || force.Y != 0)
            {
                world.bodies[0].ApplyForce(force);
                force = new Vector2(0, 0);
            }
            world.UpdateWorld();
            for (int i = 0; i < world.bodies.Count; ++i)
            {
                if (world.bodies[i].GetType() == typeof(PolygonBody))
                {
                    if (((PolygonBody)world.bodies[i]).IsInside(mouse - zero))
                    {
                        if (rotatingRight == true)
                        {
                            world.bodies[i].rotaion++;
                            break;
                        }
                        if (rotatingLeft == true)
                        {
                            world.bodies[i].rotaion--;
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
            if (e.KeyCode == Keys.Oemplus)
            {
                scale += (float)0.1;
            }
            if (e.KeyCode == Keys.OemMinus)
            {
                scale -= (float)0.1;
            }
            if (e.KeyCode == Keys.W)
            {
                force.Y -= 2;
            }
            if (e.KeyCode == Keys.S)
            {
                force.Y += 2;
            }
            if (e.KeyCode == Keys.A)
            {
                force.X -= 2;
            }
            if (e.KeyCode == Keys.D)
            {
                force.X += 2;
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
            world.DrawWorld(e, zero, scale);
            Physics.Drawing.DrawLine(e, new Vector2(0, 0), new Vector2(100, 100), new Pen(Color.White), zero, scale);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            AllocConsole();
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
            world.AddBody(BodyFactory.CreatePolygonBody(1, 1, new Vector2(100, 400), 1, 0, Color.Gray, v2));
            world.AddBody(BodyFactory.CreateBoxBody(1, 1, new Vector2(500, 100), 1, 0, Color.Pink, 100, 100));
            world.AddBody(BodyFactory.CreateBoxBody(1, 1, new Vector2(700, 100), 1, 0, Color.Gray, 100, 100));
            world.AddBody(BodyFactory.CreateCircleBody(1, 1, new Vector2(600, 300), 1, 0, Color.Pink, 50));
            world.AddBody(BodyFactory.CreateCircleBody(1, 1, new Vector2(700, 300), 1, 0, Color.Gray, 50));
            world.AddBody(BodyFactory.CreatePolygonBody(1, 1, new Vector2(100, 100), 1, 0, Color.Pink, v1));
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        private void MainScreen_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.X = e.X / scale; mouse.Y = e.Y / scale;
            if (moving == true)
            {
                foreach (Body body in world.bodies)
                {
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
