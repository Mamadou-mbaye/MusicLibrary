<template>
  <div>
    <label for="known">Identified</label>
    <input type="checkbox" @input="handleIdentified" id="known" />

    <label for="unknow">Unidentified</label>
    <input type="checkbox" @input="handleUnidentified" id="unknow" />
  </div>
</template>
<script setup lang="ts">
const emit = defineEmits<{
  'handle-identified': [value: boolean];
  'handle-unidentified': [value: boolean];
}>();
const handleIdentified = (e: Event) => {
  emit('handle-identified', (e.target as HTMLInputElement).checked);
};

const handleUnidentified = (e: Event) => {
  emit('handle-unidentified', (e.target as HTMLInputElement).checked);
};
</script>
