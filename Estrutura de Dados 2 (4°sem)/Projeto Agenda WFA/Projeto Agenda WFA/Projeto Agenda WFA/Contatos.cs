using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Agenda_WFA
{
    internal class Contatos
    {
        public List<Contato> Agenda { get; private set; }

        public Contatos() { Agenda = new List<Contato>(); }

        public bool Adicionar(Contato c)
        {
            if (Pesquisar(c) != null) return false;
            Agenda.Add(c);
            return true;
        }

        public Contato Pesquisar(Contato c) => Agenda.Find(item => item.Equals(c));

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
            if (existente != null) return Agenda.Remove(existente);
            return false;
        }
    }
}

