using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

public class NavidromeClient : INavidromeClient
{

    private readonly HttpClient _http;
    private readonly NavidromeOptions _options;

    public NavidromeClient(HttpClient http, IOptions<NavidromeOptions> options)
    {
        _http = http;
        _options = options.Value;
    }

    public async Task<string> PingAsync(CancellationToken ct = default)
    {
        var url = BuildUrl("ping.view");
        var response = await _http.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> GetArtistsAsync(CancellationToken ct = default)
    {
        var url = BuildUrl("getArtists");
        var response = await _http.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> GetMusicFoldersAsync(CancellationToken ct = default)
    {
        var url = BuildUrl("getMusicFolders");
        var response = await _http.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> GetAlbumListAsync(CancellationToken ct = default)
    {
        var url = BuildUrl("getAlbumList");
        var response = await _http.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> GetAlbumList2Async(  Dictionary<string, string> extraParams, CancellationToken ct = default)
    {
        var url = BuildUrl("getAlbumList2", extraParams);
        var response = await _http.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> GetIndexesAsync( CancellationToken ct = default)
    {
        var url = BuildUrl("getIndexes" );
        var response = await _http.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
public async  Task<string> SearchAllSongsAsync(int songCount,int songOffset,  CancellationToken ct = default)
    {
        var extraParams = new Dictionary<string, string>
    {
        ["query"] = "",
        ["artistCount"] = "0",
        ["albumCount"] = "0",
        ["songCount"] = songCount.ToString(),
        ["songOffset"] = songOffset.ToString()
    };
        var url = BuildUrl("search3", extraParams);
        var response = await _http.GetAsync(url, ct);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }


    private string BuildUrl(string endpoint, Dictionary<string, string>? extraParams = null)
    {

        var salt = GenerateSalt();
        var token = ComputeToken(_options.Password, salt);


        var parameters = new List<string>
        {
            $"u={Uri.EscapeDataString(_options.Username)}",
            $"t={token}",
            $"s={Uri.EscapeDataString(salt)}",
            "v=1.16.1",
            "c=MusicLibrary",
            "f=json"
        };

        if (extraParams is not null)
        {
            parameters.AddRange(extraParams.Select(p =>
                $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
        }

        return $"{_options.BaseUrl.TrimEnd('/')}/rest/{endpoint}?{string.Join("&", parameters)}";
    }
    private static string GenerateSalt()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(6)).ToLowerInvariant();
    }
    private static string ComputeToken(string password, string salt)
    {
        var bytes = Encoding.UTF8.GetBytes(password + salt);
        var hash = MD5.HashData(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}