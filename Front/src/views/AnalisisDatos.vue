<template>
  <div>
    <MenuAdmin />
    <div class="container-fluid d-flex flex-row w-100 p-0 m-0">
      <MenuLateral :opciones="opciones" />
      <div class="bg-light w-100 p-5">
        <h4 class="mb-5 text-center">Análisis de datos</h4>
        <ul class="nav nav-tabs" id="myTab" role="tablist">
          <li class="nav-item" role="presentation">
            <button class="nav-link active" id="graficos-tab" data-bs-toggle="tab" data-bs-target="#graficos"
              type="button" role="tab" aria-controls="graficos" aria-selected="true">Gráficos</button>
          </li>
          <li class="nav-item" role="presentation">
            <button class="nav-link" id="mapas-tab" data-bs-toggle="tab" data-bs-target="#mapas" type="button"
              role="tab" aria-controls="mapas" aria-selected="false">Mapas</button>
          </li>
        </ul>
        <div class="tab-content p-5 w-100" id="myTabContent">
          <div class="tab-pane fade show active" id="graficos" role="tabpanel" aria-labelledby="graficos-tab">
            <div class="d-flex gap-4 mb-4">
              <div style="flex:1; min-width: 300px;">
                <GraficoBarras v-if="chartData && chartData.labels.length" :chartData="chartData"
                  :chartOptions="chartOptions" />
              </div>
              <div class="mb-5" style="flex:1; min-width: 300px;">
                <GraficoBarras v-if="chartDataAgrupadas && chartDataAgrupadas.labels.length"
                  :chartData="chartDataAgrupadas" :chartOptions="chartOptionsAgrupadas" />
              </div>
            </div>
            <div style="min-width: 300px; max-width: 700px; margin: 0 auto;">
              <GraficoSectores v-if="pieChartData && pieChartData.labels.length" :chartData="pieChartData"
                :chartOptions="pieChartOptions" />
            </div>
          </div>
          <div class="tab-pane fade" id="mapas" role="tabpanel" aria-labelledby="mapas-tab">
            <div id="map" style="width: 1500px; height: 800px;"></div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MenuLateral from '../components/MenuLateral.vue';
import MenuAdmin from '../components/MenuAdmin.vue';
import GraficoBarras from '../components/GraficoBarras.vue';
import GraficoSectores from '../components/GraficoSectores.vue';

