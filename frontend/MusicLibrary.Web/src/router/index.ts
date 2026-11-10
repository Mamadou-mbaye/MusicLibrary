import { createRouter, createWebHistory } from 'vue-router';
import Artists from '@/view/Artists.vue';
import Albums from '@/view/Albums.vue';
import Tracks from '@/view/Tracks.vue';
import DataQuality from '@/view/DataQuality.vue';

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      redirect: '/artists',
    },
    {
      path: '/artists',
      name: 'artists',
      component: Artists,
    },
    {
      path: '/albums',
      name: 'albums',
      component: Albums,
    },
    {
      path: '/tracks',
      name: 'tracks',
      component: Tracks,
    },
    {
      path: '/artists/:artistId/albums',
      name: 'artistAlbums',
      component: Albums,
    },
    {
      path: '/artists/:artistId/tracks',
      name: 'artistTracks',
      component: Tracks,
    },
    {
      path: '/albums/:albumId/tracks',
      name: 'albumTracks',
      component: Tracks,
    },
    {
      path: '/data-quality',
      name: 'dataQuality',
      component: DataQuality,
    },
  ],
});
export default router;
