namespace MusicLibrary.Application.Models;

public class TrackTags
{
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string Album { get; set; } = string.Empty;
    public int? TrackNumber { get; set; }
    public int? DiscNumber { get; set; }
    public string Genre { get; set; } = string.Empty;
    public int? Year { get; set; }
    public string FileName { get; set; } = string.Empty;
}
