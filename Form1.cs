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
    public enum SelectedItem
    {
        BackWall,
        RightWall,
        LeftWall,
        Floor,
        Ceiling,
        Cube,
        SphereOnCube,
        SphereOnGround
    }

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
        Face cameraWall;
        Face ceiling;
        Face floor;

        LightSource additionalLight = new LightSource(new Point(-7, 7, 0), 0.8);

        public Form1()
        {
            InitializeComponent();
            canvas.Image = new Bitmap(canvas.Width, canvas.Height);
            g = Graphics.FromImage(canvas.Image);

            var center = new Point(0, 0, 9);
            double roomSide = 20;

            initShapes(center, roomSide);
            initScheme();

            rayTracing = new RayTracing(new Room(center, roomSide, leftWall, rightWall, backWall,floor,ceiling,cameraWall));
            rayTracing.renderProgress += updateProgress;

            currentItemType = SelectedItem.BackWall;
            currentItem = backWall;
            updateValuesForSelectedItem();
            redrawScheme();
        }

        void initShapes(Point center, double roomSide)
        {
            sphereOnGround = new Sphere(new Point(5, -8, 17), 4, Color.DarkSalmon,
                new Material(10, 0.1, 0.85, 0.05, 0, 0));
            cube = new Cube(new Point(-5, -7, 16), 6, Color.DarkRed, new Material(40, 0.25, 0.7, 0.05, 0, 0));
            sphereOnCube = new Sphere(new Point(-5, 0, 16), 2, Color.DarkGreen,
                new Material(40, 0.25, 0.7, 0.05, 0, 0));
            
            leftWall = new Face(new Point(center.x - roomSide / 2, center.y, center.z), new Vector(1, 0, 0),
                new Vector(0, 1, 0), roomSide, roomSide, Color.IndianRed, new Material(0, 0, 0.9, 0.1, 0, 0));
            rightWall = new Face(new Point(center.x + roomSide / 2, center.y, center.z), new Vector(-1, 0, 0),
                new Vector(0, 1, 0), roomSide, roomSide, Color.Navy, new Material(0, 0, 0.9, 0.1, 0, 0));
            backWall = new Face(new Point(center.x, center.y, center.z + roomSide / 2), new Vector(0, 0, -1),
                new Vector(0, 1, 0), roomSide, roomSide, Color.Gray, new Material(0, 0, 0.9, 0.1, 0, 0));
            cameraWall = new Face(new Point(center.x, center.y, center.z - roomSide / 2), new Vector(0, 0, 1), new Vector(0, 1, 0), roomSide, roomSide, Color.Gray, new Material(0, 0, 0.9, 0.1, 0, 0));
            ceiling = new Face(new Point(center.x, center.y + roomSide / 2, center.z), new Vector(0, -1, 0), new Vector(0, 0, 1), roomSide, roomSide, Color.Gray, new Material(0, 0, 0.9, 0.1, 0, 0));
            floor = new Face(new Point(center.x, center.y - roomSide / 2, center.z), new Vector(0, 1, 0), new Vector(0, 0, 1), roomSide, roomSide, Color.Gray, new Material(0, 0, 0.9, 0.1, 0, 0));
        }

        void initScheme()
        {
            backWallScheme = new Rectangle(canvas.Width / 2 - 100, canvas.Height / 2 - 100, 200, 200);
            cubeScheme = new Rectangle(canvas.Width / 2 - 125, canvas.Height / 2 + 25, 100, 100);
            sphereOnCubeScheme = new Rectangle(canvas.Width / 2 - 100, canvas.Height / 2 - 25, 50, 50);
            sphereOnGroundScheme = new Rectangle(canvas.Width / 2 + 25, canvas.Height / 2 + 40, 100, 100);
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
            progressBar.Value = (int) Math.Round(progress * 100);
        }

        private void btnCompute_Click(object sender, EventArgs e)
        {
            labelTime.Visible = true;
            progressBar.Visible = true;

            rayTracing.clear();

            rayTracing.addShape(sphereOnCube);
            //rayTracing.addShape(new Face(new Point(-5, -7 + 3, 16), new Vector(0, 1, 0), new Vector(0, 0, 1), 6, 6, Color.DarkRed));
            rayTracing.addShape(cube);
            rayTracing.addShape(sphereOnGround);
            
            rayTracing.addShape(cameraWall);
            rayTracing.addShape(backWall);
            rayTracing.addShape(ceiling);
            rayTracing.addShape(floor);
            rayTracing.addShape(rightWall);
            rayTracing.addShape(leftWall);

            rayTracing.addLightSource(new LightSource(new Point(0, 9, 9), 1));

            if (checkBoxAdditionalLight.Checked)
            {
                rayTracing.addLightSource(additionalLight);
            }
            changeInterfaceAvailability(false);
            runAsyncComputation();
        }

        async void runAsyncComputation()
        {
            try
            {
                var bitmap = await Task.Run(() => rayTracing.compute(new Size(640,480)));
                changeInterfaceAvailability(true);
                var form = new FormResult(bitmap);
                form.Show();
            }
            catch (Exception e)
            {
                labelTime.Text = "HAHA exception";
            }
        }

        // ====================================================================
        // ========================== BORING UI ZONE ==========================
        // ====================================================================

        void redrawScheme()
        {
            g.Clear(Color.Gray);
            Pen backwallPen = standardPen,
                rightwallPen = standardPen,
                leftwallPen = standardPen,
                floorPen = standardPen,
                ceilingPen = standardPen,
                sphereOnCubePen = standardPen,
                sphereOnGroundPen = standardPen,
                cubePen = standardPen;
            switch (currentItemType)
            {
                case SelectedItem.BackWall:
                    backwallPen = highlightPen;
                    break;
                case SelectedItem.Cube:
                    cubePen = highlightPen;
                    break;
                case SelectedItem.LeftWall:
                    leftwallPen = highlightPen;
                    break;
                case SelectedItem.RightWall:
                    rightwallPen = highlightPen;
                    break;
                case SelectedItem.SphereOnCube:
                    sphereOnCubePen = highlightPen;
                    break;
                case SelectedItem.SphereOnGround:
                    sphereOnGroundPen = highlightPen;
                    break;
                case SelectedItem.Floor:
                    floorPen = highlightPen;
                    break;
                case SelectedItem.Ceiling:
                    ceilingPen = highlightPen;
                    break;
            }
            // ========================== COLORS ==========================
            g.FillRectangle(new SolidBrush(backWall.color), backWallScheme);

            g.FillPolygon(new SolidBrush(ceiling.color),
                new PointF[]
                {
                    new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y),
                    new System.Drawing.Point(0, 0),
                    new System.Drawing.Point(canvas.Width, 0),
                    new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y),
                });
            g.FillPolygon(new SolidBrush(leftWall.color),
                new PointF[]
                {
                    new System.Drawing.Point(0, 0), backWallScheme.Location,
                    new System.Drawing.Point(backWallScheme.Location.X,
                        backWallScheme.Location.Y + backWallScheme.Height),
                    new System.Drawing.Point(0, canvas.Height)
                });
            g.FillPolygon(new SolidBrush(rightWall.color),
                new PointF[]
                {
                    new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width,
                        backWallScheme.Location.Y),
                    new System.Drawing.Point(canvas.Width, 0), new System.Drawing.Point(canvas.Width, canvas.Height),
                    new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width,
                        backWallScheme.Location.Y + backWallScheme.Height)
                });
            g.FillPolygon(new SolidBrush(floor.color),
                new PointF[]
                {
                    new System.Drawing.Point(backWallScheme.Left, backWallScheme.Bottom),
                    new System.Drawing.Point(0, canvas.Height),
                    new System.Drawing.Point(canvas.Width, canvas.Height),
                    new System.Drawing.Point(backWallScheme.Right, backWallScheme.Bottom),
                });

            // ========================== BACK WALL ==========================
            g.DrawLine(backwallPen, backWallScheme.Location,
                new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y));
            g.DrawLine(backwallPen, backWallScheme.Location,
                new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y + backWallScheme.Height));
            g.DrawLine(backwallPen,
                new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y),
                new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width,
                    backWallScheme.Location.Y + backWallScheme.Height));
            g.DrawLine(backwallPen,
                new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y + backWallScheme.Height),
                new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width,
                    backWallScheme.Location.Y + backWallScheme.Height));

            // ========================== LEFT WALL ==========================
            g.DrawLine(leftwallPen, backWallScheme.Location, new System.Drawing.Point(0, 0));
            g.DrawLine(leftwallPen,
                new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y + backWallScheme.Height),
                new System.Drawing.Point(0, canvas.Height));
            if (currentItemType == SelectedItem.LeftWall)
            {
                g.DrawLine(leftwallPen, backWallScheme.Location,
                    new System.Drawing.Point(backWallScheme.Location.X,
                        backWallScheme.Location.Y + backWallScheme.Height));
            }

            // ========================== RIGHT WALL ==========================
            g.DrawLine(rightwallPen,
                new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y),
                new System.Drawing.Point(canvas.Width, 0));
            g.DrawLine(rightwallPen,
                new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width,
                    backWallScheme.Location.Y + backWallScheme.Height),
                new System.Drawing.Point(canvas.Width, canvas.Height));
            if (currentItemType == SelectedItem.RightWall)
            {
                g.DrawLine(rightwallPen,
                    new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width,
                        backWallScheme.Location.Y),
                    new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width,
                        backWallScheme.Location.Y + backWallScheme.Height));
            }

            // ========================== CEILING ==========================
            if(currentItemType != SelectedItem.RightWall)
            {
                g.DrawLine(ceilingPen,
                new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y),
                new System.Drawing.Point(canvas.Width, 0));
            }
            if (currentItemType != SelectedItem.LeftWall)
            {
                g.DrawLine(ceilingPen,
                new System.Drawing.Point(backWallScheme.Location.X,
                    backWallScheme.Location.Y),
                new System.Drawing.Point(0, 0));
            }
            if (currentItemType == SelectedItem.Ceiling)
            {
                g.DrawLine(ceilingPen,
                    new System.Drawing.Point(backWallScheme.Location.X, backWallScheme.Location.Y),
                    new System.Drawing.Point(backWallScheme.Location.X + backWallScheme.Width, backWallScheme.Location.Y));
            }

            // ========================== FLOOR ==========================
            if (currentItemType != SelectedItem.RightWall)
            {
                g.DrawLine(floorPen,
                new System.Drawing.Point(backWallScheme.Right, backWallScheme.Bottom),
                new System.Drawing.Point(canvas.Width, canvas.Height));
            }
            if (currentItemType != SelectedItem.LeftWall)
            {
                g.DrawLine(floorPen,
                new System.Drawing.Point(backWallScheme.Left, backWallScheme.Bottom),
                new System.Drawing.Point(0, canvas.Height));
            }
            if (currentItemType == SelectedItem.Floor)
            {
                g.DrawLine(floorPen,
                    new System.Drawing.Point(backWallScheme.Left, backWallScheme.Bottom),
                    new System.Drawing.Point(backWallScheme.Right, backWallScheme.Bottom));
            }

            // ========================== OBJECTS ==========================
            g.FillRectangle(new SolidBrush(cube.color), cubeScheme);
            g.DrawRectangle(cubePen, cubeScheme);

            g.FillEllipse(new SolidBrush(sphereOnCube.color), sphereOnCubeScheme);
            g.DrawEllipse(sphereOnCubePen, sphereOnCubeScheme);

            g.FillEllipse(new SolidBrush(sphereOnGround.color), sphereOnGroundScheme);
            g.DrawEllipse(sphereOnGroundPen, sphereOnGroundScheme);

            g.FillEllipse(new SolidBrush(Color.Gold), new Rectangle(canvas.Width / 2 - 11, 50, 25, 7));

            canvas.Invalidate();
        }

        private void canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (cubeScheme.Contains(e.Location))
            {
                currentItemType = SelectedItem.Cube;
                currentItem = cube;
            }
            else if (sphereOnCubeScheme.Contains(e.Location))
            {
                currentItemType = SelectedItem.SphereOnCube;
                currentItem = sphereOnCube;
            }
            else if (sphereOnGroundScheme.Contains(e.Location))
            {
                currentItemType = SelectedItem.SphereOnGround;
                currentItem = sphereOnGround;
            }
            else if (backWallScheme.Contains(e.Location))
            {
                currentItemType = SelectedItem.BackWall;
                currentItem = backWall;
            }
            else if (e.Location.X < backWallScheme.Location.X)
            {
                currentItemType = SelectedItem.LeftWall;
                currentItem = leftWall;
            }
            else if (e.Location.X > backWallScheme.Location.X + backWallScheme.Width)
            {
                currentItemType = SelectedItem.RightWall;
                currentItem = rightWall;
            }
            else if (e.Location.Y < backWallScheme.Location.Y)
            {
                currentItemType = SelectedItem.Ceiling;
                currentItem = ceiling;
            }
            else if (e.Location.Y > backWallScheme.Location.Y + backWallScheme.Height)
            {
                currentItemType = SelectedItem.Floor;
                currentItem = floor;
            }

            updateValuesForSelectedItem();
            redrawScheme();
        }
        
        void updateValuesForSelectedItem()
        {
            if (currentItemType == SelectedItem.BackWall || currentItemType == SelectedItem.RightWall || currentItemType == SelectedItem.LeftWall ||
                currentItemType == SelectedItem.Ceiling || currentItemType == SelectedItem.Floor)
            {
                checkTransparency.Enabled = false;
                transparencyValue.Enabled = false;
            }
            else
            {
                checkTransparency.Enabled = true;
                checkTransparency.Checked = currentItem.material.transparency > 0;
                transparencyValue.Enabled = currentItem.material.transparency > 0;
                transparencyValue.Value = Convert.ToDecimal(currentItem.material.transparency);
            }

            checkMirror.Checked = currentItem.material.reflectivity > 0;
            reflectivityValue.Enabled = currentItem.material.reflectivity > 0;
            reflectivityValue.Value = Convert.ToDecimal(currentItem.material.reflectivity);
        }

        private void buttonChangeColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = currentItem.color;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                currentItem.color = colorDialog1.Color;
                redrawScheme();
            }
        }

        private void checkBoxAdditionalLight_CheckedChanged(object sender, EventArgs e)
        {
            lightValue.Enabled = checkBoxAdditionalLight.Checked;
            lightX.Enabled = checkBoxAdditionalLight.Checked;
            lightY.Enabled = checkBoxAdditionalLight.Checked;
            lightZ.Enabled = checkBoxAdditionalLight.Checked;
        }

        private void lightX_ValueChanged(object sender, EventArgs e)
        {
            additionalLight.location.x = (double) lightX.Value;
        }

        private void lightY_ValueChanged(object sender, EventArgs e)
        {
            additionalLight.location.y = (double) lightY.Value;
        }

        private void lightZ_ValueChanged(object sender, EventArgs e)
        {
            additionalLight.location.z = (double) lightZ.Value;
        }

        private void lightValue_ValueChanged(object sender, EventArgs e)
        {
            additionalLight.intensity = (double) lightValue.Value;
        }

        private void checkMirror_CheckedChanged(object sender, EventArgs e)
        {
            reflectivityValue.Enabled = checkMirror.Checked;
            reflectivityValue.Value = (decimal)(checkMirror.Checked? 1.0 : 0.0);
        }

        private void checkTransparency_CheckedChanged(object sender, EventArgs e)
        {
            transparencyValue.Enabled = checkTransparency.Checked;
            transparencyValue.Value = (decimal)(checkTransparency.Checked ? 1.0 : 0.0);
        }

        private void reflectivityValue_ValueChanged(object sender, EventArgs e)
        {
            currentItem.material.reflectivity = (double)reflectivityValue.Value;
        }

        private void transparencyValue_ValueChanged(object sender, EventArgs e)
        {
            currentItem.material.transparency = (double)transparencyValue.Value;
        }

        void changeInterfaceAvailability(bool isEnabled)
        {
            groupBox2.Enabled = isEnabled;
            groupBox3.Enabled = isEnabled;
            btnCompute.Enabled = isEnabled;
        }
    }
}