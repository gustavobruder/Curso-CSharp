﻿using System;

namespace E.Topicos.Especiais.Parte01.Aula02
{
    public class Aula02
    {
        public static void Executar()
        {
            Console.Write("Informe um número: ");
            int x = int.Parse(Console.ReadLine());
            string dia;

            switch (x)
            {
                case 1:
                    dia = "Domingo";
                    break;
                case 2:
                    dia = "Segunda";
                    break;
                case 3:
                    dia = "Terça";
                    break;
                case 4:
                    dia = "Quarta";
                    break;
                case 5:
                    dia = "Quinta";
                    break;
                case 6:
                    dia = "Sexta";
                    break;
                case 7:
                    dia = "Sábado";
                    break;
                default:
                    dia = "Valor inválido! Tente de 1-7";
                    break;
            }

            Console.WriteLine($"Dia da semana: {dia}");
        }
    }
}