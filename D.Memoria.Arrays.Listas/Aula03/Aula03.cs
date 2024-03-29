﻿using System;
using System.Globalization;

namespace D.Memoria.Arrays.Listas.Aula03
{
    public class Aula03
    {
        public static void Executar()
        {
            Console.Write("Quantas posições o vetor terá? ");
            int n = int.Parse(Console.ReadLine());

            double[] vetor = new double[n];
            double soma = 0.0;

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Posição {i}: ");
                vetor[i] = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                soma += vetor[i];
            }

            double media = soma / n;

            Console.WriteLine();
            Console.WriteLine("Soma total: " + soma);
            Console.WriteLine("Media final: " + media);
        }
    }
}