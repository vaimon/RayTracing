using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    class Room : Shape
    {
        double side;
        private List<Face> faces;
        public Room(Point center, double side, Face leftWall, Face rightWall, Face backWall, Face floor, Face ceiling, Face cameraWall)
        {
            this.center = center;
            this.side = side;
            faces = new List<Face>();
            
            faces.Add(cameraWall);
            faces.Add(backWall);
            faces.Add(ceiling);
            faces.Add(floor);
            faces.Add(rightWall);
            faces.Add(leftWall);
        }

        public override Tuple<Point, Vector> getIntersection(Vector direction, Point origin)
        {
            double nearestPoint = double.MaxValue;
            Tuple<Point, Vector> res = null;
            foreach (var face in faces)
            {
                Tuple<Point, Vector> intersectionAndNormale;
                if ((intersectionAndNormale = face.getIntersection(direction, origin)) != null)
                {
                    nearestPoint = intersectionAndNormale.Item1.z;
                    res = intersectionAndNormale;
                    color = face.color;
                    material = face.material;
                }
            }
            return res;
        }

        public override Tuple<Point, Vector> getInnerIntersection(Vector direction, Point origin)
        {
            return null;
        }
    }
}
