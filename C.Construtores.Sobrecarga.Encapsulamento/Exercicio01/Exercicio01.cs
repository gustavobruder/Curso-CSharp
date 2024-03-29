﻿using System;
using System.Globalization;

namespace C.Construtores.Sobrecarga.Encapsulamento.Exercicio01
{
    public class Exercicio01
    {
        public static void Executar()
        {
            Conta c;

            Console.Write("Entre com o número da conta: ");
            int numero = int.Parse(Console.ReadLine());

            Console.Write("Entre com o titular da conta: ");
            string titular = Console.ReadLine();

            Console.Write("Haverá depósito inicial (s/n)? ");
            char resposta = char.Parse(Console.ReadLine());

            if (resposta == 's' || resposta == 'S')
            {
                Console.Write("Entre com o valor inicial: ");
                double depositoInicial = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                c = new Conta(numero, titular, depositoInicial);
            }
            else
            {
                c = new Conta(numero, titular);
            }

            Console.WriteLine();
            Console.WriteLine("Dados da conta: \n" + c + "\n");
            Console.Write("Entre com um valor para depósito: ");
            double deposito = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            c.AdicionarDeposito(deposito);

            Console.WriteLine();
            Console.WriteLine("Dados da conta: \n" + c + "\n");
            Console.Write("Entre com um valor para saque: ");
            double saque = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            c.RecolherSaque(saque);

            Console.WriteLine();
            Console.WriteLine("Dados da conta: \n" + c);
        }
    }
}