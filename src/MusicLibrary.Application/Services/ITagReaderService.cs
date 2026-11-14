using MusicLibrary.Application.Models;
public interface ITagReaderService
{
    Task<TrackTags?> ReadTags(string filePath);
}