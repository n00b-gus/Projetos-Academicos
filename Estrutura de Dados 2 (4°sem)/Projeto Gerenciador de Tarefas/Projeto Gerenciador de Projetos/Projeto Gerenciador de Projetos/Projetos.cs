using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Gerenciador_de_Projetos
{
    internal class Projetos
    {
        private List<Projeto> itens;

        public Projetos()
        {
            itens = new List<Projeto>();
        }

        public List<Projeto> Itens => itens;

        public bool adicionar(Projeto p)
        {
            if (buscar(p) == null)
            {
                itens.Add(p);
                return true;
            }
            return false;
        }

        public bool remover(Projeto p)
        {
            Projeto existente = buscar(p);
            // Regra: Remover projeto apenas se sem tarefas
            if (existente != null && existente.Tarefas.Count == 0)
            {
                itens.Remove(existente);
                return true;
            }
            return false;
        }

        public Projeto buscar(Projeto p)
        {
            return itens.FirstOrDefault(x => x.Equals(p));
        }

        public List<Projeto> listar()
        {
            return itens;
        }
    }
}
