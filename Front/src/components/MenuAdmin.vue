<template>
  <nav class="navbar sticky-top navbar-expand-lg px-4 py-2 bg-transparent position-relative z-2 fw-bold border-bottom">
    <div class="container-fluid d-flex justify-content-between align-items-center">
      <router-link to="/" class="navbar-brand">
        <img src="../assets/img/LogoNaure.png" alt="Logo Naure Solutions" width="70" height="70" />
      </router-link>
      <ul class="navbar-nav flex-row gap-4 align-items-center mb-0">
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
  name: 'MenuAdmin',
  methods: {
    cerrarSesion() {
      localStorage.removeItem("token");
      window.location.href = '/';
    }
  }
}
</script>

<style></style>
