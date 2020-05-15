﻿using System;

namespace D_memoria_arrays_listas.Exercicio03
{
    public class Program
    {
        /*static void Main(string[] args)
        {
            Teste();
        }*/

        static void Teste()
        {
            Console.Write("Insira a ordem (n) da matriz: ");
            int n = int.Parse(Console.ReadLine());
            
            int[,] matriz = new int[n, n];
            int numerosNegativos = 0;

            for (int linha = 0; linha < n; linha++)
            {
                string[] vetor = Console.ReadLine().Split(' ');
                
                for (int coluna = 0; coluna < n; coluna++)
                {
                    matriz[linha, coluna] = int.Parse(vetor[coluna]);
                    if (matriz[linha, coluna] < 0)
                    {
                        numerosNegativos++;
                    }
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("Diagonal Principal:");
            for (int i = 0; i < n; i++)
            {
                Console.Write(matriz[i,i] + " ");
            }
            
            Console.WriteLine("\n");
            Console.WriteLine("Números negativos: " + numerosNegativos);
        }
    }
}