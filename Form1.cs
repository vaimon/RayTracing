using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RayTracing
{
    public enum SelectedItem { BackWall, RightWall, LeftWall, Cube, SphereOnCube, SphereOnGround}
    public partial class Form1 : Form
    {
        RayTracing rayTracing;

        // ========================== SCHEME DRAWING ==========================
        Rectangle backWallScheme;
        Rectangle cubeScheme;
        Rectangle sphereOnCubeScheme;
        Rectangle sphereOnGroundScheme;
        
        Pen standardPen = new Pen(Color.Black, 2);
        Pen highlightPen = new Pen(Color.WhiteSmoke, 4);
        Graphics g;
        // ====================================================================

        SelectedItem currentItemType;
        Shape currentItem;

        Cube cube;
        Sphere sphereOnCube;
        Sphere sphereOnGround;
        Face leftWall;
        Face rightWall;
        Face backWall;

        public Form1()
        {
            InitializeComponent();
            canvas.Image = new Bitmap(canvas.Width,canvas.Height);
            g = Graphics.FromImage(canvas.Image);

            var center = new Point(0, 0, 9);
            double roomSide = 20;

            sphereOnGround = new Sphere(new Point(5, -8, 17), 4, Color.DarkSalmon);
            cube = new Cube(new Point(-5,-7,16), 6, Color.DarkRed);
            sphereOnCube = new Sphere(new Point(-5, 0, 17), 2, Color.DarkGreen);
            leftWall = new Face(new Point(center.x - roomSide / 2, center.y, center.z), new Vector(1, 0, 0), new Vector(0, 1, 0), roomSide, roomSide, Color.IndianRed);
            rightWall = new Face(new Point(center.x + roomSide / 2, center.y, center.z), new Vector(-1, 0, 0), new Vector(0, 1, 0), roomSide, roomSide, Color.Navy);
            backWall = new Face(new Point(center.x, center.y, center.z + roomSide / 2), new Vector(0, 0, -1), new Vector(0, 1, 0), roomSide, roomSide, Color.Gray);


            rayTracing = new RayTracing(new LightSource(new Point(0, 9, 9), 1.5), new Room(center, roomSide,leftWall,rightWall,backWall));
            rayTracing.renderProgress += updateProgress;

            backWallScheme = new Rectangle(canvas.Width / 2 - 100, canvas.Height / 2 - 100, 200, 200);
            cubeScheme = new Rectangle(canvas.Width / 2 - 125, canvas.Height / 2 + 25, 100, 100);
            sphereOnCubeScheme = new Rectangle(canvas.Width / 2 - 100, canvas.Height / 2 - 25, 50, 50);
            sphereOnGroundScheme = new Rectangle(canvas.Width / 2 + 25, canvas.Height / 2 + 40, 100, 100);
            currentItemType = SelectedItem.BackWall;
            currentItem = backWall;
            redrawScheme();
        }

        

        void redrawScheme()
        {
            g.Clear(Color.Gray);
            Pen backwallPen = standardPen, rightwallPen = standardPen, leftwallPen = standardPen, sphereOnCubePen = standardPen, sphereOnGroundPen = standardPen, cubePen = standardPen;
            switch (currentItemType)
            {
                case SelectedItem.BackWall: backwallPen = highlightPen; break;
                case SelectedItem.Cube: cubePen = highlightPen; break;
                case SelectedItem.LeftWall: leftwallPen = highlightPen; break;
                case SelectedItem.RightWall: rightwallPen = highlightPen; break;
                case SelectedItem.SphereOnCube: sphereOnCubePen = highlightPen; break;
                case SelectedItem.SphereOnGround: sphereOnGroundPen = highlightPen; break;
            }

            g.FillRectangle(new SolidBrush(backWall.color), backWallScheme);
            g.DrawLine(backwallPen, backWallScheme.Location, new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y));
            g.DrawLine(backwallPen, backWallScheme.Location, new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y + backWallScheme.Height));
            g.DrawLine(backwallPen, new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y), new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y + backWallScheme.Height));
            g.DrawLine(backwallPen, new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y + backWallScheme.Height), new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y + backWallScheme.Height));

            g.FillPolygon(new SolidBrush(leftWall.color), new PointF[] { new System.Drawing.Point(0, 0), backWallScheme.Location, new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y + backWallScheme.Height), new System.Drawing.Point(0, canvas.Height) });
            g.DrawLine(leftwallPen, backWallScheme.Location, new System.Drawing.Point(0,0));
            g.DrawLine(leftwallPen, new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y + backWallScheme.Height), new System.Drawing.Point(0, canvas.Height));
            if(currentItemType == SelectedItem.LeftWall)
            {
                g.DrawLine(leftwallPen, backWallScheme.Location, new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y + backWallScheme.Height));
            }

            g.FillPolygon(new SolidBrush(rightWall.color), new PointF[] { new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y), new System.Drawing.Point(canvas.Width, 0), new System.Drawing.Point(canvas.Width, canvas.Height), new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y + backWallScheme.Height) });
            g.DrawLine(rightwallPen, new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y), new System.Drawing.Point(canvas.Width,0));
            g.DrawLine(rightwallPen, new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y + backWallScheme.Height), new System.Drawing.Point(canvas.Width, canvas.Height));
            if(currentItemType == SelectedItem.RightWall)
            {
                g.DrawLine(rightwallPen, new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y), new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y + backWallScheme.Height));
            }

            

            g.FillRectangle(new SolidBrush(cube.color), cubeScheme);
            g.DrawRectangle(cubePen, cubeScheme);

            g.FillEllipse(new SolidBrush(sphereOnCube.color), sphereOnCubeScheme);
            g.DrawEllipse(sphereOnCubePen, sphereOnCubeScheme);

            g.FillEllipse(new SolidBrush(sphereOnGround.color), sphereOnGroundScheme);
            g.DrawEllipse(sphereOnGroundPen, sphereOnGroundScheme);

            g.FillEllipse(new SolidBrush(Color.Gold), new Rectangle(canvas.Width / 2 - 11, 50, 25, 7));

            canvas.Invalidate();
        }

        // Спасибо лабе по ИС за реализацию такой же штуки
        public void updateProgress(double progress, TimeSpan elapsedTime)
        {
            if (progressBar.InvokeRequired)
            {
                progressBar.Invoke(new RenderProgressHandler(updateProgress), progress, elapsedTime);
                return;
            }
            labelTime.Text = $"Время: {elapsedTime.Duration().ToString(@"hh\:mm\:ss\:ff")}";
            progressBar.Value = (int)Math.Round(progress * 100);
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            labelTime.Visible = true;
            progressBar.Visible = true;
            rayTracing.addShape(sphereOnCube);
            //rayTracing.addShape(new Face(new Point(-5, -7 + 3, 16), new Vector(0, 1, 0), new Vector(0, 0, 1), 6, 6, Color.DarkRed));
            rayTracing.addShape(cube);
            rayTracing.addShape(sphereOnGround);
            runAsyncComputation();
        }

        async void runAsyncComputation()
        {
            try
            {
                var bitmap = await Task.Run(() => rayTracing.compute(new Size(640,480)));
                var form = new FormResult(bitmap);
                form.Show();
            } catch (Exception e)
            {
                labelTime.Text = "HAHA exception";
            }
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (cubeScheme.Contains(e.Location))
            {
                currentItemType = SelectedItem.Cube;
                currentItem = cube;
            }else if (sphereOnCubeScheme.Contains(e.Location))
            {
                currentItemType = SelectedItem.SphereOnCube;
                currentItem = sphereOnCube;
            } else if (sphereOnGroundScheme.Contains(e.Location))
            {
                currentItemType = SelectedItem.SphereOnGround;
                currentItem = sphereOnGround; 
            } else if (backWallScheme.Contains(e.Location))
            {
                currentItemType = SelectedItem.BackWall;
                currentItem = backWall;
            }
            else if(e.Location.X < backWallScheme.Location.X)
            {
                currentItemType = SelectedItem.LeftWall;
                currentItem = leftWall;
            }
            else if (e.Location.X > backWallScheme.Location.X + backWallScheme.Width)
            {
                currentItemType = SelectedItem.RightWall;
                currentItem = rightWall;
            }

            redrawScheme();
        }

        private void buttonChangeColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = currentItem.color;
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                currentItem.color = colorDialog1.Color;
                redrawScheme();
            }
        }
    }
}
