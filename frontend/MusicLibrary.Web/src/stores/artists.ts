import { ref } from "vue";
import { defineStore } from "pinia";
import apiClient from "@/api/client.ts";
import type { Artist } from "@/types/artist";

export const useArtistsStore = defineStore("artists", () => {
  const artists = ref<Artist[]>([]);

  const ArtistsLoad = async () => {
    try {
      const { data } = await apiClient.get("/artists");
      artists.value = data.artists;
    } catch (error) {
      console.error(error);
    }
  };
  return { artists, ArtistsLoad };
});
