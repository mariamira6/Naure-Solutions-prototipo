<template>
  <div>
    <MenuCliente />
    <div class="container-fluid d-flex flex-row w-100 p-0 m-0">
      <MenuLateral :opciones="opciones" />
      <div class="bg-light w-100 p-5">
        <EstructuraDetalle :elemento="elemento" />
        <div class="citas container justify-content-center mt-5 my-5">
          <h4 class="mb-5 text-center">Calendario de citas</h4>
          <FullCalendar :options="calendarOptions" class="calendario" />
        </div>
        <div class="tramos container justify-content-center">
          <h4 class="mb-5 text-center">Desglose del servicio</h4>
          <table class="table table-light">
            <thead>
              <tr class="bg-info">
                <th scope="col">Tramo</th>
                <th scope="col">Fecha desde</th>
                <th scope="col">Fecha vencimiento</th>
                <th scope="col">Precio</th>
                <th scope="col">Pago</th>
              </tr>
            </thead>
            <tbody class="table-group-divider">
              <tr v-for="(tramo, i) in tramos" :key="i">
                <td scope="row">{{ tramo.nombre }}</td>
                <td>{{ formatearFecha(tramo.fechaDesde) }}</td>
                <td>{{ formatearFecha(tramo.fechaHasta) }}</td>
                <td>{{ tramo.precio }} €</td>
                <td><span v-if="tramo.pago === true">Pagado</span>
                  <a v-else href="#" target="_blank">Pagar ahora</a>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MenuCliente from '../components/MenuCliente.vue';
import MenuLateral from '../components/MenuLateral.vue';
import EstructuraDetalle from '../components/EstructuraDetalle.vue';
import FullCalendar from '@fullcalendar/vue3';
import dayGridPlugins from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';

export default {
  name: 'Detalle de servicio',
  components: { MenuCliente, MenuLateral, EstructuraDetalle, FullCalendar },

  data() {
    return {
      idContratacion: this.$route.params.idContratacion,
      idUsuario: localStorage.getItem("idUsuario"),
      opciones: [
        { id: 1, label: 'Servicios contratados', url: `/servicioscontratados/${this.idUsuario}` },
        { id: 2, label: 'Gestión económica', url: '' },
        { id: 3, label: 'Mis datos personales', url: '' },
        { id: 4, label: '', url: '' }
      ],
      elemento: {},
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
        events: []
      },
      tramos: []
    }
  },
  methods: {
    async obtenerServicioPorId() {
      const token = localStorage.getItem("token");
      this.elemento = {};

      try {
        let res = await fetch(`http://localhost:5259/api/Contratacion/ObtenerContratacionPorId?id=${this.idContratacion}`, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Token: token
          },
        });

        if (!res.ok) throw new Error('Error al obtener el detalle del servicio.');

        let datos = await res.json();

        this.elemento = {
          id: datos.id,
          imagen: datos.servicio.imagen,
          nombre: datos.servicio.nombre,
          fechaDesde: datos.fechaDesde,
          fechaHasta: datos.fechaHasta,
          descripcion: datos.servicio.descripcion
        };
      } catch (err) {
        console.error(err);
      }
    },
    async listarTramosContratacion() {
      this.tramos = [];
      const token = localStorage.getItem("token");
      try {
        let res = await fetch(`http://localhost:5259/api/Tramo/ListarTramosContratacion?idContratacion=${this.idContratacion}`, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Token: token
          },
        });

        if (!res.ok) throw new Error('Error al obtener la lista de contrataciones.');

        let datos = await res.json();
        this.tramos = datos.map(dato => ({
          id: dato.id,
          nombre: dato.descripcion,
          fechaDesde: dato.fechaDesde,
          fechaHasta: dato.fechaHasta,
          precio: dato.importe,
          pago: dato.pagado
        }));
        console.log(this.tramos);
      } catch (err) {
        console.error(err);
      }
    },
    async listarCitasContratacion() {
      this.calendarOptions.events = [];
      const token = localStorage.getItem("token");
      try {
        let res = await fetch(`http://localhost:5259/api/Cita/ListarCitasContratacion?idContratacion=${this.idContratacion}`, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Token: token
          },
        });

        if (!res.ok) throw new Error('Error al obtener la lista de contrataciones.');

        let datos = await res.json();

        this.calendarOptions.events = datos.map(dato => ({
          title: this.elemento.nombre,
          date: dato.fecha,
          color: this.obtenerColor(dato.contratacion.idServicio)
        }));
      } catch (err) {
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
    },
    formatearFecha(fechaISO) {
      let fecha = new Date(fechaISO);
      let dia = String(fecha.getDate()).padStart(2, '0');
      let mes = String(fecha.getMonth() + 1).padStart(2, '0');
      let anyo = String(fecha.getFullYear()).slice(0);
      return `${dia}/${mes}/${anyo}`;
    }
  },
  watch: {
    '$route.params.id'(nuevoId) {
      this.id = nuevoId;
      this.obtenerServicioPorId();
      this.listarTramosContratacion();
    }
  },
  async mounted() {
    await this.obtenerServicioPorId();
    await this.listarTramosContratacion();
    await this.listarCitasContratacion();
  }
}
</script>

<style>
.citas {
  width: 70%;
}

.calendario a {
  color: black;
  text-decoration: none;
}

.fc-toolbar-title {
  font-size: 18px !important;
  font-weight: bold;
}

.tramos {
  margin-top: 100px;
  width: 80%;
}
</style>
