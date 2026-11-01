<template>
  <div>
    <label for="dupplicate">Duplicates</label>
    <input type="checkbox" @input="handleDupplicate" id="dupplicate" />
  </div>
</template>
<script setup lang="ts">
const emit = defineEmits<{
  'handle-dupplicate': [value: boolean];
}>();
const handleDupplicate = (e: Event) => {
  emit('handle-dupplicate', (e.target as HTMLInputElement).checked);
};
</script>
