import type { Artist } from '@/types/artist';
import type { Album } from '@/types/album';
import type { Track } from '@/types/track';

const getFilterFields = (record: Artist | Album | Track): string[] => {
  if ('title' in record) return [record.title, record.artist, record.album];

  if ('name' in record) {
    return 'artist' in record && typeof record.artist === 'string'
      ? [record.name, record.artist]
      : [record.name];
  }

  return [];
};
const getFilterField = (record: Artist | Album | Track): string[] => {
  if ('title' in record) return [record.title];
  if ('name' in record) return [record.name];
  return [];
};

const filterSearch = (records: (Artist | Album | Track)[], search: string) => {
  const query = search.toLowerCase();
  if (query === '') return records;

  return records.filter((record) => {
    const isRecord = (value: string) => value.toLowerCase().includes(query);
    return getFilterFields(record).some(isRecord);
  });
};

const filterDate = (records: (Artist | Album | Track)[], startDate: string, endDate: string) => {
  records = records.filter((record) => {
    const recordCreatedAt = new Date(record.createdAt);
    const recordUpdatedAt = new Date(record.updatedAt);
    if (startDate !== '' && endDate !== '')
      return (
        (recordCreatedAt >= new Date(startDate) && recordCreatedAt <= new Date(endDate)) ||
        (recordUpdatedAt >= new Date(startDate) && recordUpdatedAt <= new Date(endDate))
      );
    else if (startDate !== '')
      return recordCreatedAt >= new Date(startDate) || recordUpdatedAt >= new Date(startDate);
    else if (endDate !== '')
      return recordCreatedAt <= new Date(endDate) || recordUpdatedAt <= new Date(endDate);
    return true;
  });

  return records;
};

const filterArtist = (records: (Album | Track)[], artistId: number | null) => {
  if (artistId) return records.filter((record) => record.artistId === artistId);
  return records;
};

const filterAlbum = (records: Track[], albumId: number | null) => {
  if (albumId) return records.filter((record) => record.albumId === albumId);
  return records;
};

const filterByIdentificationStatus = (
  records: (Artist | Album | Track)[],
  identified: boolean,
  unidentified: boolean,
) => {
  if (identified === unidentified) return records;
  const isUnknown = (value: string) => value.toLowerCase().includes('unknown');

  return records.filter((record) => {
    const hasUnknownField = getFilterFields(record).some(isUnknown);
    return identified ? !hasUnknownField : hasUnknownField;
  });
};

const filterUnidentified = (
  records: (Artist | Album | Track)[],
  identified: boolean,
  unidentified: boolean,
) => {
  if (identified === unidentified) return records;
  const isUnknown = (value: string) => value.toLowerCase().includes('unknown');

  return records.filter((record) => {
    const hasUnknownField = getFilterField(record).some(isUnknown);
    return identified ? !hasUnknownField : hasUnknownField;
  });
};

const filterDuplicate = (records: (Artist | Album | Track)[], duplicate: boolean) => {
  if (!duplicate) return records;
  const counts = new Map<string, number>();

  for (const record of records) {
    const key =
      'title' in record ? record.title.trim().toLowerCase() : record.name.trim().toLowerCase();

    counts.set(key, (counts.get(key) ?? 0) + 1);
  }

  return records.filter((record) => {
    const key =
      'title' in record ? record.title.trim().toLowerCase() : record.name.trim().toLowerCase();

    return (counts.get(key) ?? 0) > 1;
  });
};

export {
  filterSearch,
  filterDate,
  filterArtist,
  filterAlbum,
  filterByIdentificationStatus,
  filterDuplicate,
  filterUnidentified,
};
