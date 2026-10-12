using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Biblioteca
{
    internal class Emprestimo
    {
        private DateTime dtEmprestimo;
        private DateTime dtDevolucao;

        public Emprestimo ()
        {
            this.dtEmprestimo = DateTime.Now;
            this.dtDevolucao = DateTime.MinValue;
        }

        public DateTime DtEmprestimo => dtEmprestimo;
        public DateTime DtDevolucao
        {
            get => dtDevolucao;
            set => dtDevolucao = value;
        }

    }
}
