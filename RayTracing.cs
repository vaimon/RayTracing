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

        public int simulate()
        {
            var start = DateTime.Now;
            onProgressIncremented(0.0, DateTime.Now - start);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    for (int k = 0; k < 10000; k++)
                    {
                        continue;
                    }
                }
                onProgressIncremented(i * 1.0 / 100, DateTime.Now - start);
            }
            onProgressIncremented(1.0, DateTime.Now - start);
            return 1;
        }

        public Bitmap compute(Size frameSize)
        {
            var start = DateTime.Now;
            onProgressIncremented(0.0, DateTime.Now - start);
            var bitmap = new Bitmap(frameSize.Width, frameSize.Height);
            Random rand = new Random();
            int processedPixelCount = 0;
            double totalPixelCount = frameSize.Width * frameSize.Height;
            using (FastBitmap fbitmap = new FastBitmap(bitmap)) {    
                for(int x = 0; x < frameSize.Width; x++)
                {
                    for(int y = 0; y < frameSize.Height; y++)
                    {
                        fbitmap.SetPixel(new Point(x,y),Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255), rand.Next(255)));
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
