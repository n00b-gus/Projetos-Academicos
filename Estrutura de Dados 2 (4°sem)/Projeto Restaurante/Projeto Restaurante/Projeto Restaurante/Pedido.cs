using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Restaurante
{
    internal class Pedido
    {
        private int id;
        private string cliente;
        private Item[] itens;
        private int totalItens;

        public Pedido(int id, string cliente)
        {
            this.id = id;
            this.cliente = cliente;
            this.itens = new Item[10];
            this.totalItens = 0;
        }

        public Pedido(int id) : this(id, string.Empty) { }

        public int Id => id;
        public string Cliente => cliente;
        public Item[] Itens => itens;

        public bool adicionarItem(Item item)
        {
            if(totalItens < 10)
            {
                itens[totalItens] = item;
                totalItens++;
                return true;
            }
            return false;
        }

        public bool removerItem(Item item)
        {
            for (int i = 0; i < totalItens; i++)
            {
                if (itens[i].Id == item.Id)
                {
                    for (int j = i; j < totalItens - 1; j++)
                    {
                        itens[j] = itens[j + 1];
                    }
                    itens[totalItens - 1] = null;
                    totalItens--;
                    return true;
                }
            }
            return false;
        }

        public string dadosDoPedido()
        {
            string dados = $"Pedido #{id} | Cliente: {cliente}\n Itens:\n";
            if (totalItens == 0)
            {
                dados += "  (Nenhum item adicionado)\n";
                return dados;
            }
            else
            {
                for (int i = 0; i < totalItens; i++)
                {
                    dados += $"  - [{itens[i].Id} {itens[i].Descricao}: R$ {itens[i].Preco:F2}\n]";
                }
                dados += $"Valor Total: R$ {calcularTotal():F2}";
                return dados;
            }
        }

        public double calcularTotal()
        {
            double soma = 0;
            for (int i = 0; i < totalItens; i++)
            {
                soma += itens[i].Preco;
            }
            return soma;
        }

    }
}