export default {
  components: { MenuAdmin, MenuLateral, GraficoBarras, GraficoSectores },
  data() {
    return {
      opciones: [
        { id: 1, label: 'Gestión de servicios', url: '/GestionServicios' },
        { id: 2, label: 'Gestión de clientes', url: '' },
        { id: 3, label: 'Contrataciones', url: '' },
        { id: 4, label: 'Análisis de datos', url: '/AnalisisDatos' }
      ],
      chartData: {
        labels: [],
        datasets: []
      },
      mapaMostrado: false,
      chartOptions: {
        responsive: true,
        plugins: {
          legend: { display: false },
          title: {
            display: true,
            text: "Ventas por año",
            font: {
              size: 16
            }
          }
        }
      },
      chartDataAgrupadas: {
        labels: [],
        datasets: []
      },
      chartOptionsAgrupadas: {},
      pieChartData: {
        labels: [],
        datasets: []
      },
      pieChartOptions: {
        responsive: true,
        plugins: {
          legend: {
            position: 'right', labels: {
              font: {
                size: 16
              }
            }
          },
          title: {
            display: true, text: 'Ventas por servicio', font: {
              size: 16
            }
          },

        }
      }
    }
  },
  methods: {
    async ventasPorAnyo() {
      const token = localStorage.getItem("token");

      try {
        let res = await fetch(`http://localhost:5259/api/Analisis/ObtenerVentasPorAnyo`, {
          method: "GET",
          headers: {
            Token: token,
          },
        });

        if (res.status === 401) {
          alert("No tienes permisos");
          return;
        }

        let json = await res.json();
        let xValues = json.map(item => item.anyo);
        let yValues = json.map(item => item.importe);
        let barColorsBase = [
          'rgba(255, 99, 132, 0.2)',
          'rgba(255, 159, 64, 0.2)',
          'rgba(255, 205, 86, 0.2)',
          'rgba(75, 192, 192, 0.2)',
          'rgba(54, 162, 235, 0.2)',
          'rgba(153, 102, 255, 0.2)',
          'rgba(201, 203, 207, 0.2)'
        ];
        let borders = [
          'rgb(255, 99, 132)',
          'rgb(255, 159, 64)',
          'rgb(255, 205, 86)',
          'rgb(75, 192, 192)',
          'rgb(54, 162, 235)',
          'rgb(153, 102, 255)',
          'rgb(201, 203, 207)'
        ];
        let barColors = xValues.map((_, i) => barColorsBase[i % barColorsBase.length]);
        let border = xValues.map((_, i) => borders[i % borders.length]);

        this.chartData = {
          labels: xValues,
          datasets: [{
            data: yValues,
            backgroundColor: barColors,
            borderColor: border,
            borderWidth: 2
          }]
        };

      } catch (err) {
        console.error("Error al obtener datos:", err);
      }
    },
    async ventasPorAnyoAgrupadas() {
      const token = localStorage.getItem("token");
      try {
        let res = await fetch(`http://localhost:5259/api/Analisis/ObtenerVentasPorAnyo`, {
          method: "GET",
          headers: { Token: token }
        });
        if (res.status == 401) {
          alert("No tienes permisos");
          return;
        }

        let json = await res.json();
        let xValues = json.map(item => item.anyo);
        let yValuesSinPagar = json.map(item => item.importePendiente);
        let yValuesPagado = json.map(item => item.importeCobrado);

        this.chartDataAgrupadas = {
          labels: xValues,
          datasets: [
            { label: "Pagado", backgroundColor: "#4bbfbf", data: yValuesPagado, borderColor: 'rgb(75, 192, 192)', borderWidth: 2 },
            { label: "Sin pagar", backgroundColor: "rgba(75, 192, 192, 0.2)", data: yValuesSinPagar, borderColor: 'rgb(75, 192, 192)', borderWidth: 2 }
          ],
        };

        this.chartOptionsAgrupadas = {
          responsive: true,
          scales: {
            y: { stacked: true },
            x: {
              stacked: true
            }
          },
          plugins: {
            legend: {
              display: true,
              labels: {
                font: {
                  size: 16
                }
              }
            },
            title: {
              display: true, text: "Comparativo pagado/impagado por año", font: {
                size: 16
              }
            }
          }
        };

      } catch (err) {
        console.error(err);
      }
    },
    async cargarDatosPie() {
      const token = localStorage.getItem("token");
      try {
        let res = await fetch('http://localhost:5259/api/Analisis/ObtenerVentasPorServicio', {
          method: 'GET',
          headers: { Token: token }
        });
        if (res.status === 401) {
          alert('No tienes permisos');
          return;
        }

        let json = await res.json();
        let labels = json.map(x => x.servicioNombre);
        let dataValues = json.map(x => x.importe);
        let colors = [
          'rgba(255, 99, 132, 0.6)',
          'rgba(255, 159, 64, 0.6)',
          'rgba(255, 205, 86, 0.6)',
          'rgba(153, 102, 255, 0.6)',
          'rgba(54, 162, 235, 0.6)',
          'rgba(75, 192, 192, 0.6)',
          'rgba(201, 203, 207, 0.6)'
        ];
        let borderColor = [
          'rgb(255, 99, 132)',
          'rgb(255, 159, 64)',
          'rgb(255, 205, 86)',
          'rgb(153, 102, 255)',
          'rgb(54, 162, 235)',
          'rgb(75, 192, 192)',
          'rgb(201, 203, 207)'
        ];

        this.pieChartData = {
          labels,
          datasets: [{
            data: dataValues,
            backgroundColor: colors,
            borderColor: borderColor,
            borderWidth: 2,
            hoverOffset: 50
          }]
        }
      } catch (e) {
        console.error(e);
      }
    },
    async mostrarContratacionesMapa2() {
      if (!this.mapaMostrado) {

        const token = localStorage.getItem("token");

        fetch(`http://localhost:5259/api/Localidad/ObtenerLocalidadesConContratacion`, {
          method: "GET",
          headers: { Token: token },
        }).then(res => res.json()).then(json => {
          const ol = window.ol; // <- esto es necesario para usar ol con la CDN

          const map = new ol.Map({
            target: "map",
            layers: [
              new ol.layer.Tile({
                source: new ol.source.OSM()
              })
            ],
            view: new ol.View({
              center: ol.proj.fromLonLat([-3.7038, 40.4168]),
              zoom: 6.5
            })
          });

          const features = json.map(item => {
            const geom = item.geometria;
            const lat = parseFloat(geom.split(" ")[0].split("(")[1]);
            const lon = parseFloat(geom.split(" ")[1].split(")")[0]);

            return new ol.Feature({
              geometry: new ol.geom.Point(ol.proj.fromLonLat([lon, lat]))
            });
          });

          const vectorSource = new ol.source.Vector({ features });

          const heatmapLayer = new ol.layer.Heatmap({
            source: vectorSource,
            blur: 15,
            radius: 10,
            weight: () => 0.8,
          });

          map.addLayer(heatmapLayer);
        }).catch(err => {
          console.error('Error al obtener o mostrar el mapa:', err);
        });

        this.mapaMostrado = true;
      } else {
        return;
      }
    }
  },
  mounted() {
    this.ventasPorAnyo(),
      this.ventasPorAnyoAgrupadas(),
      this.cargarDatosPie()

    const mapasTab = document.getElementById('mapas-tab');
    if (mapasTab) {
      mapasTab.addEventListener('shown.bs.tab', () => {
        this.mostrarContratacionesMapa2();
      });
    }
  }
}
</script>
