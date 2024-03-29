﻿using System;
using System.Globalization;

namespace B.Classes.Atributos.Metodos.Exercicio02
{
    public class Exercicio02
    {
        public static void Executar()
        {
            Funcionario f = new Funcionario();

            Console.WriteLine("Entre com os dados do funcionário: ");
            f.Nome = Console.ReadLine();
            f.SalarioBruto = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            double SalarioLiquido = f.ObterSalarioLiquido();

            Console.WriteLine(
                $"Funcionário: {f.Nome}, $ {SalarioLiquido.ToString("F2", CultureInfo.InvariantCulture)}");

            Console.WriteLine("Digite a porcentagem para aumentar o salário: ");
            double porcentagem = double.Parse(Console.ReadLine());

            f.AumentarSalarioBruto(porcentagem);
            SalarioLiquido = f.ObterSalarioLiquido();

            Console.WriteLine(
                $"Dados atualizados: {f.Nome}, $ {SalarioLiquido.ToString("F2", CultureInfo.InvariantCulture)}");
        }
    }
}