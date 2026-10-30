<template>
  <div>
    <h1>Artists component</h1>
    <div class="filters">
      <SearchForm @search="handleSearch" />
      <DateForm @start-date-change="handleStartDateChange" @end-date-change="handleEndDateChange" />
    </div>

    <table>
      <thead>
        <tr>
          <th></th>
          <th></th>
          <th>id</th>
          <th>navidromeId</th>
          <th>name</th>
          <th>createdAt</th>
          <th>updatedAt</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="artist in artists" :key="artist.id">
          <td><a :href="`/artists/${artist.id}/albums`">albums</a></td>
          <td><a :href="`/artists/${artist.id}/tracks`">tracks</a></td>
          <td>{{ artist.id }}</td>
          <td>{{ artist.navidromeId }}</td>
          <td>{{ artist.name }}</td>
          <td>{{ formatDate(artist.createdAt) }}</td>
          <td>{{ formatDate(artist.updatedAt) }}</td>
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
import { useArtistsStore } from '@/stores/artists';
import SearchForm from '@/components/SearchForm.vue';
import DateForm from '@/components/DateForm.vue';
import { formatDate } from '@/utils/date';
import type { Artist } from '@/types/artist';
import { filterSearch, filterDate } from '@/utils/filters';

const artistsStore = useArtistsStore();

const search = ref<string>('');
const startDate = ref<string>('');
const endDate = ref<string>('');

const artists = computed(() => {
  let records = artistsStore.artists;
  records = filterSearch(records, search.value) as Artist[];
  records = filterDate(records, startDate.value, endDate.value) as Artist[];
  return records;
});

const handleSearch = (searchInput: string) => {
  search.value = searchInput.trim();
};
const handleStartDateChange = (dateInput: string) => {
  startDate.value = dateInput.trim();
};
const handleEndDateChange = (dateInput: string) => {
  endDate.value = dateInput.trim();
};
onMounted(async () => {
  await artistsStore.ArtistsLoad();
});
</script>
