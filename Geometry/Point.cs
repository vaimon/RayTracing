using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Point
    {
        public double x, y, z;

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
        {
            return $"({x}, {y}, {z})";
        }

        public Point shift(Vector v, double distance)
        {
            return new Point(x + distance * v.x, y + distance * v.y, z + distance * v.z);
        }
       
    }
}
