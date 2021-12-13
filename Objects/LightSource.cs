using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class LightSource
    {
        public Point location;
        public double intensity;

        public LightSource(Point location, double intensivity)
        {
            this.location = location;
            this.intensity = intensivity;
        }
    }
}
