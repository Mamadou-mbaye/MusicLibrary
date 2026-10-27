using System.Text.Json.Nodes;

namespace MusicLibrary.Application.Models;

public class Track
{
    public long Id { get; set; }
    public string NavidromeId { get; set; } = string.Empty;
    public long AlbumId { get; set; } 
    public string Title { get; set; } = string.Empty;
    public int DurationSeconds { get ; set; } 
    public string Path { get ; set;  } = string .Empty; 
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Album { get; set; } = string.Empty; 
    public string Artist { get; set; } = string.Empty; 
    public long ArtistId { get; set; }
    // public int TrackNumber { get ; set; } 
    // public JsonArray RawTags { get ; set;  } = new () ; 
}