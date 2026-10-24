using Dapper;

using MusicLibrary.Application.Models;
namespace MusicLibrary.Application.Persistence;

public class ArtistRepository : IArtistRepository
{

    private readonly INpgsqlConnectionFactory _connectionFactory;

    public ArtistRepository(INpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    public async Task<IEnumerable<Artist>> GetArtistsAsync(CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.CreateOpenConnectionAsync();
        return await connection.QueryAsync<Artist>("""
        SELECT 
            id,
            navidrome_id AS "NavidromeId",
            name,
            created_at AS "CreatedAt",
            updated_at AS "UpdatedAt"
        FROM artists;
        """);
    }
    public async Task<Artist> GetArtistByNavidromeIdAsync(string navidromeId, CancellationToken ct)
    {
        await using var connection = await _connectionFactory.CreateOpenConnectionAsync();

        var artist = 
        await connection.QueryFirstOrDefaultAsync<Artist>("""
        SELECT 
            id,
            navidrome_id AS "NavidromeId",
            name,
            created_at AS "CreatedAt",
            updated_at AS "UpdatedAt"
        FROM artists 
        WHERE navidrome_id = @navidrome_id;
        """, new {  navidrome_id = navidromeId});
        return  artist ?? new Artist() ;
    }

    public async Task UpsertAsync(Artist artist, CancellationToken ct = default)
    {
        await using var connection = await _connectionFactory.CreateOpenConnectionAsync();
        await connection.ExecuteAsync("""
        INSERT INTO artists (navidrome_id, name)
        VALUES (@navidrome_id, @name)
        ON CONFLICT (navidrome_id)
        DO UPDATE SET name = EXCLUDED.name, updated_at = now();
        """,
        new { navidrome_id = artist.NavidromeId, name = artist.Name });
    }
}