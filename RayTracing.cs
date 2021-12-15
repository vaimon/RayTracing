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
        const double fov = 80;
        Point cameraPosition = new Point(0, 0, 0);
        List<LightSource> lightSources;
        Room room;

        public RayTracing(Room room)
        {
            this.room = room;
            scene = new List<Shape>();
            lightSources = new List<LightSource>();
        }

        public void addShape(Shape shape)
        {
            scene.Add(shape);
        }

        public void addLightSource(LightSource lightSource)
        {
            lightSources.Add(lightSource);
        }

        public void clear()
        {
            scene.Clear();
            lightSources.Clear();
        }

        static Color changeColorIntensity(Color color, double intensity)
        {
            if (intensity < 0)
            {
                throw new Exception("Intensity must be >= 0!");
            }

            return Color.FromArgb((byte) Math.Clamp(Math.Round(color.R * intensity), 0, 255),
                (byte) Math.Clamp(Math.Round(color.G * intensity), 0, 255),
                (byte) Math.Clamp(Math.Round(color.B * intensity), 0, 255));
        }

        bool doesRayIntersectSomething(Vector direction, Point origin)
        {
            foreach (var shape in scene)
            {
                if (shape.getIntersection(direction, origin) != null)
                {
                    return true;
                }
            }

            return false;
        }

        Vector getReflectionRay(Vector shadowRay, Vector normale)
        {
            return (2 * (shadowRay ^ normale) * normale - shadowRay).normalize();
        }

        double computeLightness(Shape shape, Tuple<Point, Vector> intersectionAndNormale, Vector viewRay)
        {
            double diffuseLightness = 0;
            double specularLightness = 0;
            double ambientLightness = 1;
            foreach (var lightSource in lightSources)
            {
                var shadowRay = new Vector(intersectionAndNormale.Item1, lightSource.location, true);
                var reflectionRay = getReflectionRay(shadowRay, intersectionAndNormale.Item2);

                if (doesRayIntersectSomething(shadowRay, intersectionAndNormale.Item1))
                {
                    continue;
                }

                diffuseLightness += lightSource.intensity * Math.Clamp(shadowRay ^ intersectionAndNormale.Item2,
                    0.0, double.MaxValue);
                specularLightness += lightSource.intensity *
                                     Math.Pow(Math.Clamp(reflectionRay ^ (-1 * viewRay), 0.0, double.MaxValue),
                                         shape.material.shininess);
            }

            return ambientLightness * shape.material.kambient +
                                    diffuseLightness * shape.material.kdiffuse +
                                    specularLightness * shape.material.kspecular;
        }

        Color shootRay(Vector viewRay)
        {
            double nearestPoint = double.MaxValue;
            Color res = Color.Transparent;
            foreach (var shape in scene)
            {
                Tuple<Point, Vector> intersectionAndNormale;
                if ((intersectionAndNormale = shape.getIntersection(viewRay, cameraPosition)) != null &&
                    intersectionAndNormale.Item1.z < nearestPoint)
                {
                    nearestPoint = intersectionAndNormale.Item1.z;
                    res = changeColorIntensity(shape.color, computeLightness(shape,intersectionAndNormale,viewRay));
                }
            }

            if (res.ToArgb() == Color.Transparent.ToArgb())
            {
                Tuple<Point, Vector> intersectionAndNormale = room.getIntersection(viewRay, cameraPosition);
                res = changeColorIntensity(room.color, computeLightness(room,intersectionAndNormale,viewRay));
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

            using (FastBitmap fbitmap = new FastBitmap(bitmap))
            {
                for (int x = 0; x < frameSize.Width; x++)
                {
                    for (int y = 0; y < frameSize.Height; y++)
                    {
                        // Формулу для лучей в итоге дёрнул отсюда (тут фов красиво задаётся, а не двиганием экрана...): https://habr.com/ru/post/436790/
                        Vector ray = new Vector(
                            (2 * (x + 0.5) / frameSize.Width - 1) * Math.Tan(Geometry.degreesToRadians(fov / 2)) *
                            frameSize.Width / frameSize.Height,
                            -(2 * (y + 0.5) / frameSize.Height - 1) * Math.Tan(Geometry.degreesToRadians(fov / 2)),
                            1, true);
                        var color = shootRay(ray);
                        fbitmap.SetPixel(new System.Drawing.Point(x, y), color);
                        processedPixelCount++;
                        if (y % 10 == 0)
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