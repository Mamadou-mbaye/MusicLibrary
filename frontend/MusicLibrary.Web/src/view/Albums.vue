<template>
  <div>
    <h1>Albums component</h1>
    <div>
      <div>Albums count: {{ albums.length }}</div>
      <button @click="handleSync">Sync albums</button>
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
          <th></th>
          <th>id</th>
          <th>navidromeId</th>
          <th>artistId</th>
          <th @click="handleSort('artist')">artist</th>
          <th @click="handleSort('name')">name</th>
          <th @click="handleSort('createdAt')">createdAt</th>
          <th @click="handleSort('updatedAt')">updatedAt</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="album in albums" :key="album.id">
          <td><a :href="`/albums/${album.id}/tracks`">tracks</a></td>
          <td>{{ album.id }}</td>
          <td>{{ album.navidromeId }}</td>
          <td>{{ album.artistId }}</td>
          <td>{{ album.artist }}</td>
          <td>{{ album.name }}</td>
          <td>{{ formatDate(album.createdAt) }}</td>
          <td>{{ formatDate(album.updatedAt) }}</td>
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
import type { Album } from '@/types/album';
import { useAlbumsStore } from '@/stores/albums';
import { formatDate } from '@/utils/date';
import {
  filterSearch,
  filterDate,
  filterArtist,
  filterByIdentificationStatus,
  filterDuplicate,
} from '@/utils/filters';
import { createSortState, orderByField, toggleSort } from '@/utils/sort';
import SearchForm from '@/components/SearchForm.vue';
import DateForm from '@/components/DateForm.vue';
import CheckForm from '@/components/CheckForm.vue';
import DupCheckForm from '@/components/DupCheckForm.vue';

const albumsStore = useAlbumsStore();

const search = ref<string>('');
const startDate = ref<string>('');
const endDate = ref<string>('');
const identified = ref<boolean>(false);
const unidentified = ref<boolean>(false);
const duplicate = ref<boolean>(false);
const sortState = ref(createSortState<Album>('name'));

const albums = computed(() => {
  let records = albumsStore.albums;

  records = filterByIdentificationStatus(records, identified.value, unidentified.value) as Album[];
  records = filterArtist(records, artistId.value) as Album[];
  records = filterSearch(records, search.value) as Album[];
  records = filterDate(records, startDate.value, endDate.value) as Album[];
  records = filterDuplicate(records, duplicate.value) as Album[];
  return records;
});

const route = useRoute();
const artistId = computed(() => {
  const id = route.params.artistId;
  return typeof id === 'string' ? Number(id) : null;
});

const handleSearch = (searchInput: string) => (search.value = searchInput);
const handleStartDateChange = (dateInput: string) => (startDate.value = dateInput);
const handleEndDateChange = (dateInput: string) => (endDate.value = dateInput);
const handleIdentified = (checkInput: boolean) => (identified.value = checkInput);
const handleUnidentified = (checkInput: boolean) => (unidentified.value = checkInput);
const handleDupplicate = (checkInput: boolean) => (duplicate.value = checkInput);
const handleSync = async () => await albumsStore.AlbumsSync();
const handleSort = (field: keyof Album) => {
  sortState.value = toggleSort(sortState.value, field);
  albumsStore.albums = orderByField(albumsStore.albums, field, sortState.value.direction);
};

onMounted(async () => {
  await albumsStore.AlbumsLoad();
});
</script>
