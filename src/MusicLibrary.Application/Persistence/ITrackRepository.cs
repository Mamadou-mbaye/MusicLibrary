using MusicLibrary.Application.Models;
namespace MusicLibrary.Application.Persistence;

public interface ITrackRepository
{
    Task<IEnumerable<Track>> GetTracksAsync(CancellationToken ct);
    Task UpsertAsync(Track track, CancellationToken ct);
}