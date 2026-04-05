using System.Text.Json;

namespace TestCommon.HttpClient
{
    public class Ch3Client
    {
        protected System.Net.Http.HttpClient _client;

        public Ch3Client(System.Net.Http.HttpClient client) 
        {
            this._client = client;    
        }

        //schDate format ex. 20210909
        public async Task<JsonDocument>? GetSch(string schDate= "20210909")
        {
            var resp = await this._client.GetAsync($"schedulebydatepartner?date={schDate}");
            return await resp.Content.ReadFromJsonAsync<JsonDocument>();
        }
    }
}
