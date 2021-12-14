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

        Rectangle backWall;
        Rectangle cube;
        Rectangle sphereOnCube;
        Rectangle sphereOnGround;
        

        Pen standardPen = new Pen(Color.Black, 2);
        Pen highlightPen = new Pen(Color.DarkRed, 4);
        Graphics g;

        SelectedItem currentItem;

        public Form1()
        {
            InitializeComponent();
            canvas.Image = new Bitmap(canvas.Width,canvas.Height);
            g = Graphics.FromImage(canvas.Image);
            rayTracing = new RayTracing(new LightSource(new Point(0,10,10),1.5), new Room(new Point(0,0,9),20, Color.Gray));
            rayTracing.renderProgress += updateProgress;
            rayTracing.addShape(new Sphere(new Point(5, -8, 17), 4, Color.DarkSalmon));
            //rayTracing.addShape(new Sphere(new Point(-1, -1.5, 12), 3, Color.DarkRed));
            rayTracing.addShape(new Cube(new Point(-5,-6,17), 6, Color.DarkRed));
            rayTracing.addShape(new Sphere(new Point(-5, -1, 17), 2, Color.DarkGreen));
            //rayTracing.addShape(new Face(new Point(4, 0, 6), new Vector(1, 0, 0), new Vector(0, 1, 0), 4, 4, Color.DarkRed));
            //rayTracing.addShape(new Face(new Point(0, 0, 6), new Vector(0, 0, -1), new Vector(0, 1, 0), 4, 4, Color.DarkRed));
            //rayTracing.addShape(new Sphere(new Point(0, 0, 6), 2, Color.DarkRed));
            //rayTracing.addShape(new Sphere(new Point(7, 5, 18), 4, Color.DarkBlue));

            backWall = new Rectangle(canvas.Width / 2 - 100, canvas.Height / 2 - 100, 200, 200);
            cube = new Rectangle(canvas.Width / 2 - 125, canvas.Height / 2 + 25, 100, 100);
            sphereOnCube = new Rectangle(canvas.Width / 2 - 100, canvas.Height / 2 - 25, 50, 50);
            sphereOnGround = new Rectangle(canvas.Width / 2 + 25, canvas.Height / 2 + 40, 100, 100);
            currentItem = SelectedItem.BackWall;
            redrawScheme();
        }

        

        void redrawScheme()
        {
            g.Clear(SystemColors.Control);
            Pen backwallPen = standardPen, rightwallPen = standardPen, leftwallPen = standardPen, sphereOnCubePen = standardPen, sphereOnGroundPen = standardPen, cubePen = standardPen;
            switch (currentItem)
            {
                case SelectedItem.BackWall: backwallPen = highlightPen; break;
                case SelectedItem.Cube: cubePen = highlightPen; break;
                case SelectedItem.LeftWall: leftwallPen = highlightPen; break;
                case SelectedItem.RightWall: rightwallPen = highlightPen; break;
                case SelectedItem.SphereOnCube: sphereOnCubePen = highlightPen; break;
                case SelectedItem.SphereOnGround: sphereOnGroundPen = highlightPen; break;
            }
            
            var shapeBrush = new SolidBrush(SystemColors.Control);

            g.DrawLine(backwallPen, backWall.Location, new System.Drawing.Point(backWall.Location.X + backWall.Width, backWall.Location.Y));
            g.DrawLine(backwallPen, backWall.Location, new System.Drawing.Point(backWall.Location.X, backWall.Location.Y + backWall.Height));
            g.DrawLine(backwallPen, new System.Drawing.Point(backWall.Location.X + backWall.Width, backWall.Location.Y), new System.Drawing.Point(backWall.Location.X + backWall.Width, backWall.Location.Y + backWall.Height));
            g.DrawLine(backwallPen, new System.Drawing.Point(backWall.Location.X, backWall.Location.Y + backWall.Height), new System.Drawing.Point(backWall.Location.X + backWall.Width, backWall.Location.Y + backWall.Height));

            g.DrawLine(leftwallPen, backWall.Location, new System.Drawing.Point(0,0));
            g.DrawLine(leftwallPen, new System.Drawing.Point(backWall.Location.X, backWall.Location.Y + backWall.Height), new System.Drawing.Point(0, canvas.Height));
            if(currentItem == SelectedItem.LeftWall)
            {
                g.DrawLine(leftwallPen, backWall.Location, new System.Drawing.Point(backWall.Location.X, backWall.Location.Y + backWall.Height));
            }

            g.DrawLine(rightwallPen, new System.Drawing.Point(backWall.Location.X + backWall.Width, backWall.Location.Y), new System.Drawing.Point(canvas.Width,0));
            g.DrawLine(rightwallPen, new System.Drawing.Point(backWall.Location.X + backWall.Width, backWall.Location.Y + backWall.Height), new System.Drawing.Point(canvas.Width, canvas.Height));
            if(currentItem == SelectedItem.RightWall)
            {
                g.DrawLine(rightwallPen, new System.Drawing.Point(backWall.Location.X + backWall.Width, backWall.Location.Y), new System.Drawing.Point(backWall.Location.X + backWall.Width, backWall.Location.Y + backWall.Height));
            }

            g.FillRectangle(shapeBrush, cube);
            g.DrawRectangle(cubePen, cube);

            g.FillEllipse(shapeBrush, sphereOnCube);
            g.DrawEllipse(sphereOnCubePen, sphereOnCube);

            g.FillEllipse(shapeBrush, sphereOnGround);
            g.DrawEllipse(sphereOnGroundPen, sphereOnGround);



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
            if (cube.Contains(e.Location))
            {
                currentItem = SelectedItem.Cube;
            }else if (sphereOnCube.Contains(e.Location))
            {
                currentItem = SelectedItem.SphereOnCube;
            } else if (sphereOnGround.Contains(e.Location))
            {
                currentItem = SelectedItem.SphereOnGround;
            } else if (backWall.Contains(e.Location))
            {
                currentItem = SelectedItem.BackWall;
            }else if(e.Location.X < backWall.Location.X)
            {
                currentItem = SelectedItem.LeftWall;
            }
            else if (e.Location.X > backWall.Location.X + backWall.Width)
            {
                currentItem = SelectedItem.RightWall;
            }

            redrawScheme();
        }
    }
}
