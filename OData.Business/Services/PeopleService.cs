using OData.Business.Models;
using OData.Business.Utilities;

namespace OData.Business.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly HttpClient _httpClient;
        public PeopleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PeopleRowModel> Get(SearchRequest request)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            string? apiurl = $"{ApiConst.People}?$filter={request.Col.ReplaceWhitespace()} eq '{request.Value}'";
#pragma warning restore CS8604 // Possible null reference argument.
            HttpResponseMessage? response = await _httpClient.GetAsync(apiurl);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleRowModel>();
        }

        public async Task<PeopleRowModel> Get()
        {
            HttpResponseMessage? response = await _httpClient.GetAsync($"{ApiConst.People}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleRowModel>();
        }

        public async Task<PeopleModel> GetById(string id)
        {
            HttpResponseMessage? response = await _httpClient.GetAsync($"{ApiConst.People}('{id}')");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"{response.StatusCode}");
            }
            string? jsonResult = await response.Content.ReadAsStringAsync();
            return jsonResult.Deserialize<PeopleModel>();
        }

    }
}