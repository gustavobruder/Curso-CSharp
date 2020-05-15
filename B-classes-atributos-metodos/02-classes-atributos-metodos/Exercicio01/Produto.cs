﻿﻿﻿using System.Globalization;

namespace Curso.Exercicio01
{
    public class Produto
    {
        public string Nome;
        public double Preco;
        public int Quantidade;

        public double CalcularValorTotal()
        {
            return Preco * Quantidade;
        }

        public void AdicionarProdutos(int quantidade)
        {
            Quantidade += quantidade;
        }

        public void RemoverProdutos(int quantidade)
        {
            Quantidade -= quantidade;
        }

        public string ExibirMensagem()
        {
            return $"{Nome}, "
                   + $"$ {Preco.ToString("F2", CultureInfo.InvariantCulture)}, "
                   + $"{Quantidade} unidades, "
                   + $"Total: $ {(CalcularValorTotal()).ToString("F2", CultureInfo.InvariantCulture)}";
        }
    }
}