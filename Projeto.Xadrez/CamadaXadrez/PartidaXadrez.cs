﻿using Projeto.Xadrez.CamadaTabuleiro;

namespace Projeto.Xadrez.CamadaXadrez;

public class PartidaXadrez
{
    public Tabuleiro Tabuleiro { get; private set; }
    public int Turno { get; private set; }
    public Cor JogadorAtual { get; private set; }
    public bool Terminada { get; private set; }
    public bool Xeque { get; private set; }
    public Peca VulneravelEnPassant { get; private set; }
    private readonly HashSet<Peca> _pecas;
    private readonly HashSet<Peca> _capturadas;

    public PartidaXadrez()
    {
        Tabuleiro = new Tabuleiro(8, 8);
        Turno = 1;
        JogadorAtual = Cor.Branca;
        Terminada = false;
        Xeque = false;
        VulneravelEnPassant = null;
        _pecas = new HashSet<Peca>();
        _capturadas = new HashSet<Peca>();

        InicializarPecas();
    }

    public void RealizaJogada(Posicao origem, Posicao destino)
    {
        var pecaCapturada = ExecutaMovimento(origem, destino);

        if (EstaEmXeque(JogadorAtual))
        {
            DesfazMovimento(origem, destino, pecaCapturada);
            throw new TabuleiroException("Você não pode se colocar em xeque!");
        }

        var peca = Tabuleiro.Peca(destino);

        // #jogadaespecial promocao
        if (peca is Peao)
        {
            if ((peca.Cor == Cor.Branca && destino.Linha == 0) || (peca.Cor == Cor.Preta && destino.Linha == 7))
            {
                peca = Tabuleiro.RetirarPeca(destino);
                _pecas.Remove(peca);
                var dama = new Dama(Tabuleiro, peca.Cor);
                Tabuleiro.ColocarPeca(dama, destino);
                _pecas.Add(dama);
            }
        }

        if (EstaEmXeque(Adversaria(JogadorAtual)))
            Xeque = true;
        else
            Xeque = false;

        if (TesteXequemate(Adversaria(JogadorAtual)))
        {
            Terminada = true;
        }
        else
        {
            Turno++;
            MudaJogador();
        }

        // #jogadaespecial en passant
        if (peca is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
        {
            VulneravelEnPassant = peca;
        }
        else
        {
            VulneravelEnPassant = null;
        }
    }

    private Peca ExecutaMovimento(Posicao origem, Posicao destino)
    {
        var peca = Tabuleiro.RetirarPeca(origem);
        peca.IncrementarQteMovimentos();

        var pecaCapturada = Tabuleiro.RetirarPeca(destino);
        Tabuleiro.ColocarPeca(peca, destino);

        if (pecaCapturada != null)
        {
            _capturadas.Add(pecaCapturada);
        }

        // #jogadaespecial roque pequeno
        if (peca is Rei && destino.Coluna == origem.Coluna + 2)
        {
            var origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            var destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            var pecaT = Tabuleiro.RetirarPeca(origemT);
            pecaT.IncrementarQteMovimentos();
            Tabuleiro.ColocarPeca(pecaT, destinoT);
        }

        // #jogadaespecial roque grande
        if (peca is Rei && destino.Coluna == origem.Coluna - 2)
        {
            var origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            var destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            var pecaT = Tabuleiro.RetirarPeca(origemT);
            pecaT.IncrementarQteMovimentos();
            Tabuleiro.ColocarPeca(pecaT, destinoT);
        }

        // #jogadaespecial en passant
        if (peca is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == null)
            {
                Posicao posicaoP;
                if (peca.Cor == Cor.Branca)
                    posicaoP = new Posicao(destino.Linha + 1, destino.Coluna);
                else
                    posicaoP = new Posicao(destino.Linha - 1, destino.Coluna);

                pecaCapturada = Tabuleiro.RetirarPeca(posicaoP);
                _capturadas.Add(pecaCapturada);
            }
        }

        return pecaCapturada;
    }

    private bool EstaEmXeque(Cor cor)
    {
        var rei = Rei(cor);

        if (rei == null)
        {
            throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
        }

        foreach (var peca in PecasEmJogo(Adversaria(cor)))
        {
            var matriz = peca.MovimentosPossiveis();

            if (matriz[rei.Posicao.Linha, rei.Posicao.Coluna])
                return true;
        }

        return false;
    }

    private bool TesteXequemate(Cor cor)
    {
        if (!EstaEmXeque(cor))
            return false;

        foreach (var peca in PecasEmJogo(cor))
        {
            var matriz = peca.MovimentosPossiveis();

            for (var i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (var j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (matriz[i, j])
                    {
                        var origem = peca.Posicao;
                        var destino = new Posicao(i, j);
                        var pecaCapturada = ExecutaMovimento(origem, destino);
                        var testeXeque = EstaEmXeque(cor);
                        DesfazMovimento(origem, destino, pecaCapturada);

                        if (!testeXeque)
                            return false;
                    }
                }
            }
        }

        return true;
    }

