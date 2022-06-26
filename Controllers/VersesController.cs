using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace _2022_remember_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VersesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<VersesController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly String _token = "";
    private readonly String _bibleId = "";

    public VersesController(ILogger<VersesController> logger
                            , IHttpClientFactory httpClientFactory
                            , IConfiguration configuration)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _bibleId = configuration["bibleId"];
        _token = configuration["bibleApiToken"];
    }

    [HttpGet]
    public async Task<Object> Get() {

        var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "https://api.scripture.api.bible/v1/bibles") {
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
            return await JsonSerializer.DeserializeAsync
                <Object>(contentStream) ?? Array.Empty<Object>();
        }
        return Array.Empty<Object>();
    }
}
