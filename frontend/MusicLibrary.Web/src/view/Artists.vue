<template>
  <div>
    <h1>Artists component</h1>
    <div>
      <SearchForm @search="handleSearch" />
      <DateForm @start-date-change="handleStartDateChange" @end-date-change="handleEndDateChange" />

      <button type="button">Search</button>
    </div>

    <table>
      <thead>
        <tr>
          <th>id</th>
          <th>navidromeId</th>
          <th>name</th>
          <th>createdAt</th>
          <th>updatedAt</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="artist in artists" :key="artist.id">
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
</style>
<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useArtistsStore } from '@/stores/artists';
import SearchForm from '@/components/SearchForm.vue';
import DateForm from '@/components/DateForm.vue';
import { formatDate } from '@/utils/date';
import type { Artist } from '@/types/artist';

const artistsStore = useArtistsStore();

const search = ref<string>('');
const startDate = ref<string>('')
const endDate = ref<string>('')


const artists = computed(() => {
  // if (search.value.trim() === '') return artistsStore.artists;
  // return artistsStore.artists.filter((artist) =>
  //   artist.name.toLowerCase().includes(search.value.toLowerCase()),
  // );
  let records = artistsStore.artists
  records = filterSearch(records)
  records = filterDate(records)
  return records;
});

const filterSearch = (records: Artist[]) => {
  records = records.filter((artist) => {
    if (search.value !== '') {
      return artist.name.toLowerCase().includes(search.value.toLowerCase())
    }
    return true
  });
  return records;
}
const filterDate = (records: Artist[]) => {
  records = records.filter((artist) => {
    const recordCreatedAt = new Date(artist.createdAt)
    const recordUpdatedAt = new Date(artist.updatedAt)
    if (startDate.value !== '' && endDate.value !== '') {
      return (recordCreatedAt >= new Date(startDate.value) && recordCreatedAt <= new Date(endDate.value)) ||
        (recordUpdatedAt >= new Date(startDate.value) && recordUpdatedAt <= new Date(endDate.value));
    }
     else if (startDate.value !== '') {
      return recordCreatedAt >= new Date(startDate.value) || recordUpdatedAt >= new Date(startDate.value);
    }
    else if (endDate.value !== '') {
      return recordCreatedAt <= new Date(endDate.value) || recordUpdatedAt <= new Date(endDate.value);
    }
    return true
  });
  return records;
}

const handleSearch = (searchInput: string) => {
  search.value = searchInput.trim()
}
const handleStartDateChange = (dateInput: string) => {
  startDate.value = dateInput.trim()
}
const handleEndDateChange = (dateInput: string) => {
  endDate.value = dateInput.trim()
}
onMounted(async () => {
  await artistsStore.ArtistsLoad();
});
</script>
