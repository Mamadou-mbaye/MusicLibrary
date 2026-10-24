using MusicLibrary.Application.Models;
namespace MusicLibrary.Application.Services;

public interface IAlbumService
{
    Task<IEnumerable<Album>> GetAlbumsAsync(CancellationToken ct);
    Task<Album> GetAlbumByNavidromeIdAsync(string navidromeId, CancellationToken ct);
    Task<int> SyncAlbumsAsync(CancellationToken ct = default);
}