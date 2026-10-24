namespace MusicLibrary.Application.Models;

public class Artist
{
    public long Id { get; set; }
    public string NavidromeId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}