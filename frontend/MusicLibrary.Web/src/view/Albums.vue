<template>
  <div>
    <h1>Albums component</h1>
    <div class="filters">
      <SearchForm @search="handleSearch" />
      <DateForm @start-date-change="handleStartDateChange" @end-date-change="handleEndDateChange" />
    </div>
    <table>
      <thead>
        <tr>
          <th></th>
          <th>id</th>
          <th>navidromeId</th>
          <th>artistId</th>
          <th>artist</th>
          <th>name</th>
          <th>createdAt</th>
          <th>updatedAt</th>
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
}
</style>
<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useAlbumsStore } from '@/stores/albums';
import SearchForm from '@/components/SearchForm.vue';
import DateForm from '@/components/DateForm.vue';
import { formatDate } from '@/utils/date';
import type { Album } from '@/types/album';
import { filterSearch, filterDate, filterArtist } from '@/utils/filters';
import { useRoute } from 'vue-router';

const albumsStore = useAlbumsStore();

const search = ref<string>('');
const startDate = ref<string>('');
const endDate = ref<string>('');

const albums = computed(() => {
  let records = albumsStore.albums;
  records = filterArtist(records, artistId.value) as Album[];
  records = filterSearch(records, search.value) as Album[];
  records = filterDate(records, startDate.value, endDate.value) as Album[];
  return records;
});

const route = useRoute();
const artistId = computed(() => {
  const id = route.params.artistId;
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
  await albumsStore.AlbumsLoad();
});
</script>
