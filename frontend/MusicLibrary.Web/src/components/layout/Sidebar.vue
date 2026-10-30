<template>
  <nav id="navbar">
    <RouterLink to="/" class="link">Music library</RouterLink>
    <RouterLink to="/artists" class="link">Artists</RouterLink>
    <RouterLink to="/albums" class="link">Albums</RouterLink>
    <RouterLink to="/tracks" class="link">Tracks</RouterLink>
  </nav>
</template>
<style scoped>
#navbar {
  display: flex;
  flex-direction: column;
  gap: 10px;
  background-color: rgb(229, 229, 218);
}
.link {
  text-decoration: none;
  color: inherit;
}
</style>
<script setup lang="ts">
import { RouterLink } from 'vue-router';
</script>
