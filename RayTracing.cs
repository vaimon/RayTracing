using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
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

        void onProgressIncremented(double progress, TimeSpan time)
        {
            renderProgress?.Invoke(progress, time);
        }
    }
}
