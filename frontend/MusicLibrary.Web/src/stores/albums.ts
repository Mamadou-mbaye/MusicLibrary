import { ref } from 'vue';
import { defineStore } from 'pinia';
import apiClient from '@/api/client';
import type { Album } from '@/types/album';

export const useAlbumsStore = defineStore('albums', () => {
  const albums = ref<Album[]>([]);
  const AlbumsLoad = async () => {
    try {
      const { data } = await apiClient.get('/albums');
      albums.value = data.albums;
    } catch (error) {
      console.error(error);
    }
  };
  return { albums, AlbumsLoad };
});
