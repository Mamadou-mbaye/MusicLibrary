<template>
  <div>
    <label for="dupplicate">Dupplicate</label>
    <input type="checkbox" @input="handleDupplicate" id="dupplicate" />
  </div>
</template>
<script setup lang="ts">
import { defineEmits } from 'vue';

const emit = defineEmits<{
  'handle-dupplicate': [value: boolean];
}>();
const handleDupplicate = (e: Event) => {
  emit('handle-dupplicate', (e.target as HTMLInputElement).checked);
};
</script>
