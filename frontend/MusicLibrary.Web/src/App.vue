<template>
  <button @click="changeForm(1)">Artists</button>
  <button @click="changeForm(2)">Albums</button>
  <button @click="changeForm(3)">Tracks</button>
  <Artists v-if="isArtists" />
  <Albums v-if="isAlbums" />
  <Tracks v-if="isTracks"/>
</template>

<style scoped></style>
<script setup lang="ts">
import { ref } from "vue"
import Artists from './view/Artists.vue';
import Albums from './view/Albums.vue';
import Tracks from "./view/Tracks.vue";

const isArtists = ref(true)
const isAlbums = ref(false)
const isTracks = ref(false)

const changeForm = (idx: number) => {
  isArtists.value = false
  isAlbums.value = false
  isTracks.value = false

  if (idx === 1)
    isArtists.value = true
  else if (idx === 2)
    isAlbums.value = true
  else if (idx === 3)
    isTracks.value = true
}
</script>
