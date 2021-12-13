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
    public partial class Form1 : Form
    {
        RayTracing rayTracing;
        public Form1()
        {
            InitializeComponent();
            rayTracing = new RayTracing(new LightSource(new Point(-20,20,-20),1.5));
            rayTracing.renderProgress += updateProgress;
            rayTracing.addShape(new Sphere(new Point(-3, 0, 16), 2, Color.DarkSalmon));
            rayTracing.addShape(new Sphere(new Point(-1, -1.5, 12), 2, Color.DarkRed));
            rayTracing.addShape(new Sphere(new Point(1.5, -0.5, 18), 3, Color.DarkGreen));
            rayTracing.addShape(new Sphere(new Point(7, 5, 18), 4, Color.DarkBlue));
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
                var bitmap = await Task.Run(() => rayTracing.compute(new Size(800,400)));
                var form = new FormResult(bitmap);
                form.Show();
            } catch (Exception e)
            {
                labelTime.Text = "HAHA exception";
            }
        }
    }
}
