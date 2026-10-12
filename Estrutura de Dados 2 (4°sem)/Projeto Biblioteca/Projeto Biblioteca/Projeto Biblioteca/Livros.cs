using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Biblioteca
{
    internal class Livros
    {
        private List<Livro> acervo;

        public Livros()
        {
            this.acervo = new List<Livro>();
        }

        public void adicionar(Livro livro)
        {
            if (pesquisar(livro) == null)
            {
                acervo.Add(livro);
            }
            else
            {
                Console.WriteLine("Erro: Livro com este ISBN já existe no acervo.");
            }
        }

        public Livro pesquisar(Livro livro)
        {
            return acervo.FirstOrDefault(l => l.Equals(livro));
        }
    }
}
