<template>
  <div class="d-flex align-items-start gap-5 justify-content-center mx-5 p-3">
    <img :src="elemento.imagen" alt="" width="45%" height="auto" class="mb-5" />
    <div>
      <h3 class="my-3">Detalle del servicio</h3>
      <p>Servicio: {{ elemento.nombre }}</p>
      <p>Duración: {{ formatearFecha(elemento.fechaDesde) }} - {{ formatearFecha(elemento.fechaHasta) }}</p>
      <p class="justify">{{ elemento.descripcion }}</p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Estructura detalle',
  props: {
    elemento: {
      type: Object,
      required: true
    }
  },
  methods: {
    formatearFecha(fechaISO) {
      let fecha = new Date(fechaISO);
      let dia = String(fecha.getDate()).padStart(2, '0');
      let mes = String(fecha.getMonth() + 1).padStart(2, '0');
      let anyo = String(fecha.getFullYear()).slice(0);;
      return `${dia}/${mes}/${anyo}`;
    }
  }
}
</script>

<style>
.justify {
  text-align: justify;
}
</style>
