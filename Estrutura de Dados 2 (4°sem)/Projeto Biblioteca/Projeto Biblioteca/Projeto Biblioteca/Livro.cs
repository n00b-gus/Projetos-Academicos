using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Biblioteca
{
    internal class Livro
    {
        private int isbn;
        private string titulo;
        private string autor;
        private string editora;
        private List<Exemplar> exemplares;

        public Livro(int isbn, string titulo, string autor, string editora)
        {
            this.isbn = isbn;
            this.titulo = titulo;
            this.autor = autor;
            this.editora = editora;
            this.exemplares = new List<Exemplar>();
        }

        public Livro(int isbn) : this(isbn, "", "", "") { }

        public int Isbn => isbn;
        public string Titulo => titulo;
        public string Autor => autor;
        public string Editora => editora;
        public List<Exemplar> Exemplares => exemplares;

        public void adicionarExemplar(Exemplar exemplar)
        {
            exemplares.Add(exemplar);
        }

        public int qtdeExemplares()
        {
            return exemplares.Count;
        }

        public int qtdeDisponiveis()
        {
            int cont = 0;
            foreach (var ex in exemplares)
            {
                if (ex.disponivel()) cont++;
            }
            return cont;
        }

        public int qtdeEmprestimos()
        {
            int total = 0;
            foreach (var ex in exemplares)
            {
                total += ex.qtdeEmprestimos();
            }
            return total;
        }

        public double percDisponibilidade()
        {
            if (qtdeExemplares() == 0) return 0;
            return (double)qtdeDisponiveis() / qtdeExemplares() * 100.0;
        }

        public override bool Equals(object obj)
        {
            if (obj is Livro outro)
            {
                return this.isbn == outro.isbn;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return isbn.GetHashCode();
        }
    }
}
