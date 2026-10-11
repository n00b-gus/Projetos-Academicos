using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Restaurante
{
    internal class Restaurante
    {
        private int proxPedido;
        private Pedido[] pedidos;
        private int qtdPedidos;

        public Restaurante()
        {
            this.proxPedido = 1;
            this.pedidos = new Pedido[50];
            this.qtdPedidos = 0;
        }

        public int ProxPedido => proxPedido;
        public int QtdPedidos => qtdPedidos;
        public Pedido[] Pedidos => pedidos;

        public bool NovoPedido(Pedido pedido)
        {
            if (qtdPedidos < 50)
            {
                pedidos[qtdPedidos] = pedido;
                qtdPedidos++;
                proxPedido++;
                return true;
            }
            return false;
        }

        public Pedido BuscarPedido(Pedido pedido)
        {
            for (int i = 0; i < qtdPedidos; i++)
            {
                if (pedidos[i].Id == pedido.Id)
                {
                    return pedidos[i];
                }
            }
            return null;
        }

        public bool CancelarPedido(Pedido pedido)
        {
            for (int i = 0; i < qtdPedidos; i++)
            {
                if (pedidos[i].Id == pedido.Id)
                {
                    for (int j = i; j < qtdPedidos - 1; j++)
                    {
                        pedidos[j] = pedidos[j + 1];
                    }
                    pedidos[qtdPedidos - 1] = null;
                    qtdPedidos--;
                    return true;
                }
            }
            return false;
        }
    }
}
