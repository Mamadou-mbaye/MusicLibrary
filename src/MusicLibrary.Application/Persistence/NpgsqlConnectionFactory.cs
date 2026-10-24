using Dapper;
using Npgsql;

// namespace MusicLibrary.Infrastructure;
public interface INpgsqlConnectionFactory
{
    Task<NpgsqlConnection> CreateOpenConnectionAsync(
        CancellationToken ct = default);
}
public class NpgsqlConnectionFactory : INpgsqlConnectionFactory
{
    private readonly NpgsqlDataSource _dataSource;
    public NpgsqlConnectionFactory(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<NpgsqlConnection> CreateOpenConnectionAsync(CancellationToken ct = default)
    {
        return await _dataSource.OpenConnectionAsync(ct);

    }
}