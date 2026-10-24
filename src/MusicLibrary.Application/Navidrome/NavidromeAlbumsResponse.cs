using System.Text.Json.Serialization;

public record NavidromeAlbumsResponse(
    [property: JsonPropertyName("subsonic-response")]
    AlbumSubsonicResponse SubsonicResponse);

public record AlbumSubsonicResponse(
    [property: JsonPropertyName("status")] string Status,
    [property: JsonPropertyName("albumList2")] AlbumList2? AlbumList);

public record AlbumList2(
    [property: JsonPropertyName("album")] List<NavidromeAlbum> Album);

public record NavidromeAlbum(
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("artistId")] string ArtistId,
    [property: JsonPropertyName("created")] DateTime Created);