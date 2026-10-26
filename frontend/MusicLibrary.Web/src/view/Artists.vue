<template>
    <h1>Artists component</h1>
    <div>
        <label for="filter-name">Artist name</label>
        <input type="text" id="filter-name" v-model="filterName" />
    </div>

    <table>
        <thead>
            <tr>
                <th> id </th>
                <th> navidromeId </th>
                <th> name </th>
                <th> createdAt </th>
                <th> updatedAt </th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="artist in artists" :key="artist.id">
                <td> {{ artist.id }}</td>
                <td> {{ artist.navidromeId }}</td>
                <td> {{ artist.name }}</td>
                <td> {{ artist.createdAt }}</td>
                <td> {{ artist.updatedAt }}</td>
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
import { ref, onMounted, computed } from "vue";
import apiClient from "@/api/client.ts" 

const artistsData = ref([]);
const baseUrl = ref("")
const filterName = ref("")

const getArtists = async () => {
    try {
        const { data } = await apiClient.get("/artists");
        artistsData.value = data.artists;
    } catch (error) {
        console.error(error);
    }
}
const artists = computed(() => {
    if (filterName.value.trim() === "") return artistsData.value;
    return artistsData.value
        .filter(artists => artists.name.toLowerCase()
            .includes(filterName.value.toLowerCase()));

})

onMounted(async () => {
    baseUrl.value = "http://localhost:5255/api/artists";
    await getArtists()
})

</script>