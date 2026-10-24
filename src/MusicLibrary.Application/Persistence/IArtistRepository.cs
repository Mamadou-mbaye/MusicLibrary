using MusicLibrary.Application.Models;
namespace MusicLibrary.Application.Persistence;

public interface IArtistRepository
{
    Task<IEnumerable<Artist>> GetArtistsAsync(CancellationToken ct);
    Task<Artist> GetArtistByNavidromeIdAsync(string navidromeId, CancellationToken ct) ;
    Task UpsertAsync(Artist artist, CancellationToken ct);

}