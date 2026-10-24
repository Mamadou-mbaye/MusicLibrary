using System.Text.Json.Serialization;

public record NavidromeArtistsResponse(
    [property: JsonPropertyName("subsonic-response")] ArtistSubsonicResponse SubsonicResponse);

public record ArtistSubsonicResponse(
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("artists")] ArtistsContainer? Artists);

public record ArtistsContainer(
    [property: JsonPropertyName("index")] List<ArtistIndex> Index);

public record ArtistIndex(
    [property: JsonPropertyName("artist")] List<NavidromeArtist> Artist);

public record NavidromeArtist(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("sortName")] string? SortName);