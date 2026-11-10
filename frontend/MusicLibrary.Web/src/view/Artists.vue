<template>
  <div class="container">
    <h1>Artists</h1>
    <div class="sync_records">
      <div>Artists count: {{ artists.length }}</div>
      <button @click="handleSync" class="button">Sync artists</button>
    </div>
    <div class="filters">
      <SearchForm @search="handleSearch" />
      <DateForm @start-date-change="handleStartDateChange" @end-date-change="handleEndDateChange" />
      <div class="filter-checks">
        <CheckForm
          @handle-identified="handleIdentified"
          @handle-unidentified="handleUnidentified"
        />
        <DupCheckForm @handle-duplicate="handleDupplicate" />
      </div>
    </div>
    <div class="table-wrap">
      <ArtistsTable :artists="artists" @handle-sort="handleSort" :showActionsCol="true" />
    </div>
  </div>
</template>
<style scoped></style>
<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import type { Artist } from '@/types/artist';
import { useArtistsStore } from '@/stores/artists';
import {
  filterSearch,
  filterDate,
  filterByIdentificationStatus,
  filterDuplicate,
} from '@/utils/filters';
import { createSortState, orderByField, toggleSort } from '@/utils/sort';
import SearchForm from '@/components/SearchForm.vue';
import DateForm from '@/components/DateForm.vue';
import CheckForm from '@/components/CheckForm.vue';
import DupCheckForm from '@/components/DupCheckForm.vue';
import ArtistsTable from '@/components/layout/ArtistsTable.vue';

const artistsStore = useArtistsStore();

const search = ref<string>('');
const startDate = ref<string>('');
const endDate = ref<string>('');
const identified = ref<boolean>(false);
const unidentified = ref<boolean>(false);
const duplicate = ref<boolean>(false);
const sort = {
  state: ref(createSortState<Artist>('name')),
  field: '' as keyof Artist,
};
const artists = computed(() => {
  let records = artistsStore.artists;
  records = filterByIdentificationStatus(records, identified.value, unidentified.value) as Artist[];
  records = filterSearch(records, search.value) as Artist[];
  records = filterDate(records, startDate.value, endDate.value) as Artist[];
  records = filterDuplicate(records, duplicate.value) as Artist[];
  records = orderByField(records, sort.field, sort.state.value.direction);
  return records;
});

const handleSearch = (searchInput: string) => (search.value = searchInput.trim());
const handleStartDateChange = (dateInput: string) => (startDate.value = dateInput.trim());
const handleEndDateChange = (dateInput: string) => (endDate.value = dateInput.trim());
const handleIdentified = (checkInput: boolean) => (identified.value = checkInput);
const handleUnidentified = (checkInput: boolean) => (unidentified.value = checkInput);
const handleDupplicate = (checkInput: boolean) => (duplicate.value = checkInput);
const handleSync = async () => await artistsStore.ArtistsSync();
const handleSort = (field: keyof Artist) => {
  sort.state.value = toggleSort(sort.state.value, field);
  sort.field = field;
};
onMounted(async () => {
  await artistsStore.ArtistsLoad();
});
</script>
