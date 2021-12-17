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
        static int idpool = 0;
        public Point center;
        public Color color;
        public Material material;
        public int id;

        public Shape()
        {
            this.id = idpool++;
        }

        public abstract Tuple<Point,Vector> getIntersection(Vector direction, Point origin);
    }
}
