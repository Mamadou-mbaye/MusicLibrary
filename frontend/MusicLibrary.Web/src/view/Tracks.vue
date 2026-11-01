<template>
  <div>
    <h1>Tracks component</h1>
    <div>
      <div>Tracks count: {{ tracks.length }}</div>
      <button @click="handleSync">Sync tracks</button>
    </div>
    <div class="filters">
      <SearchForm @search="handleSearch" />
      <DateForm @start-date-change="handleStartDateChange" @end-date-change="handleEndDateChange" />
      <CheckForm @handle-identified="handleIdentified" @handle-unidentified="handleUnidentified" />
      <DupCheckForm @handle-dupplicate="handleDupplicate" />
    </div>
    <table>
      <thead>
        <tr>
          <th>id</th>
          <th>navidromeId</th>
          <th>albumId</th>
          <th @click="handleSort('album')">album</th>
          <th>artistId</th>
          <th @click="handleSort('artist')">artist</th>
          <th @click="handleSort('title')">title</th>

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
  gap: 1vw;
}
</style>
<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useRoute } from 'vue-router';
import type { Track } from '@/types/track';
import { useTracksStore } from '@/stores/tracks';
import { formatDate } from '@/utils/date';
import {
  filterSearch,
  filterDate,
  filterArtist,
  filterAlbum,
  filterByIdentificationStatus,
  filterDuplicate,
} from '@/utils/filters';
import { createSortState, orderByField, toggleSort } from '@/utils/sort';
import SearchForm from '@/components/SearchForm.vue';
import DateForm from '@/components/DateForm.vue';
import CheckForm from '@/components/CheckForm.vue';
import DupCheckForm from '@/components/DupCheckForm.vue';

const tracksStore = useTracksStore();

const search = ref<string>('');
const startDate = ref<string>('');
const endDate = ref<string>('');
const identified = ref<boolean>(false);
const unidentified = ref<boolean>(false);
const duplicate = ref<boolean>(false);
const sortState = ref(createSortState<Track>('title'));

const tracks = computed(() => {
  let records = tracksStore.tracks;

  records = filterByIdentificationStatus(records, identified.value, unidentified.value) as Track[];
  records = filterArtist(records, artistId.value) as Track[];
  records = filterAlbum(records, albumId.value) as Track[];
  records = filterSearch(records, search.value) as Track[];
  records = filterDate(records, startDate.value, endDate.value) as Track[];
  records = filterDuplicate(records, duplicate.value) as Track[];
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

const handleSearch = (searchInput: string) => (search.value = searchInput);
const handleStartDateChange = (dateInput: string) => (startDate.value = dateInput);
const handleEndDateChange = (dateInput: string) => (endDate.value = dateInput);
const handleIdentified = (checkInput: boolean) => (identified.value = checkInput);
const handleUnidentified = (checkInput: boolean) => (unidentified.value = checkInput);
const handleDupplicate = (checkInput: boolean) => (duplicate.value = checkInput);
const handleSync = async () => await tracksStore.TracksSync();
const handleSort = (field: keyof Track) => {
  sortState.value = toggleSort(sortState.value, field);
  tracksStore.tracks = orderByField(tracksStore.tracks, field, sortState.value.direction);
};

onMounted(async () => {
  await tracksStore.TracksLoad();
});
</script>
