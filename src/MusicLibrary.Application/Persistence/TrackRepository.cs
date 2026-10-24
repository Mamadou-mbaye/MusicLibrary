using Dapper;
using MusicLibrary.Application.Models;


namespace MusicLibrary.Application.Persistence;

public class TrackRepository : ITrackRepository
{
    private readonly INpgsqlConnectionFactory _connectionFactory;

    public TrackRepository(INpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<IEnumerable<Track>> GetTracksAsync(CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.CreateOpenConnectionAsync();
        return await connection.QueryAsync<Track>("""
        SELECT 
            id ,
            navidrome_id AS navidromeid ,
            album_id AS albumid,
            title ,
            duration_seconds AS durationseconds,
            path ,
            created_at AS createdat,
            updated_at AS updatedat
        FROM tracks;
        """);
    }
    public async Task UpsertAsync(Track track, CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.CreateOpenConnectionAsync();
        await connection.ExecuteAsync("""
        INSERT INTO tracks(navidrome_id, album_id, title, duration_seconds,path , created_at)
        VALUES(@navidrome_id, @album_id, @title,@duration_seconds,@path, @created_at)
        ON CONFLICT (navidrome_id)
        DO UPDATE SET
            album_id = EXCLUDED.album_id,
            title = EXCLUDED.title,
            duration_seconds = EXCLUDED.duration_seconds,
            path = EXCLUDED.path,
            updated_at = now();      
        """,
        new
        {
            navidrome_id = track.NavidromeId,
            album_id = track.AlbumId,
            title = track.Title,
            duration_seconds = track.DurationSeconds,
            path = track.Path,
            created_at = track.CreatedAt
        });
    }
}