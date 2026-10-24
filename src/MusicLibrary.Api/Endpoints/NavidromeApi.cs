using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http.HttpResults;


public class NavidromeApi
{

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
	public static async Task<IResult> GetAlbumList2(NavidromeClient client, CancellationToken ct)
	{
		int songCount = 500;
		int songOffset = 0;
		Dictionary<string, string> extraParams = new()
		{
			["type"] = "alphabeticalByName",
			["size"] = songCount.ToString(),
			["offset"] = songOffset.ToString()
		};
		var result = await client.GetAlbumList2Async(extraParams, ct);
		return Results.Content(result, "application/json");
	}
	public static async Task<IResult> SearchAllSongs(NavidromeClient client, CancellationToken ct)
	{
		const int songCount = 500;
		var songOffset = 0;
		var extraParams = new Dictionary<string, string>
		{
			["query"] = "",
			["artistCount"] = "0",
			["albumCount"] = "0",
			["songCount"] = songCount.ToString(),
			["songOffset"] = songOffset.ToString()
		};

		var result = await client.SearchAllSongsAsync(extraParams, ct);
		return Results.Content(result, "application/json");
	}

	// public static async Task<IResult> GetMusicFolders(NavidromeClient client, CancellationToken ct)
	// {
	// 	var result = await client.GetMusicFoldersAsync(ct);
	// 	return Results.Content(result, "application/json");
	// }
	// public static async Task<IResult> GetAlbumList(NavidromeClient client, CancellationToken ct)
	// {
	// 	var result = await client.GetAlbumListAsync(ct);
	// 	return Results.Content(result, "application/json");
	// }
	// public static async Task<IResult> GetIndexes(NavidromeClient client, CancellationToken ct)
	// {
	// 	var result = await client.GetIndexesAsync(ct);
	// 	return Results.Content(result, "application/json");
	// }

}