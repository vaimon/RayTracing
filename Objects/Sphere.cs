using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Sphere : Shape
    {
        double radius;

        public Sphere(Point center, double radius, Color color)
        {
            this.center = center;
            this.radius = radius;
            this.color = color;
        }


        // http://www.lighthouse3d.com/tutorials/maths/ray-sphere-intersection/
        public override Tuple<Point, Vector> getIntersection(Vector direction, Point origin)
        {
            direction = direction.normalize();
            Vector sourceToCenter = new Vector(origin, center);
            if((sourceToCenter ^ direction) < 0)  // Центр сферы за точкой выпуска луча
            {
                if(Geometry.distance(origin,center) > radius)                       // Пересечения нет
                {
                    return null;
                }
                else if (Geometry.distance(origin, center) - radius < 0.000001)       // Мы на сфере
                {
                    //return origin;
                    return null;
                }
                else                                                                // Мы внутри сферы
                {
                    Point projection = Geometry.getPointProjection(origin, direction, center);
                    double distance = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(Geometry.distance(center, projection), 2)) - Geometry.distance(origin,projection);
                    var intersection = Geometry.pointOnLine(origin, direction, distance);
                    return Tuple.Create(intersection, new Vector(center, intersection, true));
                }
            }
            else        // Центр сферы можно спроецировать на луч
            {
                Point projection = Geometry.getPointProjection(origin, direction, center);
                if(Geometry.distance(center,projection) > radius)
                {
                    return null;
                }
                else
                {
                    double distance = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(Geometry.distance(center, projection), 2));
                    if(Geometry.distance(origin, center) > radius)
                    {
                        distance = Geometry.distance(origin, projection) - distance;
                    }
                    else
                    {
                        distance = Geometry.distance(origin, projection) + distance;
                    }
                    var intersection = Geometry.pointOnLine(origin, direction, distance);
                    return Tuple.Create(intersection, new Vector(center, intersection, true));
                }
            }
        }
    }
}
