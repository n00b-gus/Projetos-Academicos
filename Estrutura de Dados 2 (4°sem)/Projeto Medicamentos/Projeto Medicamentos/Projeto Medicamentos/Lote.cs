using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Medicamentos
{
    internal class Lote
    {
        private int id;
        private int qtde;
        private DateTime venc;


        public int Id { get => id; set => id = value; }
        public int Qtde { get => qtde; set => qtde = value; }
        public DateTime Venc
        {
            get => venc; set => venc = value;
        }

        public Lote()
        {
            this.id = 0;
            this.qtde = 0;
            this.venc = DateTime.MinValue;
        }

        public Lote(int id, int qtde, DateTime venc)
        {
            this.id = id;
            this.qtde = qtde;
            this. venc = venc;
        }

        public string toString()
        {
            return $"{this.id}-{this.qtde}-{this.venc:dd/MM/yyyy}";
        }
    }
}
