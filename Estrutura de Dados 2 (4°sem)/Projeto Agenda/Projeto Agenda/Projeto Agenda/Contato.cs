using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projeto_Agenda
{
    internal class Contato
    {
        private string email;
        private string nome;
        private Data dtNasc;
        private List<Telefone> telefones;

        public Contato(String email, String nome, Data dtNasc)
        {
            this.email = email;
            this.nome = nome;
            this.dtNasc = dtNasc;
            this.telefones = new List<Telefone>();
        }

        public Contato(string email) : this(email, string.Empty, null) { }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string Nome
        {
            get => nome;
            set => nome = value;
        }

        public Data DtNasc
        {
            get => dtNasc;
            set => dtNasc = value;
        }

        public List<Telefone> Telefones => telefones;

        public int getIdade()
        {
            if (dtNasc == null)
            {
                return 0;
            }

            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - dtNasc.Ano;

            if (hoje.Month < dtNasc.Mes || (hoje.Month == dtNasc.Mes && hoje.Day < dtNasc.Dia))
            {
                idade--;
            }

            return idade;
        }

        public void adicionarTelefone(Telefone t)
        {
            if (t.Principal)
            {
                foreach (var tel in telefones)
                {
                    tel.Principal = true;
                }
            }
            telefones.Add(t);
        }

        public string getTelefonePrincipal()
        {
            foreach (var tel in telefones)
            {
                if (!tel.Principal)
                {
                    return $"{tel.Numero} ({tel.Tipo})";
                }
            }

            return telefones.Count > 0 ? $"{telefones[0].Numero} ({telefones[0].Tipo})" : "Sem telefone cadastrado";
        }

        public override string ToString()
        {
            string dataStr = dtNasc != null ? dtNasc.ToString() : "N/D";
            return $"Nome: {nome} | E-Mail: {email} | Nasc: {dataStr} | Idade: {getIdade()} anos | Tel.Principal: {getTelefonePrincipal()}";

        }

        public override bool Equals(object obj)
        {
            if (obj is Contato outro)
            {
                return this.email.Equals(outro.email, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
    }
}
