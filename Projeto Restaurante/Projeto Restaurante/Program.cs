using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Restaurante
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Restaurante restaurante = new Restaurante();
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("\n--- SISTEMA DE GESTÃO DE PEDIDOS ---");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Criar novo pedido");
                Console.WriteLine("2. Adicionar item ao pedido");
                Console.WriteLine("3. Remover item do pedido");
                Console.WriteLine("4. Consultar pedido");
                Console.WriteLine("5. Cancelar pedido");
                Console.WriteLine("6. Listar todos os pedidos");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida! DIgite um número inteiro.");
                    continue;
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;

                    case 1:
                        Console.WriteLine("Informe o nome do cliente: ");
                        String nomeCliente = Console.ReadLine();
                        Pedido novo = new Pedido(restaurante.ProxPedido, nomeCliente);
                        if (restaurante.NovoPedido(novo))
                        {
                            Console.WriteLine($"Pedido #{novo.Id} criado com sucesso");
                        }
                        else
                        {
                            Console.WriteLine("Erro! Limite de pedidos atingido!");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Informe o ID do pedido: ");
                        if (int.TryParse(Console.ReadLine(), out int idPedidoAdd))
                        {
                            Pedido pedidoAlvo = restaurante.BuscarPedido(new Pedido(idPedidoAdd));
                            if (pedidoAlvo != null)
                            {
                                Console.WriteLine("ID do Item: ");
                                int idItem = int.Parse(Console.ReadLine());
                                Console.WriteLine("Descrição do item: ");
                                string descItem = Console.ReadLine();
                                Console.WriteLine("Preço: ");
                                double precoItem = double.Parse(Console.ReadLine());

                                Item itemAdicionar = new Item(idItem, descItem, precoItem);
                                if (pedidoAlvo.adicionarItem(itemAdicionar))
                                {
                                    Console.WriteLine("Item adicionado com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Limite de itens por pedido atingido!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Pedido não encontrado!");

                            }
                        }
                        break;

                    case 3:
                        Console.WriteLine("Informe o ID do pedido: ");
                        if (int.TryParse(Console.ReadLine(), out int idPedidoRemov))
                        {
                            Pedido pedidoAlvo = restaurante.BuscarPedido(new Pedido(idPedidoRemov));
                            if (pedidoAlvo != null)
                            {
                                Console.WriteLine("Informe o Id do Item a ser removido: ");
                                int idItem = int.Parse(Console.ReadLine());
                                if (pedidoAlvo.removerItem(new Item(idItem, "", 0)))
                                {
                                    Console.WriteLine("Item removido com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Item não encontrado!");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Pedido não encontrado!");
                            }
                        }
                        break;

                    case 4:
                        Console.WriteLine("Informe o ID do pedido: ");
                        if (int.TryParse(Console.ReadLine(), out int idPedidoConsult))
                        {
                            Pedido pedidoAlvo = restaurante.BuscarPedido(new Pedido(idPedidoConsult));
                            if (pedidoAlvo != null)
                            {
                                Console.WriteLine("\n" + pedidoAlvo.dadosDoPedido());
                            }
                            else
                            {
                                Console.WriteLine("Pedido não encontrado!");
                            }
                        }
                        break;

                    case 5:
                        Console.WriteLine("Informe o ID do pedido a ser cancelado: ");
                        if (int.TryParse(Console.ReadLine(), out int idPedidoCancel))
                        {
                            if (restaurante.CancelarPedido(new Pedido(idPedidoCancel)))
                            {
                                Console.WriteLine("Pedido cancelado com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Pedido não encontrado!");
                            }
                        }
                        break;

                    case 6:
                        Console.WriteLine("\n--- PEDIDOS ---");
                        if (restaurante.QtdPedidos == 0)
                        {
                            Console.WriteLine("Nenhum pedido registrado!");
                        }
                        else
                        {
                            double somaGeral = 0;
                            for (int i = 0; i < restaurante.QtdPedidos; i++)
                            {
                                Pedido p = restaurante.Pedidos[i];
                                double total = p.calcularTotal();
                                somaGeral += total;
                                Console.WriteLine($"Pedido #{p.Id} - Cliente: {p.Cliente} | Total: R$ {total:F2}");
                            }
                            Console.WriteLine($"\nSoma total: R$ {somaGeral:F2}");
                        }
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }
    }
}
