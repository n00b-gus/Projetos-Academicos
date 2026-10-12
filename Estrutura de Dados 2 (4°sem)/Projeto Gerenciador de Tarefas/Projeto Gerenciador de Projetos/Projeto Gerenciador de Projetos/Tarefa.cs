using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Gerenciador_de_Projetos
{
    internal class Tarefa
    {
        private int id;
        private string titulo;
        private string descricao;
        private int prioridade;
        private string status;
        private DateTime dataCriacao;
        private DateTime? dataConclusao;

        public Tarefa(int id, string titulo, string descricao, int prioridade)
        {
            this.id = id;
            this.titulo = titulo;
            this.descricao = descricao;
            this.prioridade = prioridade;
            this.status = "Aberta"; // Regra implícita: toda tarefa nasce "Aberta"
            this.dataCriacao = DateTime.Now;
            this.dataConclusao = null;
        }

        public Tarefa(int id) : this(id, "", "", 3) { }

        public int Id => id;
        public string Titulo => titulo;
        public string Descricao => descricao;
        public int Prioridade => prioridade;
        public string Status => status;

        public void concluir()
        {
            this.status = "Fechada";
            this.dataConclusao = DateTime.Now;
        }

        public void cancelar()
        {
            this.status = "Cancelada";
        }

        public void reabrir()
        {
            this.status = "Aberta";
            this.dataConclusao = null; // Limpa a data de conclusão
        }

        public override bool Equals(object obj)
        {
            if (obj is Tarefa outra) return this.id == outra.id;
            return false;
        }

        public override int GetHashCode() => id.GetHashCode();

        public override string ToString()
        {
            string prioStr = prioridade == 1 ? "Alta" : prioridade == 2 ? "Média" : "Baixa";
            string conc = dataConclusao.HasValue ? dataConclusao.Value.ToString("g") : "N/A";
            return $"[{id}] {titulo} - {status} | Prioridade: {prioStr} | Criada: {dataCriacao:g} | Concluída: {conc}";
        }
    }

}
