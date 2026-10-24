using MusicLibrary.Application.Models;
namespace MusicLibrary.Application.Services;
public interface ITrackService
{
    Task<IEnumerable<Track>> GetTracksAsync(CancellationToken ct);
    Task<int> SyncTracksAsync(CancellationToken ct = default);
}