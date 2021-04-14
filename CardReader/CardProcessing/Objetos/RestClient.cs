using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CardProcessing.Objetos
{
    class RestClient
    {
        private string restBaseURL = "http://localhost/mosapp/backend/webservice/web/";

        public void SenderResult()
        {
            HttpClient client = null;
            

            if (client == null)
            {
                client = new HttpClient();
                client.BaseAddress = new Uri(restBaseURL);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }

            //chamando a api pela url (método que deve ser requisitado para retorno desejado)
            HttpResponseMessage response = client.GetAsync("/card/add").Result;

           
        }
    }
}
