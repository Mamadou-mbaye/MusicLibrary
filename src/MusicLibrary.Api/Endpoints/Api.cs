using System.Text.Json.Nodes;

public static class Api
{
	public static void ConfigureApi(this WebApplication app)
	{
		//app.MapGet("/", Root).WithName("Root");
		app.MapGet("/api/navidrome/Test", Test).WithName("Test");

		app.MapGet("/api/navidrome/ping", Ping).WithName("Ping");
		app.MapGet("/api/navidrome/artists", GetArtists).WithName("Artists");
		app.MapGet("/api/navidrome/musicFolders", GetMusicFolders).WithName("MusicFolders");
		app.MapGet("/api/navidrome/albumList", GetAlbumList).WithName("AlbumList");
		app.MapGet("/api/navidrome/albumList2/{type}", GetAlbumList2).WithName("AlbumList2");
		app.MapGet("/api/navidrome/indexes", GetIndexes).WithName("Indexes");

		app.MapGet("/api/navidrome/search3", SearchAllSongs).WithName("SearchAllSongs");




		app.MapGet("/api/database/ping", TestConnection).WithName("TestConnection");

	}
	public static async Task<IResult> Test()
	{
		return Results.Json(new
		{
			service = "Music Library",
			status = "ok",
			timestamputc = DateTime.UtcNow,
		}
		);
	}
	/*
	public static async Task<IResult> TestApp(ITestApp testApp)
	{
		return Results.Json(new
		{
			service = "Music Library",
			status = "ok",
			message = testApp.GetMessage(),
			timestamputc = DateTime.UtcNow,
		}
		);
	}
	*/
	public static async Task<IResult> Ping(NavidromeClient client, CancellationToken ct)
	{
		var result = await client.PingAsync(ct);
		return Results.Content(result, "application/json");
	}
	public static async Task<IResult> GetArtists(NavidromeClient client, CancellationToken ct)
	{
		var result = await client.GetArtistsAsync(ct);
		return Results.Content(result, "application/json");
	}
	public static async Task<IResult> GetMusicFolders(NavidromeClient client, CancellationToken ct)
	{
		var result = await client.GetMusicFoldersAsync(ct);
		return Results.Content(result, "application/json");
	}
	public static async Task<IResult> GetAlbumList(NavidromeClient client, CancellationToken ct)
	{
		var result = await client.GetAlbumListAsync(ct);
		return Results.Content(result, "application/json");
	}
	public static async Task<IResult> GetAlbumList2(NavidromeClient client, CancellationToken ct, string type)
	{
		// "alphabeticalByName"
		Dictionary<string, string> extraParams = new()
		{
			["type"] = type
		};
		var result = await client.GetAlbumList2Async(extraParams, ct);
		return Results.Content(result, "application/json");
	}
	public static async Task<IResult> GetIndexes(NavidromeClient client, CancellationToken ct)
	{
		var result = await client.GetIndexesAsync(ct);
		return Results.Content(result, "application/json");
	}
	public static async Task<IResult> SearchAllSongs(NavidromeClient client, CancellationToken ct)
	{

		var obj = new JsonObject();
		const int PageSize = 500;
		var offset = 0;

		var result = await client.SearchAllSongsAsync(PageSize, offset, ct);
		return Results.Content(result, "application/json");

		/*
				while	(true){
				var songs = ParseSongs(result);
				if (songs.Count == 0)
				break;
				 await SaveSongsAsync(songs, ct);
					offset += songs.Count;
				}
			}
				private int ParseSongs(string result){
				*/
	}
	public static async Task<IResult> TestConnection(DatabaseInitializer db, CancellationToken ct)
	{
		await db.TestConnectionAsync(ct);
		return Results.Ok(new { status = "ok" });
	}
}
