using Jtech.Common.HttpClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Jtech.Common.HttpClients.Clients
{
    public class LineNotifyClient
    {
        private readonly HttpClient _httpClient;
        public LineNotifyClient(HttpClient client)
        {
            _httpClient = client;
        }

        public async void Notify(string message, string TokenGroup)
        {
            await _httpClient.SendUrlEncodedAsync("notify", HttpMethod.Post, new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string>("message",message)
            }, header =>
            {
                header.Add("Authorization", $"Bearer {TokenGroup}");
            });
        }
    }
}
