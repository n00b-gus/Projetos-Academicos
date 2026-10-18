using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Atendimento
{
    internal class Guiche
    {
        public int id { get; private set; }
        public Queue<Senha> atendimentos { get; private set; }

        public Guiche()
        {
            this.id = 0;
            this.atendimentos = new Queue<Senha>();
        }

        public Guiche(int id)
        {
            this.id = id;
            this.atendimentos = new Queue<Senha>();
        }

        public bool chamar(Queue<Senha> filaSenhas)
        {
            if (filaSenhas.Count > 0)
            {
                Senha senhaChamada = filaSenhas.Dequeue();
                senhaChamada.dataAtend = DateTime.Now.Date;
                senhaChamada.horaAtend = DateTime.Now;

                atendimentos.Enqueue(senhaChamada);
                return true;
            }
            return false;
        }
    }
}
