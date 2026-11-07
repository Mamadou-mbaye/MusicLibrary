using System.Net;
using System.Text.Json;

namespace MusicLibrary.Tests;

public class ArtistsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public ArtistsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task GetArtists_Returns_ok()
    {
        // Act 
        var actual = await _client.GetAsync("/api/artists");
        // Assert 
        Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }

    [Fact]
    public async Task GetArtists_Returns_Array()
    {
        var response = await _client.GetAsync("/api/artists");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(body);

        var artists = document.RootElement.GetProperty("artists");

        Assert.Equal(JsonValueKind.Array, artists.ValueKind);
    }
    [Fact]
    public async Task SyncArtists_Returns_ok()
    {
        // Act 
        var actual = await _client.PostAsync("/api/artists/sync", content: null);
        // Assert 
        Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }
}
