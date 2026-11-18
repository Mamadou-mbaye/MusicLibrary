<template>
  <div class="container">
    <header class="page-header">
      <h1>Track Tags</h1>
    </header>

    <form v-if="tracksTags" class="tags-form" @submit.prevent="trackTagsSave">
      <div class="fields-grid">
        <label for="track-title">
          Title
          <input id="track-title" v-model="tracksTags.title" type="text" />
        </label>
        <label for="track-artist">
          Artist
          <input id="track-artist" v-model="tracksTags.artist" type="text" />
        </label>
        <label for="track-album">
          Album
          <input id="track-album" v-model="tracksTags.album" type="text" />
        </label>
        <label for="track-number">
          Track Number
          <input id="track-number" v-model.number="tracksTags.trackNumber" type="number" />
        </label>
        <label for="disc-number">
          Disc Number
          <input id="disc-number" v-model.number="tracksTags.discNumber" type="number" />
        </label>
        <label for="track-genre">
          Genre
          <input id="track-genre" v-model="tracksTags.genre" type="text" />
        </label>
        <label for="track-year">
          Year
          <input id="track-year" v-model.number="tracksTags.year" type="number" />
        </label>
        <label for="file-name">
          filename
          <input id="file-name" v-model="tracksTags.fileName" type="text" />
        </label>
      </div>
      <button type="submit" class="btn-primary">Save</button>
    </form>
    <LoadingForm
      v-else
      :isLoading="isLoading"
      recordName="tags"
      :emptyRecordMessage="emptyRecordMessage"
    />
  </div>
</template>
<style scoped>
.fields-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(min(100%, 14rem), 1fr));
  gap: 1rem;
}

.tags-form {
  max-width: 60rem;
  padding: 0 8px;
  display: flex;
  flex-direction: column;
  gap: 1.5rem;
}

.form-actions {
  grid-column: 1 / -1;
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
  margin-top: 1rem;
}

.btn-primary {
  font: inherit;
}

label {
  display: grid;
  gap: 0.4rem;
}

input {
  box-sizing: border-box;
  width: 100%;
  min-height: 2.3rem;
}

button {
  min-height: 1.75rem;
  width: 6rem;
}
</style>
<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useRoute } from 'vue-router';
import type { Track, TrackTags } from '@/types/track';
import apiClient from '@/api/client';
import { useTracksStore } from '@/stores/tracks';
import LoadingForm from '@/components/LoadingForm.vue';

const tracksTags = ref<TrackTags>();
const isLoading = ref(true);
const tracksStore = useTracksStore();

const emptyRecordMessage = ref('No tags available for this track.');

const route = useRoute();
const trackId = computed(() => {
  const id = route.params.id;
  return typeof id === 'string' ? Number(id) : null;
});
const trackTagsLoad = async () => {
  try {
    const track = tracksStore.tracks.find((t) => t.id === trackId.value) as Track;
    if (!track) return;
    const fileName = getFilenameSplit(track.path);

    emptyRecordMessage.value = `No tags available for ${fileName}.`;
    const { data } = await apiClient.get('/tracks/readTags/' + fileName);
    tracksTags.value = data.trackTags;
  } catch (error) {
    console.error(error);
  } finally {
    isLoading.value = false;
  }
};
const trackTagsSave = async () => {
  try {
    const { data } = await apiClient.post('/tracks/saveTags/', tracksTags.value);
    console.log(data.saved);
  } catch (error) {
    console.error(error);
  }
};
const getFilenameSplit = (path: string) => {
  return path.split(/[\\/]/).pop() ?? path;
};

onMounted(async () => {
  await tracksStore.TracksLoad();
  await trackTagsLoad();
});
</script>
