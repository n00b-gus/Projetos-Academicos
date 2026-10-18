using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Atendimento
{
    public partial class Form1 : Form
    {
        private Button btnGerar;
        private Button btnAdicionar;
        private Button btnChamar;

        private TextBox txtQtdGuiches;
        private TextBox txtGuicheChamada;

        private ListBox lstSenhas;
        private ListBox lstAtendimentos;

        private Label lblQtdGuiches;
        private Label lblGuicheChamada;
        private Label lblSenhas;
        private Label lblAtendimentos;

        private Senhas gerenciadorSenhas;
        private Guiches gerenciadorGuiches;

        public Form1()
        {
            gerenciadorSenhas = new Senhas();
            gerenciadorGuiches = new Guiches();

            ConstruirInterface();
        }

        private void ConstruirInterface()
        {
            this.Text = "Sistema de Atendimento e Filas";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            btnGerar = new Button();
            btnGerar.Text = "Gerar Nova Senha";
            btnGerar.Location = new Point(20, 20);
            btnGerar.Size = new Size(150, 30);
            btnGerar.Click += new EventHandler(btnGerar_Click);
            this.Controls.Add(btnGerar);

            lblQtdGuiches = new Label();
            lblQtdGuiches.Text = "Quantidade de Guichês a adicionar:";
            lblQtdGuiches.Location = new Point(20, 75);
            lblQtdGuiches.AutoSize = true;
            this.Controls.Add(lblQtdGuiches);

            txtQtdGuiches = new TextBox();
            txtQtdGuiches.Location = new Point(220, 72);
            txtQtdGuiches.Size = new Size(50, 20);
            this.Controls.Add(txtQtdGuiches);

            btnAdicionar = new Button();
            btnAdicionar.Text = "Adicionar Guichê(s)";
            btnAdicionar.Location = new Point(280, 70);
            btnAdicionar.Size = new Size(150, 30);
            btnAdicionar.Click += new EventHandler(btnAdicionar_Click);
            this.Controls.Add(btnAdicionar);

            lblGuicheChamada = new Label();
            lblGuicheChamada.Text = "ID do Guichê para chamar a senha:";
            lblGuicheChamada.Location = new Point(20, 125);
            lblGuicheChamada.AutoSize = true;
            this.Controls.Add(lblGuicheChamada);

            txtGuicheChamada = new TextBox();
            txtGuicheChamada.Location = new Point(220, 122);
            txtGuicheChamada.Size = new Size(50, 20);
            this.Controls.Add(txtGuicheChamada);

            btnChamar = new Button();
            btnChamar.Text = "Chamar Próxima Senha";
            btnChamar.Location = new Point(280, 120);
            btnChamar.Size = new Size(150, 30);
            btnChamar.Click += new EventHandler(btnChamar_Click);
            this.Controls.Add(btnChamar);

            lblSenhas = new Label();
            lblSenhas.Text = "Senhas na Fila (Aguardando):";
            lblSenhas.Location = new Point(20, 180);
            lblSenhas.AutoSize = true;
            this.Controls.Add(lblSenhas);

            lstSenhas = new ListBox();
            lstSenhas.Location = new Point(20, 200);
            lstSenhas.Size = new Size(250, 220);
            this.Controls.Add(lstSenhas);

            lblAtendimentos = new Label();
            lblAtendimentos.Text = "Histórico de Atendimentos:";
            lblAtendimentos.Location = new Point(300, 180);
            lblAtendimentos.AutoSize = true;
            this.Controls.Add(lblAtendimentos);

            lstAtendimentos = new ListBox();
            lstAtendimentos.Location = new Point(300, 200);
            lstAtendimentos.Size = new Size(250, 220);
            this.Controls.Add(lstAtendimentos);
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            gerenciadorSenhas.gerar();
            AtualizarListaSenhas();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtQtdGuiches.Text, out int qtd) && qtd > 0)
            {
                int guichesAtuais = gerenciadorGuiches.listaGuiches.Count;
                for (int i = 1; i <= qtd; i++)
                {
                    gerenciadorGuiches.adicionar(new Guiche(guichesAtuais + i));
                }
                MessageBox.Show($"{qtd} guichê(s) adicionado(s) com sucesso!\nTotal de guichês: {gerenciadorGuiches.listaGuiches.Count}", "Sucesso");
                txtQtdGuiches.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, insira um número inteiro válido (maior que zero) para a quantidade de guichês.", "Aviso");
            }
        }

        private void btnChamar_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtGuicheChamada.Text, out int idGuiche))
            {
                Guiche guiche = gerenciadorGuiches.listaGuiches.FirstOrDefault(g => g.id == idGuiche);

                if (guiche != null)
                {
                    if (guiche.chamar(gerenciadorSenhas.filaSenhas))
                    {
                        AtualizarListaSenhas();
                        AtualizarListaAtendimentos();
                        txtGuicheChamada.Clear();
                    }
                    else
                    {
                        MessageBox.Show("A fila de espera está vazia. Não há senhas para chamar.", "Fila Vazia");
                    }
                }
                else
                {
                    MessageBox.Show($"O guichê de ID {idGuiche} não foi encontrado. Certifica-te de que ele já foi adicionado.", "Erro");
                }
            }
            else
            {
                MessageBox.Show("Por favor, insira o número (ID) do guichê corretamente.", "Aviso");
            }
        }

        private void AtualizarListaSenhas()
        {
            lstSenhas.Items.Clear();
            foreach (Senha s in gerenciadorSenhas.filaSenhas)
            {
                lstSenhas.Items.Add(s.dadosParciais());
            }
        }

        private void AtualizarListaAtendimentos()
        {
            lstAtendimentos.Items.Clear();

            foreach (Guiche g in gerenciadorGuiches.listaGuiches)
            {
                foreach (Senha s in g.atendimentos)
                {
                    lstAtendimentos.Items.Add($"Guichê {g.id}: {s.dadosCompletos()}");
                }
            }
        }
    }
}
