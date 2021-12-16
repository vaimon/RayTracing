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

        public Face(Point center, Vector normale, Vector heightVector, double width, double height, Color color, Material material)
        {
            this.color = color;
            this.center = center;
            this.width = width;
            this.height = height;
            this.normale = normale;
            this.material = material;
            this.heightVector = heightVector.normalize();
            this.widthVector = (normale * heightVector).normalize();
        }

        public Point worldToFaceBasis(Point p, double k = 1)
        {
            return new Point(
                widthVector.x * (p.x - center.x) + widthVector.y * (p.y - center.y) +
                widthVector.z * (p.z - center.z),
                heightVector.x * (p.x - center.x) + heightVector.y * (p.y - center.y) +
                heightVector.z * (p.z - center.z),
                k * normale.x * (p.x - center.x) + k * normale.y * (p.y - center.y) +
                k * normale.z * (p.z - center.z));
        }

        public override Tuple<Point, Vector> getIntersection(Vector direction, Point origin)
        {
            if (Math.Abs(normale.x * (origin.x - center.x) + normale.y * (origin.y - center.y) + normale.z * (origin.z - center.z)) < 0.00001) // точка выпуска луча лежит в плоскости
            {
                return null;
            }

            double tn = -(normale.x * center.x) - (normale.y * center.y) - (normale.z * center.z) + (normale.x * origin.x) + (normale.y * origin.y) + (normale.z * origin.z);
            if(Math.Abs(tn) < 0.00001)          // прямая лежит на плоскости, если знаменатель тоже 0, не интересно
            {
                return null;
            }
            double td = normale.x * direction.x + normale.y * direction.y + normale.z * direction.z;
            if(Math.Abs(td) < 0.00001)          // прямая параллельна плоскости, не интересно
            {
                return null;
            }
            var pointInWorld = Geometry.pointOnLine(origin, direction, -tn / td);
            if(new Vector(origin, pointInWorld).z / direction.z < 0)     // вектор из полученной точки в ориджин всегда коллинеарен направлению, 
            {                                                            // но если х1/x2=y1/у2=z1/z2 - отрицательны, они противонаправлены
                return null;
            }
            var pointOnSurface = worldToFaceBasis(pointInWorld);
            
            if(Math.Abs(pointOnSurface.x) <= width/2 && Math.Abs(pointOnSurface.y) <= height/2)
            {
                return Tuple.Create(pointInWorld,normale);
            }
            return null;
        }

        public override Tuple<Point, Vector> getInnerIntersection(Vector direction, Point origin)
        {
            var antinormale = -1 * normale;
            if (Math.Abs(antinormale.x * (origin.x - center.x) + antinormale.y * (origin.y - center.y) + antinormale.z * (origin.z - center.z)) < 0.00001) // точка выпуска луча лежит в плоскости
            {
                return null;
            }

            double tn = -(antinormale.x * center.x) - (antinormale.y * center.y) - (antinormale.z * center.z) + (antinormale.x * origin.x) + (antinormale.y * origin.y) + (antinormale.z * origin.z);
            if(Math.Abs(tn) < 0.00001)          // прямая лежит на плоскости, если знаменатель тоже 0, не интересно
            {
                return null;
            }
            double td = antinormale.x * direction.x + antinormale.y * direction.y + antinormale.z * direction.z;
            if(Math.Abs(td) < 0.00001)          // прямая параллельна плоскости, не интересно
            {
                return null;
            }
            var pointInWorld = Geometry.pointOnLine(origin, direction, -tn / td);
            if(new Vector(origin, pointInWorld).z / direction.z > 0)     // вектор из полученной точки в ориджин всегда коллинеарен направлению, 
            {                                                            // но если х1/x2=y1/у2=z1/z2 - отрицательны, они противонаправлены
                return null;
            }
            var pointOnSurface = worldToFaceBasis(pointInWorld,-1);
            
            if(Math.Abs(pointOnSurface.x) <= width/2 && Math.Abs(pointOnSurface.y) <= height/2)
            {
                return Tuple.Create(pointInWorld,antinormale);
            }
            return null;
        }
    }
}
