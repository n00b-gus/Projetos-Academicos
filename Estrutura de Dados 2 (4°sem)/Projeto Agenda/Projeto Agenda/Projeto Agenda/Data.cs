using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Agenda
{
    internal class Data
    {
        private int dia;
        private int mes;
        private int ano;

        public Data() { }

        public Data(int dia, int mes, int ano)
        {
            setData(dia, mes, ano);
        }

        public int Dia => dia;
        public int Mes => mes;
        public int Ano => ano;

        public void setData(int dia, int mes, int ano)
        {
            this.dia = dia;
            this.mes = mes;
            this.ano = ano;
        }

        public override string ToString()
        {
            return $"{dia:D2}/{mes:D2}/{ano:D2}";
        }
    }
}
