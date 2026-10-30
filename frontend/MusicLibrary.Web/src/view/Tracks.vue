<template>
  <div>
    <h1>Tracks component</h1>
    <div class="filters">
      <SearchForm @search="handleSearch" />
      <DateForm @start-date-change="handleStartDateChange" @end-date-change="handleEndDateChange" />
    </div>
    <table>
      <thead>
        <tr>
          <th>id</th>
          <th>navidromeId</th>
          <th>albumId</th>
          <th>album</th>
          <th>artistId</th>
          <th>artist</th>
          <th>title</th>
          <th>durationSeconds</th>
          <th>path</th>
          <th>createdAt</th>
          <th>updatedAt</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="track in tracks" :key="track.id">
          <td>{{ track.id }}</td>
          <td>{{ track.navidromeId }}</td>
          <td>{{ track.albumId }}</td>
          <td>{{ track.album }}</td>
          <td>{{ track.artistId }}</td>
          <td>{{ track.artist }}</td>
          <td>{{ track.title }}</td>
          <td>{{ track.durationSeconds }}</td>
          <td>{{ track.path }}</td>
          <td>{{ formatDate(track.createdAt) }}</td>
          <td>{{ formatDate(track.updatedAt) }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
<style scoped>
#div-container {
  display: flex;
  gap: 1vw;
}

table {
  font-family: arial, sans-serif;
  border-collapse: collapse;
  width: 100%;
}

td,
th {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}

th {
  background-color: #da9f9f;
}
.filters {
  display: flex;
}
</style>
<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useTracksStore } from '@/stores/tracks';
import SearchForm from '@/components/SearchForm.vue';
import DateForm from '@/components/DateForm.vue';
import { formatDate } from '@/utils/date';
import type { Track } from '@/types/track';
import { filterSearch, filterDate, filterArtist, filterAlbum } from '@/utils/filters';
import { useRoute } from 'vue-router';

const tracksStore = useTracksStore();

const search = ref<string>('');
const startDate = ref<string>('');
const endDate = ref<string>('');

const tracks = computed(() => {
  let records = tracksStore.tracks;
  records = filterArtist(records, artistId.value) as Track[];
  records = filterAlbum(records, albumId.value) as Track[];
  records = filterSearch(records, search.value) as Track[];
  records = filterDate(records, startDate.value, endDate.value) as Track[];
  return records;
});

const route = useRoute();
const artistId = computed(() => {
  const id = route.params.artistId;
  return typeof id === 'string' ? Number(id) : null;
});
const albumId = computed(() => {
  const id = route.params.albumId;
  return typeof id === 'string' ? Number(id) : null;
});

const handleSearch = (searchInput: string) => {
  search.value = searchInput;
};
const handleStartDateChange = (dateInput: string) => {
  startDate.value = dateInput;
};
const handleEndDateChange = (dateInput: string) => {
  endDate.value = dateInput;
};
onMounted(async () => {
  await tracksStore.TracksLoad();
});
</script>
