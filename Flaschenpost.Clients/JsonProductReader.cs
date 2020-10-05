using Flaschenpost.Core.Contracts;
using Flaschenpost.Shared;
using Flaschenpost.Shared.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Flaschenpost.Clients
{
    public class JsonProductReader : IRequestHandler<string, IEnumerable<ProductModel>>
    {
        private readonly HttpClient _httpClient;

        public JsonProductReader(HttpClient httpClient)
        {
            _httpClient = httpClient.GuardAgainstNull(nameof(httpClient));
        }

        public async Task<IEnumerable<ProductModel>> HandleRequest(string payload)
        {
            var responce = await _httpClient.GetAsync(payload);

            responce.EnsureSuccessStatusCode();

            var json = await responce.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ProductModel[]>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })?.ToImmutableList() ?? Enumerable.Empty<ProductModel>();
        }
    }
}
