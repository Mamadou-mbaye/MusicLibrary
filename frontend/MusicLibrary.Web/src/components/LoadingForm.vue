<template>
  <p v-if="props.isLoading" class="loading-state">Loading {{ props.recordName }}...</p>
  <p v-else class="loading-state">{{ props.emptyRecordMessage }}</p>
</template>
<script setup lang="ts">
const props = defineProps<{
  isLoading: boolean;
  recordName: string;
  emptyRecordMessage: string;
}>();
</script>
