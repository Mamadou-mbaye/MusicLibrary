using System.Text.Json.Nodes;

public class DatabaseApi
{
	public static async Task<IResult> TestConnection(DatabaseInitializer db, CancellationToken ct)
	{
		await db.TestConnectionAsync(ct);
		return Results.Ok(new { status = "ok" });
	}
	// IArtistService
	public static async Task<IResult> GetArtists(IArtistService artistService, CancellationToken ct)
	{
		var artists = await artistService.GetArtistsAsync(ct);
		return Results.Json(new
		{
			artists,
			timestamputc = DateTime.UtcNow.AddHours(2),
		}
		);
	}
	public static async Task<IResult> SyncArtists(IArtistService artistService, CancellationToken ct)
	{
		var imported = await artistService.SyncArtistsAsync(ct);

		return Results.Json(new
		{
			service = "Music Library",
			status = "ok",
			importedArtists = imported,
			timestampUtc = DateTime.UtcNow
		});
	}

	public static async Task<IResult> GetAlbums(IAlbumService albumService, CancellationToken ct)
	{
		var albums = await albumService.GetAlbumsAsync(ct);
		return Results.Json(new
		{
			albums,
			timestamputc = DateTime.UtcNow.AddHours(2),
		}
		);
	}
	public static async Task<IResult> SyncAlbums(IAlbumService albumService, CancellationToken ct)
	{
		var imported = await albumService.SyncAlbumsAsync(ct);

		return Results.Json(new
		{
			service = "Music Library",
			status = "ok",
			importedAlbums = imported,
			timestampUtc = DateTime.UtcNow
		});
	}

	public static async Task<IResult> GetTracks(ITrackService trackService, CancellationToken ct)
	{
		var tracks = await trackService.GetTracksAsync(ct);
		return Results.Json(new
		{
			tracks,
			timestamputc = DateTime.UtcNow.AddHours(2),
		}
		);
	}
	public static async Task<IResult> SyncTracks(ITrackService trackService, CancellationToken ct)
	{
		var imported = await trackService.SyncTracksAsync(ct);

		return Results.Json(new
		{
			service = "Music Library",
			status = "ok",
			importedAlbums = imported,
			timestampUtc = DateTime.UtcNow
		});
	}

}