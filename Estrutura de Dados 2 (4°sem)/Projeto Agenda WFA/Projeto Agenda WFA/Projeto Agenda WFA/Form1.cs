using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto_Agenda_WFA
{
    public partial class Form1 : Form
    {
        private Contatos geradorContatos;

        // Elementos visuais (Controlos)
        private TextBox txtEmail, txtNome, txtDia, txtMes, txtAno, txtTelTipo, txtTelNumero;
        private CheckBox chkPrincipal;
        private ListBox lstContatos;

        public Form1()
        {
            geradorContatos = new Contatos();
            ConfigurarJanela();
            InicializarComponentes();
        }

        // Configuração básica do tamanho e título da janela
        private void ConfigurarJanela()
        {
            this.Text = "Projeto Agenda - Windows Forms";
            this.Size = new Size(650, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Desenho dos botões, caixas de texto e listas no ecrã
        private void InicializarComponentes()
        {
            // --- SEÇÃO: DADOS DO CONTATO ---
            AdicionarLabel("E-mail:", 20, 20);
            txtEmail = AdicionarTextBox(20, 40, 200);

            AdicionarLabel("Nome:", 230, 20);
            txtNome = AdicionarTextBox(230, 40, 200);

            AdicionarLabel("Nascimento (DD / MM / AAAA):", 20, 70);
            txtDia = AdicionarTextBox(20, 90, 40);
            txtMes = AdicionarTextBox(70, 90, 40);
            txtAno = AdicionarTextBox(120, 90, 60);

            // --- SEÇÃO: TELEFONE ---
            AdicionarLabel("Novo Telefone (Tipo / Número):", 230, 70);
            txtTelTipo = AdicionarTextBox(230, 90, 80);
            txtTelNumero = AdicionarTextBox(320, 90, 110);

            chkPrincipal = new CheckBox { Text = "Principal?", Top = 90, Left = 440, Width = 80 };
            this.Controls.Add(chkPrincipal);

            // --- SEÇÃO: BOTÕES DE AÇÃO ---
            Button btnAdicionar = AdicionarBotao("Adicionar", 20, 140);
            btnAdicionar.Click += BtnAdicionar_Click;

            Button btnPesquisar = AdicionarBotao("Pesquisar", 130, 140);
            btnPesquisar.Click += BtnPesquisar_Click;

            Button btnAlterar = AdicionarBotao("Alterar", 240, 140);
            btnAlterar.Click += BtnAlterar_Click;

            Button btnRemover = AdicionarBotao("Remover", 350, 140);
            btnRemover.Click += BtnRemover_Click;

            Button btnListar = AdicionarBotao("Listar Todos", 460, 140);
            btnListar.Click += BtnListar_Click;

            // --- SEÇÃO: LISTA DE VISUALIZAÇÃO ---
            AdicionarLabel("Lista de Contatos:", 20, 190);
            lstContatos = new ListBox
            {
                Top = 210,
                Left = 20,
                Width = 590,
                Height = 200,
                Font = new Font("Consolas", 9) // Fonte monoespaçada para ficar alinhado
            };
            this.Controls.Add(lstContatos);
        }

        // ==========================================
        // 3. EVENTOS DOS BOTÕES (A nova lógica do Seletor)
        // ==========================================
        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                // Lê os dados inseridos nas caixas de texto
                Data dt = new Data(int.Parse(txtDia.Text), int.Parse(txtMes.Text), int.Parse(txtAno.Text));
                Contato novo = new Contato(txtEmail.Text, txtNome.Text, dt);

                // Adiciona o telefone se os campos não estiverem vazios
                if (!string.IsNullOrWhiteSpace(txtTelNumero.Text))
                {
                    novo.AdicionarTelefone(new Telefone(txtTelTipo.Text, txtTelNumero.Text, chkPrincipal.Checked));
                }

                // Tenta adicionar à agenda
                if (geradorContatos.Adicionar(novo))
                {
                    MessageBox.Show("Contacto adicionado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                    AtualizarLista();
                }
                else
                {
                    MessageBox.Show("Já existe um contacto com este E-mail!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique se preencheu a data corretamente com números.", "Erro de Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            Contato c = geradorContatos.Pesquisar(new Contato(txtEmail.Text));
            if (c != null)
            {
                // Preenche os campos com os dados encontrados
                txtNome.Text = c.Nome;
                txtDia.Text = c.DtNasc.Dia.ToString();
                txtMes.Text = c.DtNasc.Mes.ToString();
                txtAno.Text = c.DtNasc.Ano.ToString();
                MessageBox.Show("Contacto encontrado e carregado nos campos acima.", "Pesquisa");
            }
            else
            {
                MessageBox.Show("Contacto não encontrado.", "Pesquisa");
            }
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            try
            {
                Data dt = new Data(int.Parse(txtDia.Text), int.Parse(txtMes.Text), int.Parse(txtAno.Text));
                Contato atualizado = new Contato(txtEmail.Text, txtNome.Text, dt);

                if (geradorContatos.Alterar(atualizado))
                {
                    MessageBox.Show("Contacto alterado com sucesso!");
                    AtualizarLista();
                }
                else
                {
                    MessageBox.Show("Contacto não encontrado pelo E-mail fornecido.");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique os dados inseridos.");
            }
        }

        private void BtnRemover_Click(object sender, EventArgs e)
        {
            if (geradorContatos.Remover(new Contato(txtEmail.Text)))
            {
                MessageBox.Show("Contacto removido com sucesso!");
                LimparCampos();
                AtualizarLista();
            }
            else
            {
                MessageBox.Show("Contacto não encontrado.");
            }
        }

        private void BtnListar_Click(object sender, EventArgs e)
        {
            AtualizarLista();
        }

        // ==========================================
        // 4. MÉTODOS AUXILIARES
        // ==========================================
        private void AtualizarLista()
        {
            lstContatos.Items.Clear();
            foreach (var c in geradorContatos.Agenda)
            {
                lstContatos.Items.Add(c.ToString());
            }
        }

        private void LimparCampos()
        {
            txtEmail.Clear(); txtNome.Clear();
            txtDia.Clear(); txtMes.Clear(); txtAno.Clear();
            txtTelTipo.Clear(); txtTelNumero.Clear();
            chkPrincipal.Checked = false;
        }

        // Funções para simplificar a criação visual por código
        private void AdicionarLabel(string texto, int left, int top)
        {
            this.Controls.Add(new Label { Text = texto, Left = left, Top = top, AutoSize = true });
        }

        private TextBox AdicionarTextBox(int left, int top, int width)
        {
            TextBox txt = new TextBox { Left = left, Top = top, Width = width };
            this.Controls.Add(txt);
            return txt;
        }

        private Button AdicionarBotao(string texto, int left, int top)
        {
            Button btn = new Button { Text = texto, Left = left, Top = top, Width = 100, Height = 30 };
            this.Controls.Add(btn);
            return btn;
        }
    }
}
