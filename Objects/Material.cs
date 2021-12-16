using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayTracing
{
    // Здесь собраны все константы для Фонга https://en.wikipedia.org/wiki/Phong_reflection_model
    class Material
    {
        public double shininess; // Константа альфа для бликов
        public double kspecular;
        public double kdiffuse;
        public double kambient;
        public double krefraction;
        
        public double transparency;
        public double reflectivity;
        
        public Material(double shininess, double kspecular, double kdiffuse, double kambient, double transparency, double reflectivity, double krefraction = 1.5)
        {
            this.shininess = shininess;
            this.kspecular = kspecular;
            this.kdiffuse = kdiffuse;
            this.kambient = kambient;
            this.transparency = transparency;
            this.reflectivity = reflectivity;
            this.krefraction = krefraction;
        }
    }
}
