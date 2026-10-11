using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Agenda
{
    internal class Contatos
    {
        private readonly List<Contato> agenda;

        public Contatos()
        {
            this.agenda = new List<Contato>();
        }

        public List<Contato> Agenda => agenda;

        public bool Adicionar (Contato c)
        {
            if (Pesquisar(c) != null)
            {
                return false;
            }
            agenda.Add(c);
            return true;
        }

        public Contato Pesquisar(Contato c)
        {
            return agenda.Find(item => item.Equals(c));
        }

        public bool Alterar(Contato c)
        {
            Contato existente = Pesquisar(c);
            if (existente != null)
            {
                existente.Nome = c.Nome;
                existente.DtNasc = c.DtNasc;
                return true;
            }
            return false;
        }

        public bool Remover(Contato c)
        {
            Contato existente = Pesquisar(c);
            if (existente != null)
            {
                return agenda.Remove(existente);
            }
            return false;
        }
    }
}
