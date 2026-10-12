using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Agenda_WFA
{
    internal class Data
    {
        public int Dia { get; private set; }
        public int Mes { get; private set; }
        public int Ano { get; private set; }

        public Data(int dia, int mes, int ano) => SetData(dia, mes, ano);

        public void SetData(int dia, int mes, int ano)
        {
            Dia = dia; Mes = mes; Ano = ano;
        }

        public override string ToString() => $"{Dia:D2}/{Mes:D2}/{Ano:D4}";
    }
}

