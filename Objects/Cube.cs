using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Cube : Shape
    {
        double side;
        private List<Face> faces;
        public Cube(Point center, double side, Color color, Material material)
        {
            this.center = center;
            this.side = side;
            this.color = color;
            this.material = material;
            faces = new List<Face>();
            faces.Add(new Face(new Point(center.x, center.y, center.z - side / 2), new Vector(0, 0, -1), new Vector(0, 1, 0), side, side));
            faces.Add(new Face(new Point(center.x, center.y, center.z + side / 2), new Vector(0, 0, 1), new Vector(0, 1, 0), side, side));
            faces.Add(new Face(new Point(center.x, center.y + side / 2, center.z), new Vector(0, 1, 0), new Vector(0, 0, 1), side, side));
            faces.Add(new Face(new Point(center.x, center.y - side / 2, center.z), new Vector(0, -1, 0), new Vector(0, 0, 1), side, side));
            faces.Add(new Face(new Point(center.x + side / 2, center.y, center.z), new Vector(1, 0, 0), new Vector(0, 1, 0), side, side));
            faces.Add(new Face(new Point(center.x - side / 2, center.y, center.z), new Vector(-1, 0, 0), new Vector(0, 1, 0), side, side));
        }

        public override Tuple<Point, Vector> getIntersection(Vector direction, Point origin)
        {
            double nearestPoint = double.MaxValue;
            Tuple<Point, Vector> res = null;
            foreach (var face in faces)
            {
                Tuple<Point, Vector> intersectionAndNormale;
                if ((intersectionAndNormale = face.getIntersection(direction, origin)) != null && Geometry.distance(origin, intersectionAndNormale.Item1) < nearestPoint)
                {
                    nearestPoint = Geometry.distance(origin, intersectionAndNormale.Item1);
                    res =  intersectionAndNormale;
                }
            }
            return res;
        }
    }
}
