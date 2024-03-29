﻿using System;
using System.Globalization;

namespace C.Construtores.Sobrecarga.Encapsulamento.Aula02
{
    public class Aula02
    {
        public static void Executar()
        {
            Console.WriteLine("Entre os dados do produto:");
            string nome = Console.ReadLine();
            double preco = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            /*int quantidade = int.Parse(Console.ReadLine());*/

            Produto p = new Produto(nome, preco);

            Produto p2 = new Produto();

            Console.WriteLine("Dados do produto: " + p.ExibirMensagem());

            Console.WriteLine("Digite o número de produtos a ser adicionado ao estoque: ");
            int adicionar = int.Parse(Console.ReadLine());

            p.AdicionarProdutos(adicionar);

            Console.WriteLine("Dados atualizados: " + p.ExibirMensagem());

            Console.WriteLine("Digite o número de produtos a ser removido do estoque: ");
            int remover = int.Parse(Console.ReadLine());

            p.RemoverProdutos(remover);

            Console.WriteLine("Dados atualizados: " + p.ExibirMensagem());
        }
    }
}