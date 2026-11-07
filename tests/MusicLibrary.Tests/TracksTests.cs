
using System.Net;
using System.Text.Json;

namespace MusicLibrary.Tests;

public class TracksTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public TracksTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task GetTracks_Returns_ok()
    {
        // Act 
        var actual = await _client.GetAsync("/api/tracks");
        // Assert 
        Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }


    [Fact]
    public async Task GetTracks_Returns_Array()
    {
        var response = await _client.GetAsync("/api/tracks");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(body);

        var tracks = document.RootElement.GetProperty("tracks");

        Assert.Equal(JsonValueKind.Array, tracks.ValueKind);
    }

    [Fact]
    public async Task SyncTracks_Returns_ok()
    {        // Act 
        var actual = await _client.PostAsync("/api/tracks/sync", content: null);
        // Assert 
        Assert.Equal(HttpStatusCode.OK, actual.StatusCode);
    }
}
