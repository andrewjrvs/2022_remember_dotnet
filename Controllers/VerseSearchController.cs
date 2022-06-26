using System.Text.Json;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace _2022_remember_dotnet.Controllers;

[ApiController()]
[Route("api/verses/search")]
public class VersesSearchController : ControllerBase
{

    private readonly ILogger<VersesSearchController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly String _token = "";
    private readonly String _bibleId = "";

    public VersesSearchController(ILogger<VersesSearchController> logger
                            , IHttpClientFactory httpClientFactory
                            , IConfiguration configuration)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _bibleId = configuration["bibleId"];
        _token = configuration["bibleApiToken"];
    }

    [HttpGet]
    public async Task<Object> Get(String q) {
        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"https://api.scripture.api.bible/v1/bibles/{this._bibleId}/search?query={HttpUtility.UrlEncode(q)}&sort=relevance") {
            Headers = {
                { HeaderNames.Accept, "application/json" },
                { "api-key", this._token }
            }
        };

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode) {
            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();
            Console.WriteLine(contentStream);
            return await JsonSerializer.DeserializeAsync
                <Object>(contentStream) ?? Array.Empty<Object>();
        }
        return Array.Empty<Object>();
    }
}
