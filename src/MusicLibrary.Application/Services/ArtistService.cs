using System.Text.Json;
using MusicLibrary.Application.Persistence;
using MusicLibrary.Application.Models;

namespace MusicLibrary.Application.Services;

public class ArtistService : IArtistService
{

    private IArtistRepository _artistRepository;
    private INavidromeClient _navidrome;

    public ArtistService(IArtistRepository artistRepository, INavidromeClient navidrome)
    {
        _artistRepository = artistRepository;
        _navidrome = navidrome;
    }
    private static readonly JsonSerializerOptions JsonOptions =
          new(JsonSerializerDefaults.Web);
    public async Task<IEnumerable<Artist>> GetArtistsAsync(CancellationToken ct)
    => await _artistRepository.GetArtistsAsync(ct);
    public async Task<Artist> GetArtistByNavidromeIdAsync(string navidromeId, CancellationToken ct)
     => await _artistRepository.GetArtistByNavidromeIdAsync(navidromeId, ct);

    public async Task<int> SyncArtistsAsync(CancellationToken ct = default)
    {
        var json = await _navidrome.GetArtistsAsync(ct);

        var response = JsonSerializer.Deserialize<NavidromeArtistsResponse>(
            json, JsonOptions);

        var navidromeArtists = response?
            .SubsonicResponse?
            .Artists?
            .Index?
            .SelectMany(index => index.Artist)
            .ToList() ?? [];

        var imported = 0;
        foreach (var navidromeArtist in navidromeArtists)
        {
            var artist = new Artist
            {
                NavidromeId = navidromeArtist.Id,
                Name = navidromeArtist.Name
            };

            await _artistRepository.UpsertAsync(artist, ct);
            imported++;
        }

        return imported;
    }

}