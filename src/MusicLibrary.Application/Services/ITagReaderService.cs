using MusicLibrary.Application.Models;
public interface ITagReaderService
{
    Task<TrackTags?> ReadTags(string filePath);
    Task<bool> SaveTags(string filesPath, TrackTags trackTags);
}