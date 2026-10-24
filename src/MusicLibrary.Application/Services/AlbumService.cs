using System.Text.Json;
using MusicLibrary.Application.Persistence;
using MusicLibrary.Application.Models;

namespace MusicLibrary.Application.Services;

public class AlbumService : IAlbumService
{

    private IAlbumRepository _albumRepository;
    private INavidromeClient _navidrome;
    private IArtistService _artistService;


    public AlbumService(IArtistService artistService, IAlbumRepository albumRepository, INavidromeClient navidrome)
    {
        _albumRepository = albumRepository;
        _navidrome = navidrome;
        _artistService = artistService;
    }
    private static readonly JsonSerializerOptions JsonOptions =
          new(JsonSerializerDefaults.Web);
    public async Task<IEnumerable<Album>> GetAlbumsAsync(CancellationToken ct)
    => await _albumRepository.GetAlbumsAsync(ct);
    public async Task<Album> GetAlbumByNavidromeIdAsync(string navidromeId ,CancellationToken ct)
    => await _albumRepository.GetAlbumByNavidromeIdAsync(navidromeId, ct);

    public async Task<int> SyncAlbumsAsync(CancellationToken ct = default)
    {
        int songOffset = 0;
        int songCount = 500;
        var imported = 0;
        while (true)
        {
            Dictionary<string, string> extraParams = new()
            {
                ["type"] = "alphabeticalByName",
                ["size"] = songCount.ToString(),
                ["offset"] = songOffset.ToString()
            };

            var json = await _navidrome.GetAlbumList2Async(extraParams, ct);
            var response = JsonSerializer.Deserialize<NavidromeAlbumsResponse>(
                json, JsonOptions);
            var navidromeAlbums = response?
                .SubsonicResponse?
                .AlbumList?
                .Album ?? [];

            foreach (var navidromeAlbum in navidromeAlbums)
            {
                var artista = await _artistService.GetArtistByNavidromeIdAsync(navidromeAlbum.ArtistId, ct);
                var album = new Album
                {
                    NavidromeId = navidromeAlbum.Id,
                    Name = navidromeAlbum.Name,
                    ArtistId = artista.Id,
                    CreatedAt = navidromeAlbum.Created
                };
                await _albumRepository.UpsertAsync(album, ct);
                imported++;
            }
            songOffset = songCount;
            songCount += 500;
            if (navidromeAlbums.Count == 0) break;
        }
        return imported;
    }
}