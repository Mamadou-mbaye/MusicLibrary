export interface Track {
  id: number;
  navidromeId: string;
  albumId: number;
  album: string;
  artistId: number;
  artist: string;
  title: string;
  durationSeconds: number;
  path: string;
  createdAt: string;
  updatedAt: string;
}
export interface TrackTags {
  title: string;
  artist: string;
  artistId: number;
  album: string;
  albumId: number;
  trackNumber: number;
  discNumber: number;
  genre: string;
  year: number;
  fileName: string;
}
