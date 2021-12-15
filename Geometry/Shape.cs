using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    abstract class Shape
    {
        public Point center;
        public Color color;
        public Material material;

        public abstract Tuple<Point,Vector> getIntersection(Vector direction, Point origin);
    }
}
