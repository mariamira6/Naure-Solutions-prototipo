<template>
  <header class="hero-header">
    <nav class="navbar sticky-top navbar-expand-lg px-4 py-2 bg-transparent position-relative z-2 fw-bold">
      <div class="container-fluid d-flex justify-content-between align-items-center">
        <router-link to="/" class="navbar-brand">
          <img src="../assets/img/logo1.png" alt="Logo Naure Solutions" width="70" height="70" />
        </router-link>
        <ul class="navbar-nav flex-row gap-4 align-items-center mb-0">
          <li class="nav-item">
            <router-link to="/" class="nav-link text-white">Inicio</router-link>
          </li>
          <li class="nav-item dropdown">
            <span class="nav-link dropdown-toggle text-white" role="button" data-bs-toggle="dropdown"
              aria-expanded="false">
              Servicios
            </span>
            <ul class="dropdown-menu">
              <li v-for="servicio in servicios" :key="servicio.id"><router-link :to="`/servicios/${servicio.id}`"
                  class="dropdown-item">{{ servicio.nombre }}</router-link></li>
            </ul>
          </li>
          <li class="nav-item">
            <router-link to="/gestionservicios" class="nav-link text-white">Quiénes somos</router-link>
          </li>
          <li class="nav-item">
            <router-link to="/contacto" class="nav-link text-white">Contacto</router-link>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#" data-bs-toggle="modal" data-bs-target="#loginModal">
              <img src="../assets/icons/usuario.png" alt="Login" width="32" height="30" style="cursor: pointer;" />
            </a>
          </li>
        </ul>
      </div>
    </nav>
    <div class="hero-content">
      <h1 class="display-4 fw-bold">Encuentra la manera de conseguir el éxito</h1>
      <p class="text-white">En Naure Solutions te acompañamos de la mano para que consigas un proyecto exitoso y un
        equipo de
        alto rendimiento.</p>
      <router-link to="/servicioscontratados"><button
          class="btn diagnostic btn-info mt-3 text-light fw-bold">Diagnóstico gratuito</button></router-link>
    </div>
    <div class="hero-bg"></div>
    <div class="modal fade" id="loginModal" tabindex="-1" aria-labelledby="loginModalLabel" aria-hidden="true">
      <div class="modal-dialog">
        <div class="modal-content text-start">
          <div class="modal-header">
            <h5 class="modal-title" id="loginModalLabel">Iniciar sesión</h5>
            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Cerrar"></button>
          </div>
          <div class="modal-body">
            <form @submit="loginUsuario">
              <div class="mb-3">
                <label for="usuario" class="form-label">Usuario</label>
                <input type="text" class="form-control" id="usuario" required>
              </div>
              <div class="mb-3">
                <label for="password" class="form-label">Contraseña</label>
                <input type="password" class="form-control" id="password" required>
              </div>
              <div class="container-fluid d-flex align-items-center gap-3">
                <button type="submit" class="btn btn-primary">Acceder</button>
                <a href="#">¿Has olvidado la contraseña?</a>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </header>
</template>

<script>
export default {
  name: 'App',
  data() {
    return {
      servicios: [],
      error: '',
      errorLogin: ''
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
    async loginUsuario(event) {
      event.preventDefault();
      this.errorLogin = "";

      const usuario = document.getElementById("usuario").value;
      const password = document.getElementById("password").value;

      try {
        let res = await fetch("http://localhost:5259/api/Usuario/Login", {
          method: "POST",
          headers: {
            "Content-type": "application/json; charset=utf-8",
          },
          body: JSON.stringify({
            login: usuario,
            password: password,
          })
        });

        let json = await res.json();

        if (json.correcto === true) {
          const token = json.palabraClave;
          const idUsuario = json.idUsuario;
          localStorage.setItem("token", token);
          localStorage.setItem("idUsuario", idUsuario);

          if (json.esAdmin) {
            window.location.href = '/gestionservicios';
          } else {
            window.location.href = `/servicioscontratados/${idUsuario}`;
          }
        } else {
          alert(json.mensaje);
        }
      } catch (e) {
        this.errorLogin = "Error: " + e.message;
      }
    }
  },
  mounted() {
    this.listarServicios();
  }
}
</script>

<style>
.hero-header {
  height: 500px;
  position: relative;
  color: white;
  overflow: hidden;
}

.hero-bg {
  background-image: url('../assets/img/hero.jpg');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  width: 100%;
  height: 100%;
  position: absolute;
  top: 0;
  z-index: 1;
  filter: brightness(0.6);
}

.hero-content {
  position: absolute;
  top: 57%;
  left: 10%;
  transform: translateY(-50%);
  z-index: 2;
  max-width: 600px;
  color: white;
}

.dropdown-item:hover {
  color: #52c2fa;
  font-weight: bold;
}

.nav-link:hover {
  color: #52c2fa !important;
  transform: scale(1.2);
}

.diagnostic:hover {
  transform: scale(1.1);
}
</style>
