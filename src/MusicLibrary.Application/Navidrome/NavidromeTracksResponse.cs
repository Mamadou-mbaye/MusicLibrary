using System.Text.Json.Serialization;

public record NavidromeTracksResponse(
    [property: JsonPropertyName("subsonic-response")]
    TrackSubsonicResponse SubsonicResponse);

public record TrackSubsonicResponse(
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("searchResult3")] TrackList2? TrackList);

public record TrackList2(
    [property: JsonPropertyName("song")] List<NavidromeTrack> Track);

public record NavidromeTrack(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("duration")] int DurationSeconds,
    [property: JsonPropertyName("path")] string Path,
    [property: JsonPropertyName("albumId")] string AlbumId,    
    [property: JsonPropertyName("created")] DateTime Created);