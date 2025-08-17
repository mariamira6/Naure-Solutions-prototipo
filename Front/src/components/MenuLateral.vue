<template>
  <nav class="d-flex flex-column vh-100 mt-4 bg-white p-4 gap-4" style="width: 250px;">
    <router-link v-for="opcion in opciones" :key="opcion.id" :to="opcion.url"
      class="mb-4 text-decoration-none text-dark">
      {{ opcion.label }}
    </router-link>
  </nav>
</template>

<script>
export default {
  name: 'MenuLateral',
  props: {
    opciones: {
      type: Array,
      required: true
    }
  }
}
</script>

<style></style>
