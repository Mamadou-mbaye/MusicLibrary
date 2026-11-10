<template>
  <div>
    <label><input type="checkbox" @input="handleDupplicate" /> Duplicates</label>
  </div>
</template>
<script setup lang="ts">
const emit = defineEmits<{
  'handle-duplicate': [value: boolean];
}>();
const handleDupplicate = (e: Event) => {
  emit('handle-duplicate', (e.target as HTMLInputElement).checked);
};
</script>
