using NaureBack.DTOs;
using NaureBack.Models;
using NaureBack.Services;

namespace NaureTest
{
    public class AnalisisServicioTest
    {
        /// <summary>
        /// Genera servicios de ejemplo
        /// </summary>
        /// <returns>Lista de servicios de ejemplo</returns>
        private IList<ServicioDTO> ObtenerServiciosEjemplo()
        {
            IList<ServicioDTO> listaServicio = new List<ServicioDTO>();

            ServicioDTO servicio1 = new ServicioDTO
            {
                Id = 1,
                Descripcion = "Descripción del Servicio 1",
                Nombre = "Servicio 1"
            };

            ServicioDTO servicio2 = new ServicioDTO
            {
                Id = 2,
                Descripcion = "Descripción del Servicio 2",
                Nombre = "Servicio 2"
            };

            ServicioDTO servicio3 = new ServicioDTO
            {
                Id = 3,
                Descripcion = "Descripción del Servicio 3",
                Nombre = "Servicio 3"
            };

            listaServicio.Add(servicio1);
            listaServicio.Add(servicio2);
            listaServicio.Add(servicio3);

            return listaServicio;
        }

        /// <summary>
        /// Genera tramos de ejemplo
        /// </summary>
        /// <returns>Lista de tramos de ejemplo</returns>
        private IList<TramoDTO> ObtenerTramosEjemplo()
        {
            IList<TramoDTO> listaTramos = new List<TramoDTO>();

            ContratacionDTO contrato1 = new ContratacionDTO()
            {
                Id = 1,
                IdServicio = 1
            };

            ContratacionDTO contrato2 = new ContratacionDTO()
            {
                Id = 2,
                IdServicio = 1
            };

            ContratacionDTO contrato3 = new ContratacionDTO()
            {
                Id = 3,
                IdServicio = 2
            };

            ContratacionDTO contrato4 = new ContratacionDTO()
            {
                Id = 4,
                IdServicio = 3
            };

            TramoDTO tramo1 = new TramoDTO
            {
                Id = 1,
                Descripcion = "Tramo 1",
                FechaDesde = new DateOnly(2022, 01, 01),
                FechaHasta = new DateOnly(2022, 01, 31),
                Importe = 100,
                Pagado = true,
                IdContratacion = 1,
                Contratacion = contrato1
            };

            TramoDTO tramo2 = new TramoDTO
            {
                Id = 2,
                Descripcion = "Tramo 2",
                FechaDesde = new DateOnly(2022, 02, 01),
                FechaHasta = new DateOnly(2022, 02, 28),
                Importe = 60,
                Pagado = true,
                IdContratacion = 1,
                Contratacion = contrato1
            };

            TramoDTO tramo3 = new TramoDTO
            {
                Id = 3,
                Descripcion = "Tramo 3",
                FechaDesde = new DateOnly(2022, 03, 01),
                FechaHasta = new DateOnly(2022, 03, 31),
                Importe = 800,
                Pagado = false,
                IdContratacion = 2,
                Contratacion = contrato2
            };

            TramoDTO tramo4 = new TramoDTO
            {
                Id = 4,
                Descripcion = "Tramo 4",
                FechaDesde = new DateOnly(2023, 04, 01),
                FechaHasta = new DateOnly(2023, 04, 30),
                Importe = 40,
                Pagado = false,
                IdContratacion = 2,
                Contratacion = contrato2
            };

            TramoDTO tramo5 = new TramoDTO
            {
                Id = 5,
                Descripcion = "Tramo 5",
                FechaDesde = new DateOnly(2023, 05, 01),
                FechaHasta = new DateOnly(2023, 05, 31),
                Importe = 140,
                Pagado = true,
                IdContratacion = 3,
                Contratacion = contrato3
            };

            TramoDTO tramo6 = new TramoDTO
            {
                Id = 1,
                Descripcion = "Tramo 1",
                FechaDesde = new DateOnly(2023, 06, 01),
                FechaHasta = new DateOnly(2023, 06, 30),
                Importe = 48,
                Pagado = false,
                IdContratacion = 4,
                Contratacion = contrato4
            };

            listaTramos.Add(tramo1);
            listaTramos.Add(tramo2);
            listaTramos.Add(tramo3);
            listaTramos.Add(tramo4);
            listaTramos.Add(tramo5);
            listaTramos.Add(tramo6);

            return listaTramos;
        }

        [Fact]
        public void ObtenerVentasPorAnyo()
        {
            IList<TramoDTO> listaTramos = ObtenerTramosEjemplo();
            AnalisisServicio analisisServicio = new AnalisisServicio();
            IList<AnalisisDTO> listaAnalisis = analisisServicio.ObtenerVentasPorAnyo(listaTramos);

            Assert.NotEmpty(listaAnalisis);
            Assert.Equal(2, listaAnalisis.Count);
            Assert.Equal(2022, listaAnalisis[0].Anyo);
            Assert.Equal(2023, listaAnalisis[1].Anyo);
            Assert.Equal(960, listaAnalisis[0].Importe);
            Assert.Equal(160, listaAnalisis[0].ImporteCobrado);
            Assert.Equal(800, listaAnalisis[0].ImportePendiente);
            Assert.Equal(228, listaAnalisis[1].Importe);
            Assert.Equal(140, listaAnalisis[1].ImporteCobrado);
            Assert.Equal(88, listaAnalisis[1].ImportePendiente);
        }

        [Fact]
        public void ObtenerVentasPorServicio()
        {
            IList<ServicioDTO> listaServicios = ObtenerServiciosEjemplo();
            IList<TramoDTO> listaTramos = ObtenerTramosEjemplo();
            AnalisisServicio analisisServicio = new AnalisisServicio();
            IList<AnalisisDTO> listaAnalisis = analisisServicio.ObtenerVentasPorServicio(listaTramos, listaServicios);

            Assert.NotEmpty(listaAnalisis);
            Assert.Equal(3, listaAnalisis.Count);
            Assert.Equal(1, listaAnalisis[0].ServicioId);
            Assert.Equal(2, listaAnalisis[1].ServicioId);
            Assert.Equal(3, listaAnalisis[2].ServicioId);
            Assert.Equal(1000, listaAnalisis[0].Importe);
            Assert.Equal(160, listaAnalisis[0].ImporteCobrado);
            Assert.Equal(840, listaAnalisis[0].ImportePendiente);
            Assert.Equal(140, listaAnalisis[1].Importe);
            Assert.Equal(140, listaAnalisis[1].ImporteCobrado);
            Assert.Equal(0, listaAnalisis[1].ImportePendiente);
            Assert.Equal(48, listaAnalisis[2].Importe);
            Assert.Equal(0, listaAnalisis[2].ImporteCobrado);
            Assert.Equal(48, listaAnalisis[2].ImportePendiente);
        }
    }
}