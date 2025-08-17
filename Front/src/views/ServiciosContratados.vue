<template>
  <div>
    <MenuCliente />
    <div class="container-fluid d-flex flex-row w-100 p-0 m-0">
      <MenuLateral :opciones="opciones" />
      <div class="bg-light w-100 p-5">
        <h3 class="mb-5 text-center">Servicios contratados</h3>
        <div class="container-fluid d-flex flex-row w-100 justify-content-center gap-5">
          <Servicio :servicios="servicios" />
        </div>
        <div class="citas container justify-content-center">
          <h4 class="mb-5 text-center">Calendario de citas</h4>
          <FullCalendar :options="calendarOptions" class="calendario" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MenuLateral from '../components/MenuLateral.vue';
import MenuCliente from '../components/MenuCliente.vue';
import Servicio from '../components/Servicio.vue';
import FullCalendar from '@fullcalendar/vue3';
import dayGridPlugins from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';

export default {
  components: { MenuCliente, MenuLateral, Servicio, FullCalendar },
  data() {
    return {
      id: this.$route.params.id,
      opciones: [
        { id: 1, label: 'Servicios contratados', url: '/servicioscontratados/' },
        { id: 2, label: 'Gestión económica', url: '' },
        { id: 3, label: 'Mis datos personales', url: '' },
        { id: 4, label: '', url: '' }
      ],
      datos: [],
      calendarOptions: {
        plugins: [dayGridPlugins, interactionPlugin],
        initialView: 'dayGridMonth',
        firstDay: "1",
        locale: "es",
        contentHeight: 500,
        dayHeaderFormat: {
          weekday: "long"
        },
        buttonText: {
          today: "Hoy"
        },
        weekends: false,
        fixedWeekCount: false,
        dayMaxEventRows: true,
        navLinks: false,
        views: {
          timeGrid: {
            dayMaxEventRows: 6
          }
        },
        events: [
        ]
      },
      servicios: [],
      error: '',
    }
  },
  methods: {
    async listarServiciosCliente() {
      this.servicios = [];
      this.error = "";
      const token = localStorage.getItem("token");
      const idUsuario = localStorage.getItem("idUsuario");

      try {
        let respuesta = await fetch(`http://localhost:5259/api/Contratacion/ListarContratacionesCliente?idUsuario=${idUsuario}`, {
          method: "GET",
          headers: {
            Token: token,
          },
        });

        if (respuesta.ok) {
          let datos = await respuesta.json();
          this.servicios = datos.map(dato => ({
            idContratacion: dato.id,
            nombre: dato.servicio.nombre,
            idServicio: dato.servicio.id,
            imagen: dato.servicio.imagen,
            fechaHasta: dato.fechaHasta,
            fechaDesde: dato.fechaDesde
          }));
        } else {
          throw new Error("Ha habido un error.");
        }
      } catch (e) {
        this.error = 'Error. ' + e.message;
      }
    },
    async listarCitasContratacion() {
      this.calendarOptions.events = [];
      const token = localStorage.getItem("token");
      const idUsuario = localStorage.getItem("idUsuario");

      try {
        let res = await fetch(`http://localhost:5259/api/Cita/ListarCitas`, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Token: token
          },
        });

        if (!res.ok) {
          throw new Error('Error al obtener la lista de citas.');
        }

        let datos = await res.json();
        let datosCliente = datos.filter(dato => dato.contratacion.idCliente == idUsuario);

        this.calendarOptions.events = datosCliente.map(dato => ({
          title: this.servicios.find(servicio => servicio.idServicio == dato.contratacion.idServicio).nombre,
          date: dato.fecha,
          color: this.obtenerColor(dato.contratacion.idServicio)
        }));

      }
      catch (err) {
        console.error(err);
      }
    },
    obtenerColor(idServicio) {
      switch (idServicio % 10) {
        case 0:
          return "#1f77b4";
        case 1:
          return "#ff7f0e";
        case 2:
          return "#2ca02c";
        case 3:
          return "#cf5d55";
        case 4:
          return "#9467bd";
        case 5:
          return "#8c564b";
        case 6:
          return "#e377c2";
        case 7:
          return "#d1a269";
        case 8:
          return "#53c2db";
        case 9:
          return "#a64c72"
        default:
          return "#7f7f7f";
      }
    }
  },
  async mounted() {
    await this.listarServiciosCliente();
    await this.listarCitasContratacion();
  }
}
</script>

<style>
.card:hover {
  transform: scale(1.1);
}

.enlaceServicio {
  color: black;
}

.citas {
  width: 70%;
  margin-top: 100px;
}

.calendario a {
  color: black;
  text-decoration: none;
}

.fc-toolbar-title {
  font-size: 18px !important;
  font-weight: bold;
}
</style>
