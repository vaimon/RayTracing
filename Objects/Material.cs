﻿using System;
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

        public Material(double shininess, double kspecular, double kdiffuse, double kambient)
        {
            this.shininess = shininess;
            this.kspecular = kspecular;
            this.kdiffuse = kdiffuse;
            this.kambient = kambient;
        }
    }
}
