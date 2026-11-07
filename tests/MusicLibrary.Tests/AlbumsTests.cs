
using System.Net;
using System.Text.Json;

namespace MusicLibrary.Tests;

public class AlbumsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AlbumsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task GetAlbums_Returns_ok()
    {
        // Act 
        var actual = await _client.GetAsync("/api/albums");
        // Assert 
        Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }

    [Fact]
    public async Task GetAlbums_Returns_Array()
    {
        var response = await _client.GetAsync("/api/albums");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(body);

        var albums = document.RootElement.GetProperty("albums");

        Assert.Equal(JsonValueKind.Array, albums.ValueKind);
    }
    [Fact]
    public async Task SyncAlbums_Returns_ok()
    {
        // Act 
        var actual = await _client.PostAsync("/api/albums/sync", content: null);
        // Assert 
        Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }
}
