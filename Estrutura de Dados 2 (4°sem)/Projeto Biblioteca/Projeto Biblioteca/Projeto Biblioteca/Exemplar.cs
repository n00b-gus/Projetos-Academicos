using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Biblioteca
{
    internal class Exemplar
    {
        private int tombo;
        private List<Emprestimo> emprestimos;

        public Exemplar (int tombo)
        {
            this.tombo = tombo;
            this.emprestimos = new List<Emprestimo>();
        }

        public int Tombo => tombo;
        public List<Emprestimo> Emprestimos => emprestimos;

        public bool emprestar()
        {
            if (disponivel())
            {
                emprestimos.Add(new Emprestimo());
                return true;
            }
            return false;
        }

        public bool devolver()
        {
            if (!disponivel())
            {
                Emprestimo ativo = emprestimos.FirstOrDefault(e => e.DtDevolucao == DateTime.MinValue);
                if (ativo != null)
                {
                    ativo.DtDevolucao = DateTime.Now;
                    return true;
                }
            }
            return false;
        }

        public bool disponivel()
        {
            if (emprestimos.Count == 0)
            {
                return true;
            }

            return emprestimos.Last().DtDevolucao != DateTime.MinValue;
        }

        public int qtdeEmprestimos()
        {
            return emprestimos.Count;
        }
    }
}
