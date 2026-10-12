using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Gerenciador_de_Projetos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Projetos repositorio = new Projetos();
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("\n=== SISTEMA DE GERENCIAMENTO DE PROJETOS ===");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Adicionar projeto");
                Console.WriteLine("2. Pesquisar projeto (mostrar tarefas por status e totais)");
                Console.WriteLine("3. Remover projeto (apenas se sem tarefas)");
                Console.WriteLine("4. Adicionar tarefa em projeto");
                Console.WriteLine("5. Concluir tarefa");
                Console.WriteLine("6. Cancelar tarefa");
                Console.WriteLine("7. Reabrir tarefa");
                Console.WriteLine("8. Listar tarefas de um projeto");
                Console.WriteLine("9. Filtrar tarefas por status ou prioridade em um projeto");
                Console.WriteLine("10. Filtrar tarefas por status ou prioridade em todos os projetos");
                Console.WriteLine("11. Resumo geral da empresa");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida! Digite um número inteiro.");
                    continue;
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando o sistema...");
                        break;

                    case 1:
                        Console.Write("ID do Projeto: ");
                        int idProj = int.Parse(Console.ReadLine());
                        Console.Write("Nome do Projeto: ");
                        string nomeProj = Console.ReadLine();
                        if (repositorio.adicionar(new Projeto(idProj, nomeProj)))
                            Console.WriteLine("Projeto adicionado com sucesso!");
                        else
                            Console.WriteLine("Erro: Já existe um projeto com este ID.");
                        break;

                    case 2:
                        Console.Write("Informe o ID do Projeto para pesquisa: ");
                        Projeto pPesquisa = repositorio.buscar(new Projeto(int.Parse(Console.ReadLine())));
                        if (pPesquisa != null)
                        {
                            Console.WriteLine($"\n--- Projeto: {pPesquisa.Nome} ---");
                            Console.WriteLine($"Total Abertas: {pPesquisa.totalAbertas()}");
                            Console.WriteLine($"Total Fechadas: {pPesquisa.totalFechadas()}");
                            Console.WriteLine($"Total Canceladas: {pPesquisa.totalCanceladas()}");
                            Console.WriteLine("\nTarefas:");
                            foreach (var t in pPesquisa.Tarefas.OrderBy(t => t.Status))
                            {
                                Console.WriteLine(t.ToString());
                            }
                        }
                        else Console.WriteLine("Projeto não encontrado.");
                        break;

                    case 3:
                        Console.Write("Informe o ID do Projeto a remover: ");
                        Projeto pRemover = repositorio.buscar(new Projeto(int.Parse(Console.ReadLine())));
                        if (pRemover != null)
                        {
                            if (repositorio.remover(pRemover))
                                Console.WriteLine("Projeto removido com sucesso!");
                            else
                                Console.WriteLine("Erro: O projeto não pode ser removido porque ainda possui tarefas.");
                        }
                        else Console.WriteLine("Projeto não encontrado.");
                        break;

                    case 4:
                        Console.Write("Informe o ID do Projeto: ");
                        Projeto pAddTar = repositorio.buscar(new Projeto(int.Parse(Console.ReadLine())));
                        if (pAddTar != null)
                        {
                            Console.Write("ID da Tarefa: ");
                            int idTar = int.Parse(Console.ReadLine());
                            Console.Write("Título: ");
                            string titTar = Console.ReadLine();
                            Console.Write("Descrição: ");
                            string descTar = Console.ReadLine();
                            Console.Write("Prioridade (1=Alta, 2=Média, 3=Baixa): ");
                            int prioTar = int.Parse(Console.ReadLine());

                            pAddTar.adicionarTarefa(new Tarefa(idTar, titTar, descTar, prioTar));
                            Console.WriteLine("Tarefa adicionada (status inicial: Aberta).");
                        }
                        else Console.WriteLine("Projeto não encontrado.");
                        break;

                    case 5:
                    case 6:
                    case 7:
                        Console.Write("Informe o ID do Projeto: ");
                        Projeto pAlt = repositorio.buscar(new Projeto(int.Parse(Console.ReadLine())));
                        if (pAlt != null)
                        {
                            Console.Write("Informe o ID da Tarefa: ");
                            Tarefa tAlt = pAlt.buscarTarefa(new Tarefa(int.Parse(Console.ReadLine())));
                            if (tAlt != null)
                            {
                                if (opcao == 5) { tAlt.concluir(); Console.WriteLine("Tarefa marcada como Fechada."); }
                                else if (opcao == 6) { tAlt.cancelar(); Console.WriteLine("Tarefa marcada como Cancelada."); }
                                else if (opcao == 7) { tAlt.reabrir(); Console.WriteLine("Tarefa reaberta."); }
                            }
                            else Console.WriteLine("Tarefa não encontrada neste projeto.");
                        }
                        else Console.WriteLine("Projeto não encontrado.");
                        break;

                    case 8:
                        Console.Write("Informe o ID do Projeto: ");
                        Projeto pList = repositorio.buscar(new Projeto(int.Parse(Console.ReadLine())));
                        if (pList != null)
                        {
                            if (pList.Tarefas.Count == 0) Console.WriteLine("Nenhuma tarefa neste projeto.");
                            else foreach (var t in pList.Tarefas) Console.WriteLine(t.ToString());
                        }
                        else Console.WriteLine("Projeto não encontrado.");
                        break;

                    case 9:
                        Console.Write("Informe o ID do Projeto: ");
                        Projeto pFiltro = repositorio.buscar(new Projeto(int.Parse(Console.ReadLine())));
                        if (pFiltro != null)
                        {
                            Console.WriteLine("Filtrar por: 1 - Status, 2 - Prioridade");
                            string tipoFiltro = Console.ReadLine();
                            if (tipoFiltro == "1")
                            {
                                Console.Write("Status (Aberta, Fechada, Cancelada): ");
                                string stat = Console.ReadLine();
                                var filtradas = pFiltro.tarefasPorStatus(stat);
                                foreach (var t in filtradas) Console.WriteLine(t.ToString());
                            }
                            else if (tipoFiltro == "2")
                            {
                                Console.Write("Prioridade (1, 2 ou 3): ");
                                int p = int.Parse(Console.ReadLine());
                                var filtradas = pFiltro.tarefasPorPrioridade(p);
                                foreach (var t in filtradas) Console.WriteLine(t.ToString());
                            }
                        }
                        else Console.WriteLine("Projeto não encontrado.");
                        break;

                    case 10:
                        Console.WriteLine("Filtrar globalmente por: 1 - Status, 2 - Prioridade");
                        string tipoFiltroG = Console.ReadLine();
                        foreach (var proj in repositorio.listar())
                        {
                            Console.WriteLine($"\n--- Projeto: {proj.Nome} ---");
                            if (tipoFiltroG == "1")
                            {
                                Console.Write("Status (Aberta, Fechada, Cancelada): ");
                                string stat = Console.ReadLine();
                                foreach (var t in proj.tarefasPorStatus(stat)) Console.WriteLine(t.ToString());
                            }
                            else if (tipoFiltroG == "2")
                            {
                                Console.Write("Prioridade (1, 2 ou 3): ");
                                int p = int.Parse(Console.ReadLine());
                                foreach (var t in proj.tarefasPorPrioridade(p)) Console.WriteLine(t.ToString());
                            }
                        }
                        break;

                    case 11:
                        int totalProj = repositorio.Itens.Count;
                        int abertas = 0, fechadas = 0, canceladas = 0;

                        foreach (var proj in repositorio.Itens)
                        {
                            abertas += proj.totalAbertas();
                            fechadas += proj.totalFechadas();
                            canceladas += proj.totalCanceladas();
                        }

                        double percConcluidas = 0;
                        if (abertas + fechadas > 0)
                        {
                            // Calcula % de concluídas em relação ao montante de (Abertas + Fechadas)
                            percConcluidas = ((double)fechadas / (abertas + fechadas)) * 100;
                        }

                        Console.WriteLine("\n=== RESUMO GERAL DA EMPRESA ===");
                        Console.WriteLine($"Total de Projetos: {totalProj}");
                        Console.WriteLine($"Tarefas Abertas: {abertas}");
                        Console.WriteLine($"Tarefas Fechadas: {fechadas}");
                        Console.WriteLine($"Tarefas Canceladas: {canceladas}");
                        Console.WriteLine($"% Concluídas (em relação às ativas/abertas): {percConcluidas:F2}%");
                        break;

                    default:
                        Console.WriteLine("Opção inválida! Escolha um valor entre 0 e 11.");
                        break;
                }
            }
        }
    }
}
