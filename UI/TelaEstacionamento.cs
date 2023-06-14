using Business;
using Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Estacionamento
{
    public partial class TelaEstacionamento : Form
    {
        List<string> numeroVagas;
        Gerenciamento geren = new Gerenciamento();
        Veiculo veiculo = new Veiculo();
        public TelaEstacionamento()
        {
            InitializeComponent();
            numeroVagas = new List<string>();
        }
        private void TelaMotorista_Load(object sender, EventArgs e)
        {
            new Veiculo().CriarTabela();
            Carrega_DataGrid();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            var dataAtual = DateTime.Now;
            try
            {
                if(numeroVagas.Count <= 30 && !string.IsNullOrEmpty(txtPlaca.Text) && !string.IsNullOrEmpty(txtTempo.Text))
                {
                    if (txtId.Text != "")
                    {
                        veiculo.Id = int.Parse(txtId.Text);
                        MessageBox.Show("Atualizado com sucesso");
                    }
                    veiculo.Placa = txtPlaca.Text.ToUpper();
                    veiculo.Modelo = txtModelo.Text.ToUpper();
                    veiculo.Horario = int.Parse(txtTempo.Text);
                    veiculo.DataEntrada = DateTime.Now.ToString();
                    veiculo.Valor = geren.CalculaValor(veiculo.Horario);
                    veiculo.Pago = "NÃO";
                    veiculo.Salvar();
                    numeroVagas.Add(txtPlaca.Text);
                    MessageBox.Show("Salvo com sucesso");
                    Limpar();
                    Carrega_DataGrid();
                }
                else
                {
                    MessageBox.Show("Por favor, preencha com os dados corretos");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }
        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                veiculo.Id = Convert.ToInt32(txtId.Text);
                foreach (Veiculo m in veiculo.Buscar())
                {
                    txtId.Text = m.Id.ToString();
                    txtPlaca.Text = m.Placa;
                    txtModelo.Text = m.Modelo;
                    txtTempo.Text = m.Horario.ToString();
                }
                Carrega_DataGrid();
            }
            else
                MessageBox.Show("Preencha o ID");
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                var retorno = MessageBox.Show("Tem certeza que deseja excluir?", "Excluir", MessageBoxButtons.YesNo);
                if (retorno == DialogResult.Yes)
                {
                    veiculo.Id = Convert.ToInt32(txtId.Text);
                    veiculo.Excluir();
                }
                Limpar();
                Carrega_DataGrid();
            }
            else
                MessageBox.Show("Preencha o ID");

        }
        private void Carrega_DataGrid()
        {
            veiculo = new Veiculo();
            dgvDados.AutoGenerateColumns = true;
            var dados = veiculo.Todos();

            if (dados.Any())
            {
                DataTable dt = new DataTable();

                // Adicionar as colunas ao DataTable
                foreach (PropertyInfo prop in dados.First().GetType().GetProperties())
                {
                    dt.Columns.Add(prop.Name, prop.PropertyType);
                }

                // Adicionar as linhas ao DataTable
                foreach (var item in dados)
                {
                    DataRow row = dt.NewRow();
                    foreach (PropertyInfo prop in item.GetType().GetProperties())
                    {
                        row[prop.Name] = prop.GetValue(item);
                    }
                    dt.Rows.Add(row);
                }

                dgvDados.DataSource = dt;
            }
        }
        private void Limpar()
        {
            txtId.Text = null;
            txtPlaca.Text = null;
            txtModelo.Text = null;
            txtTempo.Text = null;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void btnVaga_Click(object sender, EventArgs e)
        {
            if (txtId.Text != null)
            {
                if (numeroVagas.Contains(txtPlaca.Text))
                {
                    numeroVagas.Remove(txtPlaca.Text);
                    MessageBox.Show($"Vaga da placa {txtPlaca.Text} foi liberada");
                }
                else
                {
                   MessageBox.Show("Placa não encontrada");
                }
            }
            else
                MessageBox.Show("Para liberar uma vaga, passe uma placa!");

        }

        private void btnPagar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtId.Text) && !string.IsNullOrEmpty(txtPlaca.Text) && !string.IsNullOrEmpty(txtTempo.Text))
            {
                var dataAtual = DateTime.Now;
                veiculo.Id = Convert.ToInt32(txtId.Text);
                veiculo.Placa = txtPlaca.Text.ToUpper();
                veiculo.Modelo = txtModelo.Text.ToUpper();
                veiculo.Horario = int.Parse(txtTempo.Text);
                var dataSaida = geren.Saida(veiculo.Horario, dataAtual);

                veiculo.DataSaida = dataSaida.ToString();
                veiculo.DataEntrada = DateTime.Now.ToString();

                veiculo.Valor = geren.CalculaValor(veiculo.Horario);
                veiculo.Salvar();


                Carrega_DataGrid();
            }
            else
                MessageBox.Show("Para dar um veiculo como pago, preencha todos os campos. Você pode usar o botão pesquisar para que os campos se preencham sozinho");
           
        }
    }
}
