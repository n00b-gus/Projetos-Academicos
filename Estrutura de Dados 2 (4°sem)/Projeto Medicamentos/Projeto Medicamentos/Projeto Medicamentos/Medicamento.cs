using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Medicamentos
{
    internal class Medicamento
    {
        private int id;
        private string nome;
        private string laboratorio;
        private Queue<Lote> lotes;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Laboratorio { get => laboratorio; set => laboratorio = value; }
        public Queue<Lote> Lotes { get => lotes; }

        public Medicamento()
        {
            this.id = 0;
            this.nome = "";
            this.laboratorio = "";
            this.lotes = new Queue<Lote>();
        }

        public Medicamento(int id, string nome, string laboratorio)
        {
            this.id = id;
            this.nome = nome;
            this.laboratorio = laboratorio;
            this.lotes = new Queue<Lote>();
        }

        public int qtdeDisponivel()
        {
            int total = 0;
            foreach (Lote lote in this.lotes)
            {
                total += lote.Qtde;
            }
            return total;
        }

        public void comprar(Lote lote)
        {
            this.lotes.Enqueue(lote);
        }

        public bool vender(int qtde)
        {
            if (qtde <= 0 || qtde > this.qtdeDisponivel())
            {
                return false;
            }

            int quantidadeVendida = qtde;

            while (quantidadeVendida > 0 && this.lotes.Count > 0)
            {
                Lote loteAtual = this.lotes.Peek();

                if (loteAtual.Qtde > quantidadeVendida)
                {
                    loteAtual.Qtde -= quantidadeVendida;
                    quantidadeVendida = 0;
                }
                else
                {
                    quantidadeVendida -= loteAtual.Qtde;
                    loteAtual.Qtde = 0;

                    this.lotes.Dequeue();
                }
            }

            return true;
        }

        public string toString()
        {
            return $"{this.id}-{this.nome}-{this.laboratorio}-{this.qtdeDisponivel()}";
        }

        public override string ToString() => toString();

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Medicamento))
            {
                return false;
            }

            Medicamento outro = (Medicamento)obj;
            return this.id == outro.id;
        }

    }
}
