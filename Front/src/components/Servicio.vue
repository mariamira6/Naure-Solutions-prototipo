<template>
  <div class="card" v-for="(servicio, i) in servicios" :key="i" style="width: 20rem;">
    <router-link class="enlaceServicio text-decoration-none cursor-pointer"
      :to="`/servicioscontratados/${servicio.idContratacion}/detalle`">
      <img :src="servicio.imagen" class="card-img-top" alt="hola">
      <div class="card-body text-center">
        <p class="card-text">{{ servicio.nombre }}</p>
        <p class="card-text">{{ formatearFecha(servicio.fechaDesde) }} - {{ formatearFecha(servicio.fechaHasta) }}</p>
      </div>
    </router-link>
  </div>
</template>

<script>
export default {
  name: 'Servicio',
  props: {
    servicios: {
      type: Array,
      required: true
    }
  },
  methods: {
    formatearFecha(fechaISO) {
      let fecha = new Date(fechaISO);
      let dia = String(fecha.getDate()).padStart(2, '0');
      let mes = String(fecha.getMonth() + 1).padStart(2, '0');
      let anyo = String(fecha.getFullYear()).slice(0);
      return `${dia}/${mes}/${anyo}`;
    }
  }
}
</script>

<style></style>
