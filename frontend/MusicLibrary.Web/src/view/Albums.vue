<template>
  <div>
    <h1>Albums component</h1>
    <SearchForm @search="handleSearch" />
    <DateForm @start-date-change="handleStartDateChange" @end-date-change="handleEndDateChange" />
    <table>
      <thead>
        <tr>
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
</style>
<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useAlbumsStore } from '@/stores/albums';
import SearchForm from '@/components/SearchForm.vue';
import DateForm from '@/components/DateForm.vue';
import { formatDate } from '@/utils/date';
import type { Album } from '@/types/album';

const albumsStore = useAlbumsStore();

const search = ref<string>('');
const startDate = ref<string>('')
const endDate = ref<string>('')

const albums = computed(() => {
  let records = albumsStore.albums
  records = filterSearch(records)
  records = filterDate(records)
  return records;
});

const filterSearch = (records: Album[]) => {
  records = records.filter((album) => {
    if (search.value !== '') {
      return album.name.toLowerCase().includes(search.value) ||
        album.artist.toLowerCase().includes(search.value)
    }
    return true
  });
  return records;
}
const filterDate = (records: Album[]) => {
  records = records.filter((album) => {
    const recordCreatedAt = new Date(album.createdAt)
    const recordUpdatedAt = new Date(album.updatedAt)
    if (startDate.value !== '' && endDate.value !== '') {
      return (recordCreatedAt >= new Date(startDate.value) && recordCreatedAt <= new Date(endDate.value)) ||
        (recordUpdatedAt >= new Date(startDate.value) && recordUpdatedAt <= new Date(endDate.value));
    } else if (startDate.value !== '') {
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
  search.value = searchInput
}
const handleStartDateChange = (dateInput: string) => {
  startDate.value = dateInput
}
const handleEndDateChange = (dateInput: string) => {
  endDate.value = dateInput
}
onMounted(async () => {
  await albumsStore.AlbumsLoad();
});

</script>
