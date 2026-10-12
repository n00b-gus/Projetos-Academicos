using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Gerenciador_de_Projetos
{
    internal class Projeto
    {
        private int id;
        private string nome;
        private List<Tarefa> tarefas;

        public Projeto(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
            this.tarefas = new List<Tarefa>();
        }

        public Projeto(int id) : this(id, "") { }

        public int Id => id;
        public string Nome => nome;
        public List<Tarefa> Tarefas => tarefas;

        public void adicionarTarefa(Tarefa t)
        {
            if (buscarTarefa(t) == null) tarefas.Add(t);
        }

        public bool removerTarefa(Tarefa t)
        {
            Tarefa existente = buscarTarefa(t);
            if (existente != null)
            {
                tarefas.Remove(existente);
                return true;
            }
            return false;
        }

        public Tarefa buscarTarefa(Tarefa t)
        {
            return tarefas.FirstOrDefault(x => x.Equals(t));
        }

        public List<Tarefa> tarefasPorStatus(string s)
        {
            return tarefas.Where(t => t.Status.Equals(s, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Tarefa> tarefasPorPrioridade(int p)
        {
            return tarefas.Where(t => t.Prioridade == p).ToList();
        }

        public int totalAbertas()
        {
            return tarefas.Count(t => t.Status == "Aberta");
        }

        public int totalFechadas()
        {
            return tarefas.Count(t => t.Status == "Fechada");
        }

        public int totalCanceladas()
        {
            return tarefas.Count(t => t.Status == "Cancelada");
        }

        public override bool Equals(object obj)
        {
            if (obj is Projeto outro) return this.id == outro.id;
            return false;
        }

        public override int GetHashCode() => id.GetHashCode();
    }
}
