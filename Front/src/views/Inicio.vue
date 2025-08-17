<template>
  <Header />
  <div class="container-fluid py-5 gx-4">
    <section class="text-center mb-5 mx-5">
      <h2 class="mb-5 text-shadow">Servicios</h2>
      <div class="row row-cols-1 row-cols-md-3 g-4 my-5">
        <div class="col" v-for="(servicio, i) in servicios" :key="i">
          <div class="p-3 h-100">
            <div class="mb-3">
              <img :src="servicio.icono" :alt="servicio.nombre" width="25" height="25" />
            </div>
            <h5 class="text-info fw-bold">{{ servicio.titulo }}</h5>
            <p>{{ servicio.descripcion }}</p>
            <router-link :to="servicio.url"><button
                class="btn btn-info btn-md w-25 text-white fw-bold mb-5">Ir</button></router-link>
          </div>
        </div>
      </div>
    </section>
    <section class="row align-items-center mb-5 w-100">
      <div class="col-md-6 mb-3 mb-md-0 p-5">
        <div id="carouselIndicators" class="carousel slide" data-bs-ride="carousel">
          <div class="carousel-indicators">
            <button type="button" data-bs-target="#carouselIndicators" data-bs-slide-to="0" class="active"
              aria-current="true" aria-label="Reunión oficina"></button>
            <button type="button" data-bs-target="#carouselIndicators" data-bs-slide-to="1"
              aria-label="Sesión formativa"></button>
            <button type="button" data-bs-target="#carouselIndicators" data-bs-slide-to="2"
              aria-label="Trabajo en equipo"></button>
          </div>
          <div class="carousel-inner">
            <div class="carousel-item active">
              <img src="../assets/img/formacion1.jpg" class="d-block w-100" alt="Reunión oficina">
            </div>
            <div class="carousel-item">
              <img src="../assets/img/15112.jpg" class="d-block w-100" alt="Sesión formativa">
            </div>
            <div class="carousel-item">
              <img src="../assets/img/2149352652.jpg" class="d-block w-100" alt="Engranajes (trabajo en equipo)">
            </div>
          </div>
          <button class="carousel-control-prev" type="button" data-bs-target="#carouselIndicators" data-bs-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Anterior</span>
          </button>
          <button class="carousel-control-next" type="button" data-bs-target="#carouselIndicators" data-bs-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="visually-hidden">Siguiente</span>
          </button>
        </div>
      </div>
      <div class="col-md-6 text-center p-5">
        <h4 class="mb-4">Impulsa tu equipo con metodologías ágiles</h4>
        <p>
          ¿Te imaginas un equipo que colabora mejor, se adapta rápido a los cambios y entrega resultados con más
          eficiencia?
          Nuestra formación en metodologías ágiles no es solo teoría: es una experiencia práctica y transformadora.
          Adaptamos cada sesión a las dinámicas de tu organización para ayudarte a construir un entorno más ágil,
          colaborativo y orientado a la mejora continua. ¡Es hora de dar ese salto que tu equipo necesita!
        </p>
        <p>
          No importa si estás dando tus primeros pasos o si ya tienes experiencia: nuestras formaciones se ajustan a tu
          punto de partida.
          Trabajamos con dinámicas participativas, casos reales y herramientas actuales para que cada integrante del
          equipo descubra
          cómo aplicar la agilidad en su día a día. Porque aprender haciendo es la clave para que el cambio no solo se
          entienda, sino que se viva.
        </p>
      </div>
    </section>
    <section class="row text-center bg-section p-5">
      <div class="col-md-4" v-for="(paso, i) in pasos" :key="i">
        <div class="p-3">
          <img :src="paso.icono" :alt="paso.nombre" width="28" height="28" />
          <h5 class="mt-3">{{ paso.titulo }}</h5>
          <p>{{ paso.descripcion }}</p>
        </div>
      </div>
    </section>
    <section class="d-flex justify-content-center align-items-center my-5 py-3">
      <div class="container w-25">
        <h2 class="mb-4 text-center">Contacta con nosotros</h2>
        <form>
          <div class="mb-3">
            <label for="name" class="form-label">Nombre</label>
            <input type="text" class="form-control" id="name" v-model="name" required>
          </div>
          <div class="mb-3">
            <label for="apellidos" class="form-label">Apellidos</label>
            <input type="text" class="form-control" id="apellidos" v-model="apellidos" required>
          </div>
          <div class="mb-3">
            <label for="email" class="form-label">Correo electrónico</label>
            <input type="email" class="form-control" id="email" v-model="email" required>
          </div>
          <div class="mb-3">
            <label for="tlf" class="form-label">Teléfono de contacto</label>
            <input type="tel" class="form-control" id="tlf" v-model="tlf" required>
          </div>
          <div class="mb-3">
            <label for="consulta" class="form-label">Consulta</label>
            <textarea class="form-control" id="consulta" rows="4" v-model="consulta" required></textarea>
          </div>
          <div class="d-flex justify-content-center">
            <button type="submit" class="btn btn-primary">Enviar</button>
          </div>
        </form>
      </div>
    </section>
  </div>
</template>

<script>
import Header from "../components/Header.vue";

export default {
  name: "Inicio",
  components: { Header },
  data() {
    return {
      servicios: [
        {
          titulo: "Asesoramiento",
          descripcion:
            "Ofrecemos asesoramiento especializado en gestión de proyectos con metodologías ágiles, ayudando a identificar las estrategias, herramientas y prácticas más adecuadas para cada organización. Guiamos en la toma de decisiones para optimizar procesos y mejorar la eficiencia del trabajo en equipo.",
          icono: "/public/icons/bombilla.png",
          nombre: "Icono bombilla",
          url: "/servicios/1"
        },
        {
          titulo: "Acompañamiento",
          descripcion:
            "Acompañamos a los equipos durante la implementación de metodologías ágiles, brindando apoyo continuo, formación práctica y seguimiento adaptado a sus necesidades. De esta manera, facilitamos la adopción de nuevas dinámicas de trabajo, promoviendo la mejora continua y la colaboración.",
          icono: "/public/icons/cohete.png",
          nombre: "Icono cohete",
          url: "/servicios/2"
        },
        {
          titulo: "Configuración herramientas",
          descripcion:
            "Nos encargamos de la configuración e implementación de herramientas ágiles como Jira, Trello, Confluence, Mentimeter, Monday.com, entre otras. Adaptamos cada herramientas al flujo de trabajo del equipo, asegurando su uso eficiente y alineado con los objetivos del proyecto.",
          icono: "/public/icons/herramientas.png",
          nombre: "Icono herramientas",
          url: "/servicios/3"
        }
      ],
      pasos: [
        {
          titulo: "Pide tu cita",
          descripcion:
            "Pide tu cita a través de nuestro formulario de contacto o de cualquiera de los canales de comunicación que tenemos disponibles.",
          icono: "/public/icons/calendario.png",
          nombre: "Icono calendario"
        },
        {
          titulo: "Análisis del negocio",
          descripcion:
            "Realizaremos un análisis de las necesidades del proyecto y del equipo para averiguar qué metodología de gestión de proyectos es más adecuada.",
          icono: "/public/icons/analizar.png",
          nombre: "Icono análisis"
        },
        {
          titulo: "Hoja de ruta",
          descripcion:
            "Diseñaremos una hoja de ruta basada en los objetivos propuestos, definiendo los pasos clave a seguir para alcanzar la meta.",
          icono: "/public/icons/mapa.png",
          nombre: "Icono hoja de ruta"
        }
      ]
    }
  }
}
</script>

<style>
.bg-section {
  background-color: #d0edf7;
}
</style>
