using Dapper;
namespace MusicLibrary.Application.Persistence;
public class DatabaseInitializer
{
    private readonly INpgsqlConnectionFactory _connectionFactory;
    public DatabaseInitializer(INpgsqlConnectionFactory connectionFactory)
    {
        _connectionFactory =  connectionFactory;
    }
    public async Task TestConnectionAsync(CancellationToken ct = default)
    {
        await using var connection = await  _connectionFactory.CreateOpenConnectionAsync(ct);
        var version = await connection.ExecuteScalarAsync<string>("SELECT version();");
        
        Console.WriteLine(version);
    }
}