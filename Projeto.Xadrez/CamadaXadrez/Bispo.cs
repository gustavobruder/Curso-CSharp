﻿using Projeto.Xadrez.CamadaTabuleiro;

namespace Projeto.Xadrez.CamadaXadrez;

public class Bispo : Peca
{
    public Bispo(Tabuleiro tabuleiro, Cor cor) : base(tabuleiro, cor)
    {
    }

    public override string ToString() => "B";

    public override bool[,] MovimentosPossiveis()
    {
        var matriz = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

        var posicao = new Posicao(0, 0);

        // NO
        posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
        {
            matriz[posicao.Linha, posicao.Coluna] = true;
            var peca = Tabuleiro.Peca(posicao);

            if (peca != null && peca.Cor != Cor)
                break;

            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna - 1);
        }

        // NE
        posicao.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
        {
            matriz[posicao.Linha, posicao.Coluna] = true;
            var peca = Tabuleiro.Peca(posicao);

            if (peca != null && peca.Cor != Cor)
                break;

            posicao.DefinirValores(posicao.Linha - 1, posicao.Coluna + 1);
        }

        // SE
        posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
        while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
        {
            matriz[posicao.Linha, posicao.Coluna] = true;
            var peca = Tabuleiro.Peca(posicao);

            if (peca != null && peca.Cor != Cor)
                break;

            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna + 1);
        }

        // SO
        posicao.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
        while (Tabuleiro.PosicaoValida(posicao) && PodeMover(posicao))
        {
            matriz[posicao.Linha, posicao.Coluna] = true;
            var peca = Tabuleiro.Peca(posicao);

            if (peca != null && peca.Cor != Cor)
                break;

            posicao.DefinirValores(posicao.Linha + 1, posicao.Coluna - 1);
        }

        return matriz;
    }

    private bool PodeMover(Posicao posicao)
    {
        var peca = Tabuleiro.Peca(posicao);
        return peca == null || peca.Cor != Cor;
    }
}