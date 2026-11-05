<template>
    <table>
      <thead>
        <tr>
          <th>Tracks</th>
          <th>ID</th>
          <th>Navidrome ID</th>
          <th>Artist ID</th>
          <th @click="handleSort('artist')">Artist</th>
          <th @click="handleSort('name')">Album Name</th>
          <th @click="handleSort('createdAt')">Created At</th>
          <th @click="handleSort('updatedAt')">Updated At</th>
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
import type { Album } from '@/types/album';
import { formatDate } from '@/utils/date';

const props = defineProps<{
    "albums": Album[]
}>()
const emit = defineEmits(["handle-sort"])

const handleSort = (field: keyof Album) =>  emit("handle-sort",field );
</script>
