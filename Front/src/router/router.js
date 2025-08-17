import { createRouter, createWebHistory } from 'vue-router';
import Inicio from '../views/Inicio.vue';
import PaginaServicios from '../views/PaginaServicios.vue';
import ServiciosContratados from '../views/ServiciosContratados.vue';
import GestionServicios from '../views/GestionServicios.vue';
import DetalleServicio from '../views/DetalleServicio.vue';
import AnalisisDatos from '../views/AnalisisDatos.vue';

const routes = [
  { path: '/', name: 'Inicio', component: Inicio },
  { path: '/servicios/:id', name: 'Servicios', component: PaginaServicios },
  { path: '/servicioscontratados/:idUsuario', name: 'Servicios Contratados', component: ServiciosContratados },
  { path: '/servicioscontratados/:idContratacion/detalle', name: 'Detalle Servicio', component: DetalleServicio },
  { path: '/gestionservicios', name: 'Gestión de Servicios', component: GestionServicios },
  { path: '/analisisdatos', name: 'Análisis de datos', component: AnalisisDatos }];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
