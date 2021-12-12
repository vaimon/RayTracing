using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing.Geometry
{
    abstract class Shape
    {
        public Point center;

        public abstract Point? getIntersection(Vector direction, Point origin);
    }
}
