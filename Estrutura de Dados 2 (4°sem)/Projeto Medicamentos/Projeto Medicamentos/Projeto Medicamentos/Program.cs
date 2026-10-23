using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Medicamentos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Medicamentos farmacia = new Medicamentos();
            int opcao = -1;

            do
            {
                Console.WriteLine("\n==================================================");
                Console.WriteLine("       SISTEMA DE CONTROLE DE MEDICAMENTOS        ");
                Console.WriteLine("==================================================");
                Console.WriteLine("0. Finalizar processo");
                Console.WriteLine("1. Cadastrar medicamento");
                Console.WriteLine("2. Consultar medicamento (sintético)");
                Console.WriteLine("3. Consultar medicamento (analítico)");
                Console.WriteLine("4. Comprar medicamento (cadastrar lote)");
                Console.WriteLine("5. Vender medicamento (abater do lote mais antigo)");
                Console.WriteLine("6. Listar medicamentos (dados sintéticos)");
                Console.WriteLine("==================================================");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("\n[Erro] Digite um número válido!");
                    continue;
                }

                Console.WriteLine();

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando o sistema... Até logo!");
                        break;

                    case 1:
                        Console.Write("Digite o ID do medicamento: ");
                        int idCadastro = int.Parse(Console.ReadLine());

                        Medicamento jaExiste = farmacia.pesquisar(new Medicamento(idCadastro, "", ""));
                        if (jaExiste.Id != 0)
                        {
                            Console.WriteLine("[Aviso] Já existe um medicamento cadastrado com este ID!");
                            break;
                        }

                        Console.Write("Digite o Nome do medicamento: ");
                        string nome = Console.ReadLine();

                        Console.Write("Digite o Laboratório: ");
                        string lab = Console.ReadLine();

                        farmacia.adicionar(new Medicamento(idCadastro, nome, lab));
                        Console.WriteLine("[Sucesso] Medicamento cadastrado com sucesso!");
                        break;

                    case 2:
                        Console.Write("Digite o ID do medicamento para consulta sintética: ");
                        int idSintetico = int.Parse(Console.ReadLine());

                        Medicamento medSintetico = farmacia.pesquisar(new Medicamento(idSintetico, "", ""));
                        if (medSintetico.Id == 0)
                        {
                            Console.WriteLine("[Aviso] Medicamento não encontrado.");
                        }
                        else
                        {
                            Console.WriteLine("Dados do Medicamento (ID-Nome-Laboratório-Qtde):");
                            Console.WriteLine(medSintetico.toString());
                        }
                        break;

                    case 3:
                        Console.Write("Digite o ID do medicamento para consulta analítica: ");
                        int idAnalitico = int.Parse(Console.ReadLine());

                        Medicamento medAnalitico = farmacia.pesquisar(new Medicamento(idAnalitico, "", ""));
                        if (medAnalitico.Id == 0)
                        {
                            Console.WriteLine("[Aviso] Medicamento não encontrado.");
                        }
                        else
                        {
                            Console.WriteLine("Dados do Medicamento (ID-Nome-Laboratório-Qtde):");
                            Console.WriteLine(medAnalitico.toString());
                            Console.WriteLine("--- Lotes na Fila (ID-Qtde-Vencimento) ---");

                            if (medAnalitico.Lotes.Count == 0)
                            {
                                Console.WriteLine("Nenhum lote disponível no momento.");
                            }
                            else
                            {
                                foreach (Lote lote in medAnalitico.Lotes)
                                {
                                    Console.WriteLine("  -> " + lote.toString());
                                }
                            }
                        }
                        break;

                    case 4:
                        Console.Write("Digite o ID do medicamento que deseja comprar: ");
                        int idCompra = int.Parse(Console.ReadLine());

                        Medicamento medCompra = farmacia.pesquisar(new Medicamento(idCompra, "", ""));
                        if (medCompra.Id == 0)
                        {
                            Console.WriteLine("[Erro] Medicamento não encontrado. Cadastre o medicamento primeiro (Opção 1).");
                        }
                        else
                        {
                            Console.Write("Digite o ID do Lote: ");
                            int idLote = int.Parse(Console.ReadLine());

                            Console.Write("Digite a Quantidade de unidades do lote: ");
                            int qtdeLote = int.Parse(Console.ReadLine());

                            Console.Write("Digite a Data de Vencimento (dd/mm/aaaa): ");
                            DateTime vencLote;
                            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out vencLote))
                            {
                                Console.Write("Data inválida! Digite no formato dd/mm/aaaa: ");
                            }

                            Lote novoLote = new Lote(idLote, qtdeLote, vencLote);
                            medCompra.comprar(novoLote);
                            Console.WriteLine("[Sucesso] Lote adicionado à fila do medicamento!");
                        }
                        break;

                    case 5:
                        Console.Write("Digite o ID do medicamento a ser vendido: ");
                        int idVenda = int.Parse(Console.ReadLine());

                        Medicamento medVenda = farmacia.pesquisar(new Medicamento(idVenda, "", ""));
                        if (medVenda.Id == 0)
                        {
                            Console.WriteLine("[Erro] Medicamento não encontrado.");
                        }
                        else
                        {
                            Console.Write($"Quantidade disponível: {medVenda.qtdeDisponivel()}. Digite a quantidade para venda: ");
                            int qtdeVenda = int.Parse(Console.ReadLine());

                            bool vendaRealizada = medVenda.vender(qtdeVenda);
                            if (vendaRealizada)
                            {
                                Console.WriteLine("[Sucesso] Venda realizada! Estoque abatido dos lotes mais antigos.");
                            }
                            else
                            {
                                Console.WriteLine("[Erro] Venda não realizada. Estoque insuficiente ou quantidade inválida.");
                            }
                        }
                        break;

                    case 6:
                        Console.WriteLine("--- Lista de Todos os Medicamentos (ID-Nome-Laboratório-Qtde) ---");
                        if (farmacia.ListaMedicamentos.Count == 0)
                        {
                            Console.WriteLine("Nenhum medicamento cadastrado.");
                        }
                        else
                        {
                            foreach (Medicamento med in farmacia.ListaMedicamentos)
                            {
                                Console.WriteLine(med.toString());
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("[Aviso] Opção inválida! Escolha entre 0 e 6.");
                        break;
                }

            } while (opcao != 0);
        }
    }
}
