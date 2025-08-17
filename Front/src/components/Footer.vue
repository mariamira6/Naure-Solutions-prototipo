<template>
  <div class="container-fluid bg-light pt-5 border-top">
    <footer class="container py-5">
      <div class="row row-cols-1 row-cols-md-4 g-4 text-start">
        <div class="col d-flex justify-content-center justify-content-md-start">
          <a href="/" class="d-block mb-3">
            <img src="../assets/img/LogoNauresf.png" alt="Naure Logo" width="150" />
          </a>
        </div>
        <div class="col">
          <h5>Acerca de</h5>
          <ul class="list-unstyled">
            <li><a href="#" class="text-decoration-none text-dark">Política de privacidad</a></li>
            <li><a href="#" class="text-decoration-none text-dark">Preguntas frecuentes</a></li>
          </ul>
        </div>
        <div class="col">
          <h5 class="mb-3">Contacto</h5>
          <ul class="list-unstyled">
            <li v-for="(elemento, i) in elementos" :key="i">
              <div class="d-flex align-items-center gap-3 mb-1">
                <img :src="elemento.ruta" :alt="elemento.nombre" width="25" height="25" />
                <a href="#" class="text-decoration-none text-dark">{{ elemento.texto }}</a>
              </div>
            </li>
          </ul>
        </div>
        <div class="col">
          <iframe
            src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d3128.9091227321674!2d-0.5095061245756601!3d38.351085378754675!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0xd6236675e91c5a7%3A0x3fd9b1236d0b2db7!2sC.%20Hermano%20Felic%C3%ADsimo%20Ruiz%2C%2003005%20Alicante%20(Alacant)%2C%20Alicante!5e0!3m2!1ses!2ses!4v1748473872775!5m2!1ses!2ses"
            width="100%" height="200" style="border:0;" allowfullscreen="" loading="lazy"
            referrerpolicy="no-referrer-when-downgrade"></iframe>
        </div>
      </div>
      <div class="row mt-4">
        <div class="col text-center">
          <p class="text-muted mb-0">Naure Solutions &copy; 2025</p>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
export default {
  name: "Footer",
  data() {
    return {
      elementos: [
        {
          ruta: "/public/icons/linkedin.png",
          nombre: "Icono de LinkedIn",
          texto: "Naure Solutions"
        },
        {
          ruta: "/public/icons/gmail.png",
          nombre: "Icono de Gmail",
          texto: "naure@solutions.com"
        },
        {
          ruta: "/public/icons/whatsapp.png",
          nombre: "Icono de Whatsapp",
          texto: "+34 769 543 255"
        },
        {
          ruta: "/public/icons/instagram.png",
          nombre: "Icono de Instagram",
          texto: "Naure Solutions"
        }
      ]
    };
  }
}
</script>


<style>
a:hover {
  color: #52c2fa !important;
  transition: color 0.2s ease-in;
}
</style>
