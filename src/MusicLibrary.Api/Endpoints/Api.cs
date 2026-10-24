using System.Text.Json.Nodes;

public static class Api
{
	public static void ConfigureApi(this WebApplication app)
	{
		//app.MapGet("/", Root).WithName("Root");
		{// navidrome endpoints 
			app.MapGet("/api/navidrome/Test", Test).WithName("NavidromeTest");
			app.MapGet("/api/navidrome/ping", NavidromeApi.Ping).WithName("NavidromePing");

			app.MapGet("/api/navidrome/artists", NavidromeApi.GetArtists).WithName("NavidromeArtists");
			app.MapGet("/api/navidrome/albumList2", NavidromeApi.GetAlbumList2).WithName("NavidromeAlbumList2");
			app.MapGet("/api/navidrome/search3", NavidromeApi.SearchAllSongs).WithName("NavidromeSearchAllSongs");

		}
		{ // database endpoints 
			app.MapGet("/api/database/ping", DatabaseApi.TestConnection).WithName("DBTestConnection");
	
			app.MapGet("/api/artists", DatabaseApi.GetArtists).WithName("DBArtists");
			app.MapPost("/api/artists/sync", DatabaseApi.SyncArtists).WithName("DBSyncArtists");
	
			app.MapGet("/api/albums", DatabaseApi.GetAlbums).WithName("DBAlbums");
			app.MapPost("/api/albums/sync", DatabaseApi.SyncAlbums).WithName("DBSyncAlbums");

			app.MapGet("/api/tracks", DatabaseApi.GetTracks).WithName("DBTracks");
			app.MapPost("/api/tracks/sync", DatabaseApi.SyncTracks).WithName("DBSyncTracks");
		}
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

}
