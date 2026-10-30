<template>
  <div>
    <label for="date">Date</label>
    <input type="date" name="" id="date" @change="startDateChange" />
    <input type="date" name="" id="date" @change="endDateChange" />
  </div>
</template>
<script setup lang="ts">
const emit = defineEmits<{
  'start-date-change': [value: string];
  'end-date-change': [value: string];
}>();

const startDateChange = (event: Event) => {
  emit('start-date-change', (event.target as HTMLInputElement).value.trim());
};
const endDateChange = (event: Event) => {
  emit('end-date-change', (event.target as HTMLInputElement).value.trim());
};
</script>
