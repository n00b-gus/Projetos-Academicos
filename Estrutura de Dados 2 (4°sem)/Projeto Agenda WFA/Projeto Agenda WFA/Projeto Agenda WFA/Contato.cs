using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Agenda_WFA
{
    internal class Contato
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public Data DtNasc { get; set; }
        public List<Telefone> Telefones { get; private set; }

        public Contato(string email, string nome, Data dtNasc)
        {
            Email = email; Nome = nome; DtNasc = dtNasc;
            Telefones = new List<Telefone>();
        }

        public Contato(string email) : this(email, "", null) { }

        public int GetIdade()
        {
            if (DtNasc == null) return 0;
            DateTime hoje = DateTime.Today;
            int idade = hoje.Year - DtNasc.Ano;
            if (hoje.Month < DtNasc.Mes || (hoje.Month == DtNasc.Mes && hoje.Day < DtNasc.Dia)) idade--;
            return idade;
        }

        public void AdicionarTelefone(Telefone t)
        {
            if (t.Principal)
                foreach (var tel in Telefones) tel.Principal = false;
            Telefones.Add(t);
        }

        public string GetTelefonePrincipal()
        {
            foreach (var tel in Telefones)
                if (tel.Principal) return $"{tel.Numero} ({tel.Tipo})";
            return Telefones.Count > 0 ? $"{Telefones[0].Numero} ({Telefones[0].Tipo})" : "Sem telefone";
        }

        public override string ToString()
        {
            string data = DtNasc != null ? DtNasc.ToString() : "N/D";
            return $"{Nome} | {Email} | Nasc: {data} | Idade: {GetIdade()} | Tel: {GetTelefonePrincipal()}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Contato outro)
                return Email.Equals(outro.Email, StringComparison.OrdinalIgnoreCase);
            return false;
        }
    }
}

