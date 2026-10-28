<template>
  <div id="app">
    <Sidebar />
    <RouterView />
  </div>
</template>
<style scoped>
#app{
  display: flex;
}
</style>
<script setup lang="ts">
import { RouterView } from "vue-router";
import Sidebar from "./components/layout/Sidebar.vue";
</script>
