import { ref } from 'vue';
import { defineStore } from 'pinia';
import apiClient from '@/api/client.ts';
import type { Artist } from '@/types/artist';

export const useArtistsStore = defineStore('artists', () => {
  const artists = ref<Artist[]>([]);
  const isLoading = ref(true);

  const ArtistsLoad = async () => {
    try {
      isLoading.value = true;
      const { data } = await apiClient.get('/artists');
      artists.value = data.artists;
    } catch (error) {
      console.error(error);
    } finally {
      isLoading.value = false;
    }
  };
  const ArtistsSync = async () => {
    try {
      isLoading.value = true;
      artists.value = [];
      await apiClient.post('/artists/sync');
      await ArtistsLoad();
    } catch (error) {
      console.error(error);
    } finally {
      isLoading.value = false;
    }
  };
  return { artists, ArtistsLoad, ArtistsSync, isLoading };
});
