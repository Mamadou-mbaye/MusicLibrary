using MusicLibrary.Application.Models;
public class TagReaderService : ITagReaderService
{
    public Task<TrackTags?> ReadTags(string filePath)
    {
        var file = TagLib.File.Create(filePath);
        var tags = new TrackTags
        {
            Title = file.Tag.Title,
            Artist = file.Tag.FirstPerformer,
            Album = file.Tag.Album,
            TrackNumber = (int?)file.Tag.Track,
            DiscNumber = (int?)file.Tag.Disc,
            Genre = file.Tag.FirstGenre,
            Year = (int?)file.Tag.Year,
            FileName = Path.GetFileName(file.Name)
        };

        return Task.FromResult<TrackTags?>(tags);
    }
    public async Task<bool> SaveTags(string filesPath, TrackTags trackTags)
    {
        var filePath = Path.Combine(filesPath, trackTags.FileName);
        var file = TagLib.File.Create(filePath);

        file.Tag.Title = trackTags.Title;
        file.Tag.Album = trackTags.Album;
        file.Tag.Track = (UInt32)(trackTags.TrackNumber ?? 0);
        file.Tag.Disc = (UInt32)(trackTags.DiscNumber ?? 0);
        file.Tag.Year = (UInt32)(trackTags.Year ?? 0);
        file.Save();

        return true;
    }
}
