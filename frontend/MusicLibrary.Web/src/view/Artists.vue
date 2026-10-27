<template>
  <h1>Artists component</h1>
  <div>
    <label for="filter-name">Artist name</label>
    <input type="text" id="filter-name" v-model="filterName" />
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
        <td>{{ artist.createdAt }}</td>
        <td>{{ artist.updatedAt }}</td>
      </tr>
    </tbody>
  </table>
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
const artistsStore = useArtistsStore();

const filterName = ref('');
const artists = computed(() => {
  if (filterName.value.trim() === '') return artistsStore.artists;
  return artistsStore.artists.filter((artists) =>
    artists.name.toLowerCase().includes(filterName.value.toLowerCase()),
  );
});

onMounted(async () => {
  await artistsStore.ArtistsLoad();
});
</script>
