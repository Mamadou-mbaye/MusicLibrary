<template>
    <table>
        <thead>
            <tr>
                <th>Albums</th>
                <th>Tracks</th>
                <th>ID</th>
                <th>Navidrome ID</th>
                <th @click="handleSort('name')">Name</th>
                <th @click="handleSort('createdAt')">Created At</th>
                <th @click="handleSort('updatedAt')">Updated At</th>
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
import type { Artist } from '@/types/artist';
import { formatDate } from '@/utils/date';

const props = defineProps<{
    "artists": Artist[]
}>()
const emit = defineEmits(["handle-sort"])
const handleSort = (field: keyof Artist) =>  emit("handle-sort",field );
</script>
