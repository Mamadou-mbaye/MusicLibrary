namespace MusicLibrary.Application.Models;

public class Album
{
    public long Id { get; set; }
    public string NavidromeId { get; set; } = string.Empty;
    public long ArtistId { get; set; } 
    public string Name { get; set; } = string.Empty;
    public int? Year { get ; set; } 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}