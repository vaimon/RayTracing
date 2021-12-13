using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    using FastBitmap;

    public delegate void RenderProgressHandler(double progress, TimeSpan elapsedTime);

    class RayTracing
    {
      
        public event RenderProgressHandler renderProgress;

        List<Shape> scene;
        const double fov = 60;
        Point cameraPosition = new Point(0, 0, 0);
        LightSource lightSource;

        public RayTracing(LightSource lightSource)
        {
            this.lightSource = lightSource;
            scene = new List<Shape>();
        }

        public void addShape(Shape shape)
        {
            scene.Add(shape);
        }

        static Color changeColorIntensity(Color color, double intensity)
        {
            if(intensity < 0)
            {
                throw new Exception("Intensity must be >= 0!");
            }
            return Color.FromArgb((byte)Math.Clamp(Math.Round(color.R * intensity),0,255), (byte)Math.Clamp(Math.Round(color.G * intensity), 0, 255), (byte)Math.Clamp(Math.Round(color.B * intensity), 0, 255));
        }

        Color shootRay(Vector direction)
        {
            double nearestPoint = double.MaxValue;
            Color res = Color.Gray;
            foreach (var shape in scene)
            {
                Point intersection;
                if((intersection = shape.getIntersection(direction,cameraPosition)) != null && intersection.z < nearestPoint)
                {
                    nearestPoint = intersection.z;
                    var normale = new Vector(shape.center, intersection, true);
                    var shadowRay = new Vector(intersection, lightSource.location, true);
                    var hmm = shadowRay ^ normale;
                    res = changeColorIntensity(shape.color, lightSource.intensity * Math.Clamp(shadowRay ^ normale,0.0,double.MaxValue));
                }
            }
            return res;
        }

        public Bitmap compute(Size frameSize)
        {
            var start = DateTime.Now;
            onProgressIncremented(0.0, DateTime.Now - start);
            var bitmap = new Bitmap(frameSize.Width, frameSize.Height);
            int processedPixelCount = 0;
            double totalPixelCount = frameSize.Width * frameSize.Height;

            using (FastBitmap fbitmap = new FastBitmap(bitmap)) {    
                for(int x = 0; x < frameSize.Width; x++)
                {
                    for(int y = 0; y < frameSize.Height; y++)
                    {
                        // Формулу для лучей в итоге дёрнул отсюда (тут фов красиво задаётся, а не двиганием экрана...): https://habr.com/ru/post/436790/
                        Vector ray = new Vector((2 * (x + 0.5) / frameSize.Width - 1) * Math.Tan(Geometry.degreesToRadians(fov / 2)) * frameSize.Width / frameSize.Height,
                                                -(2 * (y + 0.5) / frameSize.Height - 1) * Math.Tan(Geometry.degreesToRadians(fov / 2)),
                                                1, true);
                        var color = shootRay(ray);
                        fbitmap.SetPixel(new System.Drawing.Point(x,y),color);
                        processedPixelCount++;
                        if(y % 10 == 0)
                        {
                            onProgressIncremented(processedPixelCount / totalPixelCount, DateTime.Now - start);
                        }
                    }
                }
            }
            return bitmap;
        }

        void onProgressIncremented(double progress, TimeSpan time)
        {
            renderProgress?.Invoke(progress, time);
        }
    }
}
