using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Face : Shape
    {
        double width, height;
        public Vector normale;
        public Vector heightVector, widthVector;

        public Face(Point center, Vector normale, Vector heightVector, double width, double height)
        {
            this.center = center;
            this.width = width;
            this.height = height;
            this.normale = normale;
            this.heightVector = heightVector.normalize();
            this.widthVector = (normale * heightVector).normalize();
        }

        public Face(Point center, Vector normale, Vector heightVector, double width, double height, Color color)
        {
            this.color = color;
            this.center = center;
            this.width = width;
            this.height = height;
            this.normale = normale;
            this.heightVector = heightVector.normalize();
            this.widthVector = (normale * heightVector).normalize();
        }

        public Point worldToFaceBasis(Point p)
        {
            return new Point(
                widthVector.x * (p.x - center.x) + widthVector.y * (p.y - center.y) +
                widthVector.z * (p.z - center.z),
                heightVector.x * (p.x - center.x) + heightVector.y * (p.y - center.y) +
                heightVector.z * (p.z - center.z),
                normale.x * (p.x - center.x) + normale.y * (p.y - center.y) +
                normale.z * (p.z - center.z));
        }

        public override Tuple<Point, Vector> getIntersection(Vector direction, Point origin)
        {
            double tn = -(normale.x * center.x) - (normale.y * center.y) - (normale.z * center.z) + (normale.x * origin.x) + (normale.y * origin.y) + (normale.z * origin.z);
            if(Math.Abs(tn) < 0.00001)          // прямая лежит на плоскости, если знаменатель тоже 0, не интересно
            {
                return null;
            }
            double td = normale.x * direction.x + normale.y * direction.y + normale.z * direction.z;
            if(Math.Abs(tn) < 0.00001)          // прямая параллельна плоскости, не интересно
            {
                return null;
            }
            var pointInWorld = Geometry.pointOnLine(origin, direction, -tn / td);
            var pointOnSurface = worldToFaceBasis(pointInWorld);
            if(Math.Abs(pointOnSurface.x) <= width/2 && Math.Abs(pointOnSurface.y) <= height/2)
            {
                return Tuple.Create(pointInWorld,normale);
            }
            return null;
        }
    }
}
