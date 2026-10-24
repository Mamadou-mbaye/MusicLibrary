public interface INavidromeClient
{
    Task<string> PingAsync(CancellationToken ct = default);
    Task<string> GetArtistsAsync(CancellationToken ct = default);
    Task<string> GetAlbumList2Async(Dictionary<string, string> extraParams, CancellationToken ct = default);
    Task<string> SearchAllSongsAsync  (Dictionary<string, string> extraParams,  CancellationToken ct = default);
    // Task<string> GetMusicFoldersAsync(CancellationToken ct = default);
    // Task<string> GetAlbumListAsync(CancellationToken ct = default);
    // Task<string> GetIndexesAsync(CancellationToken ct = default);
}