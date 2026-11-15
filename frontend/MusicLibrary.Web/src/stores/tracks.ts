import { ref } from 'vue';
import { defineStore } from 'pinia';
import apiClient from '@/api/client';
import type { Track } from '@/types/track';

export const useTracksStore = defineStore('tracks', () => {
  const tracks = ref<Track[]>([]);
  const isLoading = ref(true);

  const TracksLoad = async () => {
    try {
      isLoading.value = true;
      const { data } = await apiClient.get('/tracks');
      tracks.value = data.tracks;
    } catch (error) {
      console.error(error);
    } finally {
      isLoading.value = false;
    }
  };
  const TracksSync = async () => {
    try {
      isLoading.value = true;
      tracks.value = [];
      await apiClient.post('/tracks/sync');
      await TracksLoad();
    } catch (error) {
      console.error(error);
    } finally {
      isLoading.value = false;
    }
  };
  return { tracks, TracksLoad, TracksSync, isLoading };
});
