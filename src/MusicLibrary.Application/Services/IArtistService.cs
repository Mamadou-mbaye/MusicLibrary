using MusicLibrary.Application.Models;
namespace MusicLibrary.Application.Services;

public interface IArtistService
{
    Task<IEnumerable<Artist>> GetArtistsAsync(CancellationToken ct);
    Task<Artist> GetArtistByNavidromeIdAsync(string navidromeId, CancellationToken ct);
    Task<int> SyncArtistsAsync(CancellationToken ct = default);
}