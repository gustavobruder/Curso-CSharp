﻿﻿﻿using System;
using System.Globalization;

namespace Curso.Aula01
{
    public class Aula01
    {
        public void MetodoAula01()
        {
            Triangulo x, y;
            x = new Triangulo();
            y = new Triangulo();
        
            Console.WriteLine("Entre com as medidas do triângulo X: ");
            x.A = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            x.B = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            x.C = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            
            Console.WriteLine("Entre com as medidas do triângulo Y: ");
            y.A = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            y.B = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            y.C = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
        
            double areaX = x.CalcularArea();
            double areaY = y.CalcularArea();
        
            Console.WriteLine("Área de X = " + areaX);
            Console.WriteLine("Área de Y = " + areaY);
            
            if (areaX > areaY)
            {
                Console.WriteLine("Maior área: X");
            }else
            {
                Console.WriteLine("Maior área: Y");
            }
        }
    }
}