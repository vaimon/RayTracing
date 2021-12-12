using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing.Geometry
{
    class Sphere : Shape
    {
        double radius;

        public Sphere(Point center, double radius)
        {
            this.center = center;
            this.radius = radius;
        }


        // http://www.lighthouse3d.com/tutorials/maths/ray-sphere-intersection/
        public override Point getIntersection(Vector direction, Point origin)
        {
            direction = direction.normalize();
            Vector sourceToCenter = new Vector(origin, center);
            if((sourceToCenter ^ direction) < 0)  // Центр сферы за точкой выпуска луча
            {
                if(Geometry.distance(origin,center) > radius)                       // Пересечения нет
                {
                    return null;
                }
                else if (Geometry.distance(origin, center) - radius < 0.0001)       // Мы на сфере
                {
                    return origin;
                }
                else                                                                // Мы внутри сферы
                {
                    Point projection = Geometry.getPointProjection(origin, direction, center);
                    double distance = Math.Sqrt(Math.Pow(radius, 2) - Math.Pow(Geometry.distance(center, projection), 2)) - Geometry.distance(origin,projection);
                    return Geometry.pointOnLine(origin, direction, distance);
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
                    return Geometry.pointOnLine(projection, direction, distance);
                }
            }
        }
    }
}
