import { ref } from 'vue';
import { defineStore } from 'pinia';
import apiClient from "@/api/client";
import type { Track } from '@/types/track';

export const useTracksStore = defineStore('tracks', () => {
  const tracks = ref<Track[]>([]);
  
  const TracksLoad = async () => {
    try {
      const { data } = await apiClient.get("/albums");
      tracks.value = data.tracks;
    } catch (error) {
      console.error(error);
    }
  };
  return { tracks, TracksLoad };
});
