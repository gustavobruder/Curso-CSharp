﻿using System;

namespace D.Memoria.Arrays.Listas.Aula09
{
    public class Aula09
    {
        public static void Executar()
        {
            double[,] matriz = new double[2, 3];

            Console.WriteLine("Length: " + matriz.Length);

            Console.WriteLine("Rank: " + matriz.Rank);

            Console.WriteLine("GetLength(0): " + matriz.GetLength(0));

            Console.WriteLine("GetLength(1): " + matriz.GetLength(1));
        }
    }
}