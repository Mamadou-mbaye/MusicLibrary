<template>
  <h1>Albums component</h1>
  <div>
    <label for="filter-name">Albums name</label>
    <input type="text" id="filter-name" v-model="filterName" />
  </div>

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
        <td>{{ album.createdAt }}</td>
        <td>{{ album.updatedAt }}</td>
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
import { useAlbumsStore } from '@/stores/albums';

const  albumsStore = useAlbumsStore();

const filterName = ref('');
 
const albums = computed(() => {
  if (filterName.value.trim() === '') return albumsStore.albums;
  return  albumsStore.albums.filter((albums) =>
    albums.name.toLowerCase().includes(filterName.value.toLowerCase()),
  );
});

onMounted(async () => {
 await albumsStore.AlbumsLoad();
});
</script>
