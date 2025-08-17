<template>
  <nav class="navbar sticky-top navbar-expand-lg px-4 py-2 bg-transparent position-relative z-2 fw-bold border-bottom">
    <div class="container-fluid d-flex justify-content-between align-items-center">
      <router-link to="/" class="navbar-brand">
        <img src="../assets/img/LogoNaure.png" alt="Logo Naure Solutions" width="70" height="70" />
      </router-link>
      <ul class="navbar-nav flex-row gap-4 align-items-center mb-0">
        <li class="nav-item">
          <router-link to="/" class="nav-link text-dark">Inicio</router-link>
        </li>
        <li class="nav-item dropdown">
          <span class="nav-link dropdown-toggle text-dark" role="button" data-bs-toggle="dropdown"
            aria-expanded="false">
            Servicios
          </span>
          <ul class="dropdown-menu">
            <li v-for="servicio in servicios" :key="servicio.id"><router-link :to="`/servicios/${servicio.id}`"
                class="dropdown-item">{{ servicio.nombre }}</router-link></li>
          </ul>
        </li>
        <li class="nav-item">
          <router-link to="#" class="nav-link text-dark">Quiénes somos</router-link>
        </li>
        <li class="nav-item">
          <router-link to="#" class="nav-link text-dark">Contacto</router-link>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="#" data-bs-toggle="modal" data-bs-target="#logoutModal">
            <img src="../assets/icons/user.png" alt="Logout" width="25" height="26" style="cursor: pointer;" />
          </a>
        </li>
      </ul>
    </div>
  </nav>
  <div class="modal fade" id="logoutModal" tabindex="-1" aria-labelledby="logoutModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content text-start">
        <div class="modal-header">
          <h5 class="modal-title" id="logoutModalLabel">Cerrar sesión</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
        </div>
        <div class="modal-body">
          ¿Estás seguro que quieres cerrar sesión?
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
          <button type="button" class="btn btn-danger" @click="cerrarSesion">Cerrar
            sesión</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MenuCliente',
  data() {
    return {
      servicios: [],
      error: ''
    };
  },
  methods: {
    async listarServicios() {
      this.servicios = [];
      this.error = "";

      try {
        let respuesta = await fetch(`http://localhost:5259/api/Servicio/ListarServicios`);

        if (respuesta.ok) {
          let datos = await respuesta.json();
          this.servicios = datos.map(dato => ({
            nombre: dato.nombre,
            id: dato.id
          }));
        } else {
          throw new Error("Ha habido un error.");
        }
      } catch (e) {
        this.error = 'Error. ' + e.message;
      }
    },
    cerrarSesion() {
      localStorage.removeItem("token");
      window.location.href = '/';
    }
  },
  mounted() {
    this.listarServicios();
  }
}
</script>

<style>
.dropdown-item:hover {
  color: #52c2fa;
  font-weight: bold;
}

.nav-link:hover {
  color: #52c2fa !important;
  transform: scale(1.2);
}
</style>
