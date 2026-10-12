using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Agenda_WFA
{
    internal class Telefone
    {
        public string Tipo { get; set; }
        public string Numero { get; set; }
        public bool Principal { get; set; }

        public Telefone(string tipo, string numero, bool principal)
        {
            Tipo = tipo; Numero = numero; Principal = principal;
        }
    }
}
