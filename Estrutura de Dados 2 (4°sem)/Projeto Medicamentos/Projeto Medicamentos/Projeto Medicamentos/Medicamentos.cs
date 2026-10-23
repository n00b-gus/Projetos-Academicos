using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_Medicamentos
{
    internal class Medicamentos
    {
        private List<Medicamento> listaMedicamentos;

        public List<Medicamento> ListaMedicamentos { get => listaMedicamentos; }

        public Medicamentos()
        {
            this.listaMedicamentos = new List<Medicamento>();
        }

        public void adicionar(Medicamento medicamento)
        {
            this.listaMedicamentos.Add(medicamento);
        }

        public bool deletar(Medicamento medicamento)
        {
            Medicamento encontrado = pesquisar(medicamento);

            if (encontrado.Id != 0 && encontrado.qtdeDisponivel() == 0)
            {
                return this.listaMedicamentos.Remove(encontrado);
            }

            return false;
        }

        public Medicamento pesquisar(Medicamento medicamento)
        {
            foreach (Medicamento med in this.listaMedicamentos)
            {
                if (med.Equals(medicamento))
                {
                    return med;
                }
            }
            return new Medicamento();
        }

    }
}
