<template>
  <table class="grid">
    <thead class="grid-header">
      <tr>
        <th v-if="showActionsCol"></th>
        <th>ID</th>
        <th>Navidrome ID</th>
        <th @click="handleSort('name')">Name</th>
        <th @click="handleSort('createdAt')">Created At</th>
        <th @click="handleSort('updatedAt')">Updated At</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="artist in artists" :key="artist.id">
        <td class="actions" v-if="showActionsCol">
          <a :href="`/artists/${artist.id}/albums`">albums</a>
          <a :href="`/artists/${artist.id}/tracks`">tracks</a>
        </td>
        <td>{{ artist.id }}</td>
        <td>{{ artist.navidromeId }}</td>
        <td>{{ artist.name }}</td>
        <td>{{ formatDate(artist.createdAt) }}</td>
        <td>{{ formatDate(artist.updatedAt) }}</td>
      </tr>
    </tbody>
  </table>
</template>
<style scoped></style>
<script setup lang="ts">
import type { Artist } from '@/types/artist';
import { formatDate } from '@/utils/date';

const props = defineProps<{
  artists: Artist[];
  showActionsCol: boolean;
}>();
const emit = defineEmits(['handle-sort']);
const handleSort = (field: keyof Artist) => emit('handle-sort', field);
</script>
