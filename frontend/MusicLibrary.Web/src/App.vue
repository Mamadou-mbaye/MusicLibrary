<template>
  <div id="app" class="app">
    <Sidebar />
    <RouterView />
  </div>
</template>
<style scoped>
.app {
  display: flex;
  height: 100svh;
}
</style>
<script setup lang="ts">
import { RouterView } from 'vue-router';
import Sidebar from './components/layout/Sidebar.vue';
</script>
