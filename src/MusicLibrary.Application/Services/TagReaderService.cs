using MusicLibrary.Application.Models;
public class TagReaderService : ITagReaderService
{
    public  Task<TrackTags?> ReadTags(string filePath)
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
            Year = (int?)file.Tag.Year
        };

        return  Task.FromResult<TrackTags?>(tags);
    }
}
