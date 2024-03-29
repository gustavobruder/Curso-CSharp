﻿using System;

namespace D.Memoria.Arrays.Listas.Aula05
{
    public class Aula05
    {
        private static int Somar(int[] numeros)
        {
            int soma = 0;
            for (int i = 0; i < numeros.Length; i++)
            {
                soma += numeros[i];
            }

            return soma;
        }

        private static int SomarParams(params int[] numeros)
        {
            int soma = 0;
            for (int i = 0; i < numeros.Length; i++)
            {
                soma += numeros[i];
            }

            return soma;
        }

        public static void Executar()
        {
            int soma1 = Somar(new int[] { 2, 3, 5 });
            int soma2 = SomarParams(2, 3, 5);

            Console.WriteLine("Soma default: " + soma1);
            Console.WriteLine("Soma params: " + soma2);
        }
    }
}