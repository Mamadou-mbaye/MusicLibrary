
public class TrackTagsApi
{
    public static async Task<IResult> ReadTags(
        ITagReaderService tagReaderService,
        IConfiguration configuration,
        string filename)
    {
        var filesPath = configuration["FilesPath"];

        if (string.IsNullOrWhiteSpace(filesPath)) return Results.Problem("Files path not configured.");

        var filePath = Path.Combine(filesPath, filename);
        var trackTags = await tagReaderService.ReadTags(filePath);

        return Results.Json(new
        {
            trackTags,
            timestamputc = DateTime.UtcNow
        });
    }
}

