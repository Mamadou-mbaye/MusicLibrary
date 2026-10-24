using Dapper;
using MusicLibrary.Application.Models;


namespace MusicLibrary.Application.Persistence;

public class AlbumRepository : IAlbumRepository
{
    private readonly INpgsqlConnectionFactory _connectionFactory;

    public AlbumRepository(INpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<IEnumerable<Album>> GetAlbumsAsync(CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.CreateOpenConnectionAsync();
        return await connection.QueryAsync<Album>("""
        SELECT 
            id ,
            navidrome_id AS navidromeId,
            artist_id AS artistId,
            name, 
            year,
            created_at AS createdAt,
            updated_at AS updatedAt
        FROM albums;
        """);
    }
    public async Task<Album> GetAlbumByNavidromeIdAsync(string navidromeId, CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.CreateOpenConnectionAsync();
        var album =
        await connection.QueryFirstOrDefaultAsync<Album>("""
        SELECT 
            id ,
            navidrome_id AS navidromeId,
            artist_id AS artistId,
            name, 
            year,
            created_at AS createdAt,
            updated_at AS updatedAt
        FROM albums WHERE navidrome_id = @navidrome_id;
        """, new { navidrome_id = navidromeId });
        return album ?? new Album();
    }
    public async Task UpsertAsync(Album album, CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.CreateOpenConnectionAsync();
        await connection.ExecuteAsync("""
        INSERT INTO albums (navidrome_id, artist_id, name, year, created_at)
        VALUES (@navidrome_id, @artist_id, @name, @year, @created_at)
        ON CONFLICT (navidrome_id)
        DO UPDATE SET
        artist_id = EXCLUDED.artist_id,
        name = EXCLUDED.name,
        year = EXCLUDED.year,
        updated_at = now();
        """,
        new
        {
            navidrome_id = album.NavidromeId,
            artist_id = album.ArtistId,
            name = album.Name,
            year = album.Year,
            created_at = album.CreatedAt
        });
    }

}