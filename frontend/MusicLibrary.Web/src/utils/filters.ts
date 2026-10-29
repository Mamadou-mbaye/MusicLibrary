import type { Artist } from "@/types/artist";
import type { Album } from "@/types/album";
import type { Track } from "@/types/track";


const filterSearch = (records: (Artist | Album | Track)[], search: string) => {
  const query = search.toLowerCase();
  if (query === "") return records;

  return records.filter((record) => {
    if ("title" in record) {
      return (
        record.title.toLowerCase().includes(query) ||
        record.artist.toLowerCase().includes(query) ||
        record.album.toLowerCase().includes(query)
      );
    }

    if ("name" in record) {
      return (
        record.name.toLowerCase().includes(query) ||
        ("artist" in record &&
          typeof record.artist === "string" &&
          record.artist.toLowerCase().includes(query))
      );
    }
    return false;
  });
};

 const filterDate = (
  records: (Artist | Album | Track)[],
  startDate: string,
  endDate: string,
) => {
  records = records.filter((record) => {
    const recordCreatedAt = new Date(record.createdAt);
    const recordUpdatedAt = new Date(record.updatedAt);
    if (startDate !== "" && endDate !== "")
      return (
        (recordCreatedAt >= new Date(startDate) && recordCreatedAt <= new Date(endDate)) ||
        (recordUpdatedAt >= new Date(startDate) && recordUpdatedAt <= new Date(endDate))
      );
    else if (startDate !== "")
      return recordCreatedAt >= new Date(startDate) || recordUpdatedAt >= new Date(startDate);
    else if (endDate !== "")
      return recordCreatedAt <= new Date(endDate) || recordUpdatedAt <= new Date(endDate);
    return true;
  });

  return records;
};

export { filterSearch,  filterDate}