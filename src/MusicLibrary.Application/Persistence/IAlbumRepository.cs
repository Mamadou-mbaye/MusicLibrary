using MusicLibrary.Application.Models;
namespace MusicLibrary.Application.Persistence;

public interface IAlbumRepository
{
    Task<IEnumerable<Album>> GetAlbumsAsync(CancellationToken ct);
    Task <Album> GetAlbumByNavidromeIdAsync(string navidromeId,CancellationToken ct);    
    Task UpsertAsync(Album album, CancellationToken ct);
}