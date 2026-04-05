using MassTransit;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HttpClients.Clients
{
    public class AppConfigurationClient
    {
        private readonly HttpClient _client;
        public AppConfigurationClient(HttpClient client)
        {
            this._client = client;
        }

        public async Task<HttpResponseMessage> GetJsonConfiguration()
        {
            return await this._client.SendJsonAsync($"?name={this.GetType().Assembly.GetName().Name}", HttpMethod.Get,"");
        }
    }
}
