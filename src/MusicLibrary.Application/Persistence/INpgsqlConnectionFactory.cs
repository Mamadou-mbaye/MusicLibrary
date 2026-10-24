using Npgsql;
namespace MusicLibrary.Application.Persistence;

public interface INpgsqlConnectionFactory
{
    Task<NpgsqlConnection> CreateOpenConnectionAsync (       CancellationToken ct = default);
}
