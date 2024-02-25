using System;
using System.Windows.Forms;
using ViaCep_WinForms.Models;
using ViaCep_WinForms.Services;

namespace ViaCep_WinForms
{
    public partial class FrmViaCep : Form
    {
        public FrmViaCep()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(mskCep.Text))
            {
                MessageBox.Show("O campo CEP deve ser preenchido.", "Informação do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SendKeys.Send("Tab");
                return;
            }

            if (mskCep.Text.Length != 8)
            {
                MessageBox.Show("O campo CEP deve conter 8 dígitos.", "Informação do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SendKeys.Send("Tab");
                return;
            }

            CepService service = new CepService();

            CepResponse response = service.ConsultarCep(mskCep.Text.Replace("-", ""));

            if (response == null || response.Endereco == null)
            {
                MessageBox.Show("CEP não localizado!", "Informação do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                limparCampos();
                SendKeys.Send("Tab");
                return;
            }

            txtEndereco.Text = response.Endereco;
            txtBairro.Text = response.Bairro;
            txtComplemento.Text = response.Complemento;
            txtCidade.Text = response.Cidade;
            txtUF.Text = response.UF;
        }

        private void mskCep_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                btnBuscar_Click(sender, e);
        }

        private void limparCampos()
        {
            mskCep.Clear();
            txtEndereco.Clear();
            txtBairro.Clear();
            txtComplemento.Clear();
            txtCidade.Clear();
            txtUF.Clear();
        }

        private void FrmViaCep_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
                this.Close();
        }
    }
}
