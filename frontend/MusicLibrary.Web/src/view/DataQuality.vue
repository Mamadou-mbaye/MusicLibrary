<template>
    <div>
        <!-- Metadati mancanti. -->
        <fieldset>
            <legend>Missing Metadata</legend>
        </fieldset>
        <!-- Possibili duplicati. -->
        <fieldset>
            <legend>Potential Duplicates</legend>
            <details>
                <summary>Duplicate Artists</summary>
                <fieldset>
                    <ArtistsTable :artists="duplicatedArtists" v-if="duplicatedArtists.length > 0" />
                    <div v-else>No duplicate artists found.</div>
                </fieldset>
            </details>
            <details>
                <summary>Duplicate Albums</summary>
                <fieldset>
                    <AlbumsTable :albums="duplicatedAlbums" v-if="duplicatedAlbums.length > 0" />
                    <div v-else>No duplicate albums found.</div>
                </fieldset>
            </details>
            <details>
                <summary>Duplicate Tracks</summary>
                <fieldset>
                    <TracksTable :tracks="duplicatedTracks" v-if="duplicatedTracks.length > 0" />
                    <div v-else>No duplicate tracks found.</div>
                </fieldset>
            </details>
        </fieldset>
        <fieldset>
            <legend>Unidentified</legend>
            <!-- Artisti sconosciuti. -->
            <details>
                <summary>Unidentified Artists</summary>
                <fieldset>
                    <ArtistsTable :artists="UnidentifiedArtists" v-if="UnidentifiedArtists.length > 0" />
                    <div v-else>All artists have a recognized name.</div>
                </fieldset>
            </details>
            <!-- Album sconosciuti. -->
            <details>
                <summary>Unidentified Albums</summary>
                <fieldset>
                    <AlbumsTable :albums="UnidentifiedAlbums" v-if="UnidentifiedAlbums.length > 0" />
                    <div v-else>All albums have a recognized name.</div>
                </fieldset>
            </details>
            <!-- Tracce senza titolo. -->
            <details>
                <summary>Unidentified Tracks</summary>
                <fieldset>
                    <TracksTable :tracks="UnidentifiedTracks" v-if="UnidentifiedTracks.length > 0" />
                    <div v-else>All tracks have a recognized title.</div>
                </fieldset>
            </details>
        </fieldset>
    </div>
</template>
<style scoped>
summary{
    cursor: pointer;
}
</style>
<script setup lang="ts">
import { onMounted, computed } from 'vue';

import type { Artist } from '@/types/artist';
import type { Album } from '@/types/album';
import type { Track } from '@/types/track';

import { useArtistsStore } from '@/stores/artists';
import { useAlbumsStore } from '@/stores/albums';
import { useTracksStore } from '@/stores/tracks';

import {
    filterUnidentified,
    filterDuplicate,
} from '@/utils/filters';

import ArtistsTable from '@/components/layout/ArtistsTable.vue';
import AlbumsTable from '@/components/layout/AlbumsTable.vue';
import TracksTable from '@/components/layout/TracksTable.vue';

const artistsStore = useArtistsStore();
const UnidentifiedArtists = computed(() => filterUnidentified(artistsStore.artists, false, true) as Artist[])
const duplicatedArtists = computed(() => filterDuplicate(artistsStore.artists, true) as Artist[])

const albumsStore = useAlbumsStore();
const UnidentifiedAlbums = computed(() => filterUnidentified(albumsStore.albums, false, true) as Album[]);
const duplicatedAlbums = computed(() => filterDuplicate(albumsStore.albums, true) as Album[])

const tracksStore = useTracksStore();
const UnidentifiedTracks = computed(() => filterUnidentified(tracksStore.tracks, false, true) as Track[]);
const duplicatedTracks = computed(() => filterDuplicate(tracksStore.tracks, true) as Track[])

onMounted(async () => {
    await artistsStore.ArtistsLoad();
    await albumsStore.AlbumsLoad();
    await tracksStore.TracksLoad();
})
</script>