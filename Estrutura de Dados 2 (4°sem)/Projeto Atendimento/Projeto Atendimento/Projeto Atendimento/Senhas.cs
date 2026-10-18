using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Atendimento
{
    internal class Senhas
    {
        public int proximoAtendimento { get; private set; }
        public Queue<Senha> filaSenhas { get; private set; }

        public Senhas()
        {
            filaSenhas = new Queue<Senha>();
            proximoAtendimento = 1;
        }

        public void gerar()
        {
            Senha novaSenha = new Senha(proximoAtendimento);
            filaSenhas.Enqueue(novaSenha);
            proximoAtendimento++;
        }
    }
}
