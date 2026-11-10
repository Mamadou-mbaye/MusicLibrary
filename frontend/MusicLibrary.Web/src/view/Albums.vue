<template>
  <div class="container">
    <h1>Albums</h1>
    <div class="sync_records">
      <div>Albums count: {{ albums.length }}</div>
      <button @click="handleSync" class="button">Sync albums</button>
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
      <AlbumsTable :albums="albums" @handle-sort="handleSort" :showActionsCol="true" />
    </div>
  </div>
</template>
<style scoped>
#div-container {
  display: flex;
  gap: 1vw;
}

.filters {
  display: flex;
  gap: 1vw;
}
</style>
<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useRoute } from 'vue-router';
import type { Album } from '@/types/album';
import { useAlbumsStore } from '@/stores/albums';
import {
  filterSearch,
  filterDate,
  filterArtist,
  filterByIdentificationStatus,
  filterDuplicate,
} from '@/utils/filters';
import { createSortState, orderByField, toggleSort } from '@/utils/sort';
import SearchForm from '@/components/SearchForm.vue';
import DateForm from '@/components/DateForm.vue';
import CheckForm from '@/components/CheckForm.vue';
import DupCheckForm from '@/components/DupCheckForm.vue';
import AlbumsTable from '@/components/layout/AlbumsTable.vue';

const albumsStore = useAlbumsStore();

const search = ref<string>('');
const startDate = ref<string>('');
const endDate = ref<string>('');
const identified = ref<boolean>(false);
const unidentified = ref<boolean>(false);
const duplicate = ref<boolean>(false);
const sort = {
  state: ref(createSortState<Album>('name')),
  field: '' as keyof Album,
};
const albums = computed(() => {
  let records = albumsStore.albums;
  records = filterByIdentificationStatus(records, identified.value, unidentified.value) as Album[];
  records = filterArtist(records, artistId.value) as Album[];
  records = filterSearch(records, search.value) as Album[];
  records = filterDate(records, startDate.value, endDate.value) as Album[];
  records = filterDuplicate(records, duplicate.value) as Album[];
  records = orderByField(records, sort.field, sort.state.value.direction);
  return records;
});

const route = useRoute();
const artistId = computed(() => {
  const id = route.params.artistId;
  return typeof id === 'string' ? Number(id) : null;
});

const handleSearch = (searchInput: string) => (search.value = searchInput);
const handleStartDateChange = (dateInput: string) => (startDate.value = dateInput);
const handleEndDateChange = (dateInput: string) => (endDate.value = dateInput);
const handleIdentified = (checkInput: boolean) => (identified.value = checkInput);
const handleUnidentified = (checkInput: boolean) => (unidentified.value = checkInput);
const handleDupplicate = (checkInput: boolean) => (duplicate.value = checkInput);
const handleSync = async () => await albumsStore.AlbumsSync();
const handleSort = (field: keyof Album) => {
  sort.state.value = toggleSort(sort.state.value, field);
  sort.field = field;
};
onMounted(async () => {
  await albumsStore.AlbumsLoad();
});
</script>
