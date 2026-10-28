import { createRouter, createWebHistory } from "vue-router";
import Artists from "@/view/Artists.vue";
import Albums from "@/view/Albums.vue";
import Tracks from "@/view/Tracks.vue";

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: "/",
      name: "home",
      redirect: "/artists",
    },
    {
      path: "/artists",
      name: "artists",
      component: Artists,
    },
    {
      path: "/albums",
      name: "albums",
      component: Albums,
    },
    {
      path: "/tracks",
      name: "tracks",
      component: Tracks,
    },
  ],
});
export default router