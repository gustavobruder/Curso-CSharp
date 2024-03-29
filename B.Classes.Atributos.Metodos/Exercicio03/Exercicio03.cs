﻿using System;
using System.Globalization;

namespace B.Classes.Atributos.Metodos.Exercicio03
{
    public class Exercicio03
    {
        public static void Executar()
        {
            Console.WriteLine("Entre com os dados do aluno:");

            Aluno a = new Aluno();
            a.Nome = Console.ReadLine();
            a.Nota1 = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            a.Nota2 = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            a.Nota3 = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            Console.WriteLine("Nota final = " + a.CalcularMediaFinal().ToString("F2", CultureInfo.InvariantCulture));

            if (a.CalcularMediaFinal() >= Aluno.Media)
            {
                Console.WriteLine("APROVADO");
            }
            else
            {
                Console.WriteLine("REPROVADO");
                Console.WriteLine(
                    $"Faltaram {a.CalcularPontosFaltantes().ToString("F2", CultureInfo.InvariantCulture)} pontos");
            }
        }
    }
}