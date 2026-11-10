<template>
  <div class="filter-group">
    <label for="search" class="text-lg">Search</label>
    <input type="text" @input="search" id="search" />
  </div>
</template>
<script setup lang="ts">
const emit = defineEmits<{
  search: [value: string];
}>();

const search = (e: Event) => {
  emit('search', (e.target as HTMLInputElement).value.toLowerCase());
};
</script>
