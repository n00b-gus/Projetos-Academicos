using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Agenda
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Contatos agenda = new Contatos();
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("\n==============================");
                Console.WriteLine("        AGENDA PESSOAL        ");
                Console.WriteLine("==============================");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Adicionar contato");
                Console.WriteLine("2. Pesquisar contato");
                Console.WriteLine("3. Alterar contato");
                Console.WriteLine("4. Remover contato");
                Console.WriteLine("5. Listar contatos");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção Inválida!");
                    continue;
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;

                    case 1:
                        Console.WriteLine("\n--- Adicionar Contato ---");
                        Console.Write("E-mail: ");
                        string email = Console.ReadLine();
                        Console.Write("Nome: ");
                        string nome = Console.ReadLine();

                        Console.Write("Dia de nascimento: ");
                        int dia = int.Parse(Console.ReadLine());
                        Console.Write("Mês de nascimento: ");
                        int mes = int.Parse(Console.ReadLine());
                        Console.Write("Ano de nascimento: ");
                        int ano = int.Parse(Console.ReadLine());

                        Data dtNasc = new Data(dia, mes, ano);
                        Contato novoContato = new Contato(email, nome, dtNasc);

                        Console.Write("Deseja cadastrar um telefone? (s/n): ");
                        while (Console.ReadLine().Trim().ToLower() == "s")
                        {
                            Console.Write("Tipo (Celular, Fixo, Trabalho): ");
                            string tipo = Console.ReadLine();
                            Console.Write("Número: ");
                            string numero = Console.ReadLine();
                            Console.Write("É o telefone principal? (s/n): ");
                            bool principal = Console.ReadLine().Trim().ToLower() == "s";

                            novoContato.adicionarTelefone(new Telefone(tipo, numero, principal));
                            Console.Write("Deseja adicionar mais um telefone? (s/n): ");
                        }

                        if (agenda.Adicionar(novoContato))
                        {
                            Console.WriteLine("Contato adicionado com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Erro: Já existe um contato com esse e-mail na agenda.");
                        }
                        break;

                    case 2:
                        Console.WriteLine("\n--- Pesquisar Contato ---");
                        Console.Write("Informe o e-mail do contato: ");
                        string emailBusca = Console.ReadLine();
                        Contato contatoEncontrado = agenda.Pesquisar(new Contato(emailBusca));

                        if (contatoEncontrado != null)
                        {
                            Console.WriteLine("\nContato encontrado:");
                            Console.WriteLine(contatoEncontrado.ToString());
                            Console.WriteLine("Todos os telefones:");
                            foreach (var t in contatoEncontrado.Telefones)
                            {
                                Console.WriteLine($" - {t.Numero} ({t.Tipo}) {(t.Principal ? "[Principal]" : "")}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado.");
                        }
                        break;

                    case 3:
                        Console.WriteLine("\n--- Alterar Contato ---");
                        Console.Write("Informe o e-mail do contato que deseja alterar: ");
                        string emailAlt = Console.ReadLine();
                        Contato contatoParaAlt = agenda.Pesquisar(new Contato(emailAlt));

                        if (contatoParaAlt != null)
                        {
                            Console.Write("Novo Nome: ");
                            string novoNome = Console.ReadLine();

                            Console.Write("Novo Dia de nascimento: ");
                            int nDia = int.Parse(Console.ReadLine());
                            Console.Write("Novo Mês de nascimento: ");
                            int nMes = int.Parse(Console.ReadLine());
                            Console.Write("Novo Ano de nascimento: ");
                            int nAno = int.Parse(Console.ReadLine());

                            Contato dadosAtualizados = new Contato(emailAlt, novoNome, new Data(nDia, nMes, nAno));
                            if (agenda.Alterar(dadosAtualizados))
                            {
                                Console.WriteLine("Contato atualizado com sucesso!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado para alteração.");
                        }
                        break;

                    case 4:
                        Console.WriteLine("\n--- Remover Contato ---");
                        Console.Write("Informe o e-mail do contato a ser removido: ");
                        string emailRem = Console.ReadLine();

                        if (agenda.Remover(new Contato(emailRem)))
                        {
                            Console.WriteLine("Contato removido com sucesso!");
                        }
                        else
                        {
                            Console.WriteLine("Contato não encontrado.");
                        }
                        break;

                    case 5:
                        Console.WriteLine("\n--- Lista Geral de Contatos ---");
                        if (agenda.Agenda.Count == 0)
                        {
                            Console.WriteLine("A agenda está vazia.");
                        }
                        else
                        {
                            foreach (var c in agenda.Agenda)
                            {
                                Console.WriteLine(c.ToString());
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;
                }
            }
        }
    }
}
