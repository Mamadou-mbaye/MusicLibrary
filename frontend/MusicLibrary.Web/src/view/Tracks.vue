<template>
  <h1>Tracks component</h1>
  <div>
    <label for="filter-name">Track name</label>
    <input type="text" id="filter-name" v-model="filterName" />
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
        <td>{{ track.createdAt }}</td>
        <td>{{ track.updatedAt }}</td>
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
import { useTracksStore } from '@/stores/tracks';

const tracksStore = useTracksStore();

const filterName = ref('');
 
const tracks = computed(() => {
  if (filterName.value.trim() === '') return tracksStore.tracks ;
  return tracksStore.tracks.filter((tracks) =>
    tracks.title.toLowerCase().includes(filterName.value.toLowerCase()),
  );
});

onMounted(async () => {
 await tracksStore.TracksLoad();
});
</script>
