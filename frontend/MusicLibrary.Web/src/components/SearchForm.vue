<template>
    <div>
        <label for="search">Artist name</label>
        <input type="text" @input="search" id="search" />
    </div>
</template>
<script setup lang="ts">
import { defineEmits } from 'vue';

const emit = defineEmits<{
    'search': [value: string]
}>()

const search = (e: Event) => {
    emit("search", (e.target as HTMLInputElement).value.toLowerCase())
}
</script>