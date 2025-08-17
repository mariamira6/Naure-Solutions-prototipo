<template>
  <div class="container-fluid w-100" v-if="dato">
    <div class="breadcrumb mt-4">
      <a href="/">Inicio</a> &gt; Servicios &gt; {{ dato.nombre }}
    </div>
    <div class="container-fluid d-flex flex-column justify-content-center align-items-center w-100 gap-5">
      <h1 class="mt-5">{{ dato.nombre }}</h1>
      <div>
        <img :src="dato.imagen" :alt="dato.nombre" width="750" height="500" />
      </div>
      <section class="descripcion w-75 text-justify">
        <p class="text-center">
          {{ dato.descripcion }}
        </p>
      </section>
      <router-link to="/"><button class="btn btn-info text-white fw-bold mb-5">Solicitar
          información</button></router-link>
    </div>
  </div>
  <div v-else>
    <p>Servicio no encontrado.</p>
  </div>
</template>

<script>
export default {
  name: 'Servicio general',
  props: {
    dato: {
      type: Object,
      required: false,
      default: null
    }
  }
};
</script>

<style>
.descripcion {
  text-align: justify;
}
</style>
