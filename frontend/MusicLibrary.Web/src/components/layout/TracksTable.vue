<template>
    <table>
      <thead>
        <tr>
          <th>ID</th>
          <th>Navidrome ID</th>
          <th>Album ID</th>
          <th @click="handleSort('title')">Title</th>
          <th @click="handleSort('album')">Album</th>
          <th>Artist ID</th>
          <th @click="handleSort('artist')">Artist</th>
          <th>Duration (Seconds)</th>
          <th>Path</th>
          <th>Created At</th>
          <th>Updated At</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="track in tracks" :key="track.id">
          <td>{{ track.id }}</td>
          <td>{{ track.navidromeId }}</td>
          <td>{{ track.albumId }}</td>
          <td>{{ track.title }}</td>
          <td>{{ track.album }}</td>
          <td>{{ track.artistId }}</td>
          <td>{{ track.artist }}</td>
          <td>{{ track.durationSeconds }}</td>
          <td>{{ track.path }}</td>
          <td>{{ formatDate(track.createdAt) }}</td>
          <td>{{ formatDate(track.updatedAt) }}</td>
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

.filters {
    display: flex;
    gap: 1vw;
}
</style>
<script setup lang="ts">
import type { Track } from '@/types/track';
import { formatDate } from '@/utils/date';

const props = defineProps<{
    "tracks": Track[]
}>()
const emit = defineEmits(["handle-sort"])
const handleSort = (field: keyof Track) =>  emit("handle-sort",field );
</script>
