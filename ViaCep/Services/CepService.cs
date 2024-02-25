using Newtonsoft.Json;
using RestSharp;
using System.Windows.Forms;
using ViaCep_WinForms.Models;

namespace ViaCep_WinForms.Services
{
    public class CepService
    {
        public CepResponse ConsultarCep(string cep)
        {
            CepResponse cepResponse = new CepResponse();
            RestClient client = new RestClient("https://viacep.com.br");
            RestRequest request = new RestRequest($"/ws/{cep}/json/");

            RestResponse response = client.Execute(request);

            if ((response.StatusCode == System.Net.HttpStatusCode.OK) || (response.StatusCode == System.Net.HttpStatusCode.Created))
                cepResponse =  JsonConvert.DeserializeObject<CepResponse>(response.Content);
            else
            {
                cepResponse = null;
                MessageBox.Show("CEP inválido!", "Informação do sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return cepResponse;
        }
    }
}
