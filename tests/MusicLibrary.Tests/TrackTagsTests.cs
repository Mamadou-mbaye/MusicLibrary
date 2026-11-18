using MusicLibrary.Application.Models;
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

        var response = await _client.GetAsync("/api/tracks/readTags/Youth_Daughter.opus");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(body);

        var actualTitle = document.RootElement.GetProperty("trackTags").GetProperty("title").GetString();

        Assert.Equal(title, actualTitle);
    }
    [Fact]
    public async Task SaveTags_Returns_ok()
    {
        bool expected = true;

        var trackTags = new TrackTags()
        {
            Title = "Superman Love",
            Artist = "Pape Diouf",
            Album = "Ecoutez!",
            TrackNumber = 0,
            DiscNumber = 0,
            Genre = "",
            Year = 2022,
            FileName = "Pape_Diouf_Superman_Love.mp4"
        };
        var json = JsonSerializer.Serialize(trackTags);

        HttpContent content = new StringContent(json, encoding: null, mediaType: "application/json");

        var response = await _client.PostAsync("/api/tracks/saveTags", content);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(body);

        var actual = document.RootElement.GetProperty("saved").GetBoolean();

        Assert.Equal(expected, actual);
    }
}

