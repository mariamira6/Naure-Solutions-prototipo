<template>
  <div>
    <Header />
    <ServicioGeneral :dato="servicioSeleccionado" />
  </div>
</template>

<script>
import Header from '../components/Header.vue';
import ServicioGeneral from '../components/ServicioGeneral.vue';

export default {
  name: 'Servicio general',
  components: { Header, ServicioGeneral },
  data() {
    return {
      id: this.$route.params.id,
      datos: [],
      servicioSeleccionado: 0
    }
  },
  methods: {
    async listarServicios() {
      this.datos = [];
      this.error = "";

      try {
        let respuesta = await fetch(`http://localhost:5259/api/Servicio/ListarServicios`);

        if (respuesta.ok) {
          let data = await respuesta.json();
          this.datos = data.map(dato => ({
            nombre: dato.nombre,
            id: dato.id,
            descripcion: dato.descripcion,
            imagen: dato.imagen,
          }));

          this.servicioSeleccionado = this.datos.find(dato => dato.id === Number(this.id)) || null;
        } else {
          throw new Error("Ha habido un error.");
        }
      } catch (e) {
        this.error = 'Error. ' + e.message;
      }
    }
  },
  watch: {
    '$route.params.id'(nuevoId) {
      this.id = nuevoId;
      this.listarServicios();
    }
  },
  mounted() {
    this.listarServicios();
  }
}
</script>

<style></style>