    private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
    {
        var peca = Tabuleiro.RetirarPeca(destino);
        peca.DecrementarQteMovimentos();

        if (pecaCapturada != null)
        {
            Tabuleiro.ColocarPeca(pecaCapturada, destino);
            _capturadas.Remove(pecaCapturada);
        }

        Tabuleiro.ColocarPeca(peca, origem);

        // #jogadaespecial roque pequeno
        if (peca is Rei && destino.Coluna == origem.Coluna + 2)
        {
            var origemT = new Posicao(origem.Linha, origem.Coluna + 3);
            var destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
            var pecaT = Tabuleiro.RetirarPeca(destinoT);
            pecaT.DecrementarQteMovimentos();
            Tabuleiro.ColocarPeca(pecaT, origemT);
        }

        // #jogadaespecial roque grande
        if (peca is Rei && destino.Coluna == origem.Coluna - 2)
        {
            var origemT = new Posicao(origem.Linha, origem.Coluna - 4);
            var destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
            var pecaT = Tabuleiro.RetirarPeca(destinoT);
            pecaT.DecrementarQteMovimentos();
            Tabuleiro.ColocarPeca(pecaT, origemT);
        }

        // #jogadaespecial en passant
        if (peca is Peao)
        {
            if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
            {
                var peao = Tabuleiro.RetirarPeca(destino);

                Posicao posicaoP;
                if (peca.Cor == Cor.Branca)
                    posicaoP = new Posicao(3, destino.Coluna);
                else
                    posicaoP = new Posicao(4, destino.Coluna);

                Tabuleiro.ColocarPeca(peao, posicaoP);
            }
        }
    }

    private Cor Adversaria(Cor cor)
    {
        if (cor == Cor.Branca)
            return Cor.Preta;
        else
            return Cor.Branca;
    }

    private void MudaJogador()
    {
        if (JogadorAtual == Cor.Branca)
            JogadorAtual = Cor.Preta;
        else
            JogadorAtual = Cor.Branca;
    }

    public HashSet<Peca> PecasCapturadas(Cor cor)
    {
        var aux = new HashSet<Peca>();

        foreach (var peca in _capturadas)
        {
            if (peca.Cor == cor)
                aux.Add(peca);
        }

        return aux;
    }

    private HashSet<Peca> PecasEmJogo(Cor cor)
    {
        var aux = new HashSet<Peca>();

        foreach (var peca in _pecas)
        {
            if (peca.Cor == cor)
                aux.Add(peca);
        }

        aux.ExceptWith(PecasCapturadas(cor));
        return aux;
    }

    private Peca Rei(Cor cor)
    {
        foreach (var peca in PecasEmJogo(cor))
        {
            if (peca is Rei)
                return peca;
        }

        return null;
    }

    public void ValidarPosicaoDeOrigem(Posicao posicao)
    {
        var peca = Tabuleiro.Peca(posicao);

        if (peca == null)
        {
            throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
        }

        if (JogadorAtual != peca.Cor)
        {
            throw new TabuleiroException("A peça de origem escolhida não é sua!");
        }

        if (!peca.ExisteMovimentosPossiveis())
        {
            throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
        }
    }

    public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
    {
        if (!Tabuleiro.Peca(origem).MovimentoPossivel(destino))
        {
            throw new TabuleiroException("Posição de destino inválida!");
        }
    }

    private void InicializarPecas()
    {
        ColocarNovaPeca('a', 1, new Torre(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('b', 1, new Cavalo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('c', 1, new Bispo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('d', 1, new Dama(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('e', 1, new Rei(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('f', 1, new Bispo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('g', 1, new Cavalo(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('h', 1, new Torre(Tabuleiro, Cor.Branca));
        ColocarNovaPeca('a', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('b', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('c', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('d', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('e', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('f', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('g', 2, new Peao(Tabuleiro, Cor.Branca, this));
        ColocarNovaPeca('h', 2, new Peao(Tabuleiro, Cor.Branca, this));

        ColocarNovaPeca('a', 8, new Torre(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('b', 8, new Cavalo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('c', 8, new Bispo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('d', 8, new Dama(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('e', 8, new Rei(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('f', 8, new Bispo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('g', 8, new Cavalo(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('h', 8, new Torre(Tabuleiro, Cor.Preta));
        ColocarNovaPeca('a', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('b', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('c', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('d', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('e', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('f', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('g', 7, new Peao(Tabuleiro, Cor.Preta, this));
        ColocarNovaPeca('h', 7, new Peao(Tabuleiro, Cor.Preta, this));
    }

    private void ColocarNovaPeca(char coluna, int linha, Peca peca)
    {
        Tabuleiro.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
        _pecas.Add(peca);
    }
}