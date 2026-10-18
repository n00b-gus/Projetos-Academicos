using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Atendimento
{
    internal class Senha
    {
        public int id { get; set; }
        public DateTime dataGerac { get; set; }
        public DateTime horaGerac { get; set; }
        public DateTime dataAtend { get; set; }
        public DateTime horaAtend { get; set; }

        public Senha(int id)
        {
            this.id = id;
            this.dataGerac = DateTime.Now.Date;
            this.horaGerac = DateTime.Now;
        }

        public string dadosParciais()
        {
            return $"{id} - {dataGerac.ToShortDateString()} - {horaGerac.ToLongTimeString()}";
        }

        public string dadosCompletos()
        {
            return $"{id} - {dataGerac.ToShortDateString()} - {horaGerac.ToLongTimeString()} - {dataAtend.ToShortDateString()} - {horaAtend.ToLongTimeString()}";
        }
    }
}
