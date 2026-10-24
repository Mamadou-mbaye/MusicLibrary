using System.Text.Json;
using MusicLibrary.Application.Persistence;
using MusicLibrary.Application.Models;

namespace MusicLibrary.Application.Services;

public class TrackService : ITrackService
{

    private ITrackRepository _trackRepository;
    private INavidromeClient _navidrome;
    private IAlbumService _albumService;

    public TrackService(IAlbumService albumService, ITrackRepository trackRepository, INavidromeClient navidrome)
    {
        _trackRepository = trackRepository;
        _navidrome = navidrome;
        _albumService = albumService;
    }
    private static readonly JsonSerializerOptions JsonOptions =
          new(JsonSerializerDefaults.Web);

    public async Task<IEnumerable<Track>> GetTracksAsync(CancellationToken ct)
    => await _trackRepository.GetTracksAsync(ct);
    public async Task<int> SyncTracksAsync(CancellationToken ct = default)
    {
        int songOffset = 0;
        int songCount = 500;
        var imported = 0;
        while (true)
        {
            var extraParams = new Dictionary<string, string>
            {
                ["query"] = "",
                ["artistCount"] = "0",
                ["albumCount"] = "0",
                ["songCount"] = songCount.ToString(),
                ["songOffset"] = songOffset.ToString()
            };

            var json = await _navidrome.SearchAllSongsAsync(extraParams, ct);
            var response = JsonSerializer.Deserialize<NavidromeTracksResponse>(
                json, JsonOptions);
            var navidromeTracks = response?
                .SubsonicResponse?
                .TrackList?
                .Track ?? [];

            foreach (var navidromeTrack in navidromeTracks)
            {
                var album = await _albumService.GetAlbumByNavidromeIdAsync(navidromeTrack.AlbumId, ct);
                var track = new Track
                {
                    NavidromeId = navidromeTrack.Id,
                    Title = navidromeTrack.Title,
                    AlbumId = album.Id,
                    DurationSeconds = navidromeTrack.DurationSeconds,
                    Path = navidromeTrack.Path,
                    CreatedAt = navidromeTrack.Created
                };

                await _trackRepository.UpsertAsync(track, ct);
                imported++;
            }
            songOffset = songCount;
            songCount += 500;
            if (navidromeTracks.Count == 0) break;
        }
        return imported;
    }
}