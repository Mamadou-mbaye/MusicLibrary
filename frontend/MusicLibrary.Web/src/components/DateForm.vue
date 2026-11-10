<template>
  <div class="filter-group">
    <label for="date-start">Date</label>
    <div class="filter-row">
      <input id=" date-start" type="date" aria-label="Start date" @change="startDateChange" />
      <input id="date-end" type="date" aria-label="End date" @change="endDateChange" />
    </div>
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
