
using System.Text.Json;
public class TrackTagsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    public TrackTagsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ReadTags_Returns_Title()
    {
        string title = "Youth";

        var response = await _client.GetAsync("/api/tracks/readTags/Youth-Daughter.opus");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(body);
        
        var actualTitle = document.RootElement.GetProperty("trackTags").GetProperty("title").GetString();

        Assert.Equal(title, actualTitle);
    }
}

