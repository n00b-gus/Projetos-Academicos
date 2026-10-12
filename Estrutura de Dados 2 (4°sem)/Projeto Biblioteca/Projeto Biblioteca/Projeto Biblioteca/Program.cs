using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Livros acervo = new Livros();
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("\n=== SISTEMA DE BIBLIOTECA ===");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Adicionar livro");
                Console.WriteLine("2. Pesquisar livro (sintético)");
                Console.WriteLine("3. Pesquisar livro (analítico)");
                Console.WriteLine("4. Adicionar exemplar");
                Console.WriteLine("5. Registrar empréstimo");
                Console.WriteLine("6. Registrar devolução");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida! Digite um número.");
                    continue;
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo ...");
                        break;

                    case 1:
                        Console.Write("ISBN: ");
                        int isbn = int.Parse(Console.ReadLine());
                        Console.Write("Título: ");
                        string titulo = Console.ReadLine();
                        Console.Write("Autor: ");
                        string autor = Console.ReadLine();
                        Console.Write("Editora: ");
                        string editora = Console.ReadLine();
                        acervo.adicionar(new Livro(isbn, titulo, autor, editora));
                        Console.WriteLine("Livro adicionado (caso não existisse).");
                        break;

                    case 2:
                        {
                            Console.Write("Informe o ISBN para pesquisa sintética: ");
                            Livro l = acervo.pesquisar(new Livro(int.Parse(Console.ReadLine())));
                            if (l != null)
                            {
                                Console.WriteLine($"\n--- {l.Titulo} ({l.Autor}) ---");
                                Console.WriteLine($"Total de Exemplares: {l.qtdeExemplares()}");
                                Console.WriteLine($"Disponíveis: {l.qtdeDisponiveis()}");
                                Console.WriteLine($"Total de Empréstimos: {l.qtdeEmprestimos()}");
                                Console.WriteLine($"Disponibilidade: {l.percDisponibilidade():F2}%");
                            }
                            else Console.WriteLine("Livro não encontrado.");
                        }
                        break;

                    case 3:
                        {
                            Console.Write("Informe o ISBN para pesquisa analítica: ");
                            Livro l = acervo.pesquisar(new Livro(int.Parse(Console.ReadLine())));
                            if (l != null)
                            {
                                Console.WriteLine($"\n--- {l.Titulo} ({l.Autor}) ---");
                                Console.WriteLine($"Total de Exemplares: {l.qtdeExemplares()} | Disponíveis: {l.qtdeDisponiveis()} | Empréstimos: {l.qtdeEmprestimos()} | {l.percDisponibilidade():F2}%");
                                Console.WriteLine("\nExemplares:");
                                foreach (var ex in l.Exemplares)
                                {
                                    string status = ex.disponivel() ? "Disponível" : "Emprestado";
                                    Console.WriteLine($" -> Tombo {ex.Tombo} [{status}] - Histórico de Empréstimos: {ex.qtdeEmprestimos()}");
                                    foreach (var emp in ex.Emprestimos)
                                    {
                                        string devolucao = emp.DtDevolucao == DateTime.MinValue ? "Pendente" : emp.DtDevolucao.ToString("g");
                                        Console.WriteLine($"      Saída: {emp.DtEmprestimo:g} | Devolução: {devolucao}");
                                    }
                                }
                            }
                            else Console.WriteLine("Livro não encontrado.");
                        }
                        break;

                    case 4:
                        Console.Write("Informe o ISBN do livro: ");
                        Livro livroParaExemplar = acervo.pesquisar(new Livro(int.Parse(Console.ReadLine())));
                        if (livroParaExemplar != null)
                        {
                            Console.Write("Informe o número de Tombo do novo exemplar: ");
                            int tombo = int.Parse(Console.ReadLine());

                            // Verifica se o tombo já existe neste livro
                            if (livroParaExemplar.Exemplares.Any(e => e.Tombo == tombo))
                            {
                                Console.WriteLine("Erro: Este tombo já existe para este livro.");
                            }
                            else
                            {
                                livroParaExemplar.adicionarExemplar(new Exemplar(tombo));
                                Console.WriteLine("Exemplar adicionado com sucesso!");
                            }
                        }
                        else Console.WriteLine("Livro não encontrado.");
                        break;

                    case 5:
                        Console.Write("Informe o ISBN do livro: ");
                        Livro livroEmp = acervo.pesquisar(new Livro(int.Parse(Console.ReadLine())));
                        if (livroEmp != null)
                        {
                            // Encontra o primeiro exemplar disponível
                            Exemplar exDisponivel = livroEmp.Exemplares.FirstOrDefault(e => e.disponivel());
                            if (exDisponivel != null)
                            {
                                exDisponivel.emprestar();
                                Console.WriteLine($"Empréstimo registado com sucesso no exemplar Tombo {exDisponivel.Tombo}.");
                            }
                            else
                            {
                                Console.WriteLine("Não há exemplares disponíveis para este livro no momento.");
                            }
                        }
                        else Console.WriteLine("Livro não encontrado.");
                        break;

                    case 6:
                        Console.Write("Informe o ISBN do livro: ");
                        Livro livroDev = acervo.pesquisar(new Livro(int.Parse(Console.ReadLine())));
                        if (livroDev != null)
                        {
                            Console.Write("Informe o Tombo do exemplar a devolver: ");
                            int tomboDev = int.Parse(Console.ReadLine());
                            Exemplar exDev = livroDev.Exemplares.FirstOrDefault(e => e.Tombo == tomboDev);

                            if (exDev != null)
                            {
                                if (exDev.devolver())
                                    Console.WriteLine("Devolução registada com sucesso!");
                                else
                                    Console.WriteLine("Este exemplar não tem empréstimos ativos pendentes.");
                            }
                            else Console.WriteLine("Exemplar não encontrado.");
                        }
                        else Console.WriteLine("Livro não encontrado.");
                        break;

                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;
                }
            }
        }
    }
}
