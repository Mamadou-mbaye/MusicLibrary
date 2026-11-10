<template>
  <nav class="sidebar">
    <RouterLink to="/" class="link">Music library</RouterLink>
    <RouterLink to="/artists" class="link">Artists</RouterLink>
    <RouterLink to="/albums" class="link">Albums</RouterLink>
    <RouterLink to="/tracks" class="link">Tracks</RouterLink>
    <RouterLink to="/data-quality" class="link">Data quality</RouterLink>
    <button class="btn-theme" @click="toggleTheme">
      {{ isDark ? 'Light mode' : 'Dark mode' }}
    </button>
  </nav>
</template>
<script setup lang="ts">
import { ref } from 'vue';
import { RouterLink } from 'vue-router';

// 1) scelta salvata, altrimenti quella del sistema operativo
const saved = localStorage.getItem('theme');
const prefersDark = window.matchMedia('(prefers-color-scheme: dark)').matches;
const isDark = ref(saved ? saved === 'dark' : prefersDark);

// 2) scrive il tema su <html> e lo salva
const applyTheme = () => {
  const theme = isDark.value ? 'dark' : 'light';
  document.documentElement.dataset.theme = theme;
  localStorage.setItem('theme', theme);
};

const toggleTheme = () => {
  isDark.value = !isDark.value;
  applyTheme();
};

applyTheme(); // applica il tema all'avvio
</script>
