using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using NaureBack.DTOs;
using NaureBack.Models;
using NaureBack.Converters;

namespace NaureBack.Services
{
    public class AnalisisServicio
    {
        private readonly NaureContext _context;

        public AnalisisServicio(NaureContext context)
        {
            _context = context;
        }

        public AnalisisServicio()
        {
        }

        /// <summary>
        /// Listamos todos los tramos de la entidad Tramo y agrupamos por año el importe de cada uno de los tramos
        /// Agrupamos también las cantidades en las categorías "ImporteCobrado" e "ImportePendiente"
        /// </summary>
        /// <returns>Una lista con el acumulado de importe total y los cobrados e impagados de cada año</returns>
        //public async Task<IList<AnalisisDTO>> ObtenerVentasPorAnyoAsync()
        //{
        //    IList<AnalisisDTO> res = new List<AnalisisDTO>();
        //    bool encontrado;
        //    TramoServicio tramoServicio = new TramoServicio(_context);
        //    IList<Tramo> tramos = await tramoServicio.ListarTodosTramosAsync();

        //    //Recorremos todos los tramos para ir acumulando los importes
        //    foreach (Tramo tramo in tramos)
        //    {
        //        encontrado = false;

        //        //Por cada tramo vemos en la lista donde almacenamos los acumulados si está ya el año de este tramo concreto
        //        foreach (AnalisisDTO a in res)
        //        {
        //            //Si ya está el año en la lista de salida, acumulamos el valor
        //            if (a.Anyo == tramo.FechaDesde.Value.Year)
        //            {
        //                encontrado = true;
        //                a.Importe += tramo.Importe.Value;

        //                //Dependiendo de si está o no pagado, acumulamos en cobrado o pendiente
        //                if (tramo.Pagado.Value)
        //                {
        //                    a.ImporteCobrado += tramo.Importe.Value;
        //                }
        //                else
        //                {
        //                    a.ImportePendiente += tramo.Importe.Value;
        //                }
        //            }
        //        }

        //        //Si entramos en este condicional es que es un año nuevo y registramos sus valores
        //        if (!encontrado)
        //        {
        //            AnalisisDTO aux = new AnalisisDTO()
        //            {
        //                Anyo = tramo.FechaDesde.Value.Year,
        //                Importe = tramo.Importe.Value,
        //                ImporteCobrado = 0,
        //                ImportePendiente = 0
        //            };

        //            //Dependiendo de si está o no pagado, acumulamos en cobrado o pendiente
        //            if (tramo.Pagado.Value)
        //            {
        //                aux.ImporteCobrado = tramo.Importe.Value;
        //            }
        //            else
        //            {
        //                aux.ImportePendiente = tramo.Importe.Value;
        //            }

        //            res.Add(aux);
        //        }
        //    }

        //    return res;
        //}

        /// <summary>
        /// Listamos todos los tramos de la entidad Tramo y agrupamos por año el importe de cada uno de los tramos
        /// Agrupamos también las cantidades en las categorías "ImporteCobrado" e "ImportePendiente"
        /// </summary>
        /// <returns>Una lista con el acumulado de importe total y los cobrados e impagados de cada año</returns>
        public async Task<IList<AnalisisDTO>> ObtenerVentasPorAnyoAsync()
        {
            IList<AnalisisDTO> res = new List<AnalisisDTO>();
            TramoServicio tramoServicio = new TramoServicio(_context);
            IList<Tramo> tramos = await tramoServicio.ListarTodosTramosAsync();
            TramoConversor tramoConversor = new TramoConversor(tramos);
            IList<TramoDTO> tramosDTO = tramoConversor.ListaTramoDTO;

            res = ObtenerVentasPorAnyo(tramosDTO);

            return res;
        }

        /// <summary>
        /// Recibe una lista de tramos y se agrupa por años sumando los importes en cada año y los importes separados por impagados y pagados de cada año
        /// </summary>
        /// <param name="tramos">Lista de tramos a agrupar</param>
        /// <returns>Una lista con el acumulado de importe total y los cobrados e impagados de cada año</returns>
        public IList<AnalisisDTO> ObtenerVentasPorAnyo(IList<TramoDTO> tramos)
        {
            IList<AnalisisDTO> res = new List<AnalisisDTO>();
            bool encontrado;

            //Recorremos todos los tramos para ir acumulando los importes
            foreach (TramoDTO tramo in tramos)
            {
                encontrado = false;

                //Por cada tramo vemos en la lista donde almacenamos los acumulados si está ya el año de este tramo concreto
                foreach (AnalisisDTO a in res)
                {
                    //Si ya está el año en la lista de salida, acumulamos el valor
                    if (a.Anyo == tramo.FechaDesde.Value.Year)
                    {
                        encontrado = true;
                        a.Importe += tramo.Importe.Value;

                        //Dependiendo de si está o no pagado, acumulamos en cobrado o pendiente
                        if (tramo.Pagado.Value)
                        {
                            a.ImporteCobrado += tramo.Importe.Value;
                        }
                        else
                        {
                            a.ImportePendiente += tramo.Importe.Value;
                        }
                    }
                }

                //Si entramos en este condicional es que es un año nuevo y registramos sus valores
                if (!encontrado)
                {
                    AnalisisDTO aux = new AnalisisDTO()
                    {
                        Anyo = tramo.FechaDesde.Value.Year,
                        Importe = tramo.Importe.Value,
                        ImporteCobrado = 0,
                        ImportePendiente = 0
                    };

                    //Dependiendo de si está o no pagado, acumulamos en cobrado o pendiente
                    if (tramo.Pagado.Value)
                    {
                        aux.ImporteCobrado = tramo.Importe.Value;
                    }
                    else
                    {
                        aux.ImportePendiente = tramo.Importe.Value;
                    }

                    res.Add(aux);
                }
            }

            return res;
        }

        /// <summary>
        /// Listamos todos los tramos de la entidad Tramo y todos los servicios
        /// Agrupamos los importes por el id del servicio prestado en la contratación asociada al tramo
        /// Agrupamos también las cantidades en las categorías "ImporteCobrado" e "ImportePendiente"
        /// Buscamos cada id de la lista con los valores acumulados para devolver también el nombre del servicio
        /// </summary>
        /// <returns>Una lista con el acumulado de importe total y los cobrados e impagados de cada servicio</returns>
        //public async Task<IList<AnalisisDTO>> ObtenerVentasPorServicioAsync()
        //{
        //    IList<AnalisisDTO> res = new List<AnalisisDTO>();
        //    bool encontrado;
        //    TramoServicio tramoServicio = new TramoServicio(_context);
        //    ServicioServicio servicioServicio = new ServicioServicio(_context);
        //    IList<Tramo> tramos = await tramoServicio.ListarTodosTramosAsync();
        //    IList<Servicio> servicio = await servicioServicio.ListarTodosServiciosAsync();

        //    //Recorremos todos los tramos para ir acumulando los importes
        //    foreach (Tramo tramo in tramos)
        //    {
        //        encontrado = false;

        //        //Por cada tramo vemos en la lista donde almacenamos los acumulados si está ya el servicio de la contratación asociada al tramo concreto
        //        foreach (AnalisisDTO a in res)
        //        {
        //            //Si ya está el servicio en la lista de salida, acumulamos el valor
        //            if (a.ServicioId == tramo.IdContratacionNavigation.IdServicio)
        //            {
        //                encontrado = true;
        //                a.Importe += tramo.Importe.Value;

        //                if (tramo.Pagado.Value)
        //                {
        //                    a.ImporteCobrado += tramo.Importe.Value;
        //                }
        //                else
        //                {
        //                    a.ImportePendiente += tramo.Importe.Value;
        //                }
        //            }
        //        }

        //        //Si entramos en este condicional es que es un servicio nuevo y registramos sus valores
        //        if (!encontrado)
        //        {
        //            AnalisisDTO aux = new AnalisisDTO()
        //            {
        //                ServicioId = tramo.IdContratacionNavigation.IdServicio,
        //                Importe = tramo.Importe.Value,
        //                ImporteCobrado = 0,
        //                ImportePendiente = 0
        //            };

        //            //Dependiendo de si está o no pagado, acumulamos en cobrado o pendiente
        //            if (tramo.Pagado.Value)
        //            {
        //                aux.ImporteCobrado = tramo.Importe.Value;
        //            }
        //            else
        //            {
        //                aux.ImportePendiente = tramo.Importe.Value;
        //            }

        //            res.Add(aux);
        //        }
        //    }

        //    if (res.Count > 0)
        //    {
        //        //Recorremos cada uno de los registros con los valores ya acumulados y buscamos en la lista de servicios para meter el nombre de cada servicio además del ID
        //        foreach (AnalisisDTO a in res)
        //        {
        //            encontrado = false;

        //            for (int i = 0; i < servicio.Count && !encontrado; i++)
        //            {
        //                if (servicio[i].Id == a.ServicioId)
        //                {
        //                    a.ServicioNombre = servicio[i].Nombre;
        //                    encontrado = true;
        //                }
        //            }
        //        }
        //    }

        //    return res;
        //}

        /// <summary>
        /// Listamos todos los tramos de la entidad Tramo y todos los servicios
        /// Agrupamos los importes por el id del servicio prestado en la contratación asociada al tramo
        /// Agrupamos también las cantidades en las categorías "ImporteCobrado" e "ImportePendiente"
        /// Buscamos cada id de la lista con los valores acumulados para devolver también el nombre del servicio
        /// </summary>
        /// <returns>Una lista con el acumulado de importe total y los cobrados e impagados de cada servicio</returns>
        public async Task<IList<AnalisisDTO>> ObtenerVentasPorServicioAsync()
        {
            IList<AnalisisDTO> res = new List<AnalisisDTO>();
            TramoServicio tramoServicio = new TramoServicio(_context);
            ServicioServicio servicioServicio = new ServicioServicio(_context); 
            IList<Tramo> tramos = await tramoServicio.ListarTodosTramosAsync();
            IList<Servicio> servicios = await servicioServicio.ListarTodosServiciosAsync(); 
            TramoConversor tramoConversor = new TramoConversor(tramos);
            ServicioConversor servicioConversor = new ServicioConversor(servicios);
            IList<TramoDTO> tramosDTO = tramoConversor.ListaTramoDTO;
            IList<ServicioDTO> serviciosDTO = servicioConversor.ListaServiciosDTO;

            res = ObtenerVentasPorServicio(tramosDTO, serviciosDTO);

            return res;
        }

        /// <summary>
        /// Recibe una lista con los tramos a agrupar y otra lista con los posibles servicios de la BBDD. Por cada uno de los servicios se agrupará la cantidad
        /// de dinero acumulado total y las cantidades de pagado e impagado
        /// </summary>
        /// <param name="tramos">Lista de tramos con los importes a agrupar</param>
        /// <param name="servicios">Lista de servicios para realizar la agrupación</param>
        /// <returns>Una lista con el acumulado de importe total y los cobrados e impagados de cada servicio</returns>
        public IList<AnalisisDTO> ObtenerVentasPorServicio(IList<TramoDTO> tramos, IList<ServicioDTO> servicios)
        {
            IList<AnalisisDTO> res = new List<AnalisisDTO>();
            bool encontrado;

            //Recorremos todos los tramos para ir acumulando los importes
            foreach (TramoDTO tramo in tramos)
            {
                encontrado = false;

                //Por cada tramo vemos en la lista donde almacenamos los acumulados si está ya el servicio de la contratación asociada al tramo concreto
                foreach (AnalisisDTO a in res)
                {
                    //Si ya está el servicio en la lista de salida, acumulamos el valor
                    if (a.ServicioId == tramo.Contratacion.IdServicio)
                    {
                        encontrado = true;
                        a.Importe += tramo.Importe.Value;

                        if (tramo.Pagado.Value)
                        {
                            a.ImporteCobrado += tramo.Importe.Value;
                        }
                        else
                        {
                            a.ImportePendiente += tramo.Importe.Value;
                        }
                    }
                }

                //Si entramos en este condicional es que es un servicio nuevo y registramos sus valores
                if (!encontrado)
                {
                    AnalisisDTO aux = new AnalisisDTO()
                    {
                        ServicioId = tramo.Contratacion.IdServicio,
                        Importe = tramo.Importe.Value,
                        ImporteCobrado = 0,
                        ImportePendiente = 0
                    };

                    //Dependiendo de si está o no pagado, acumulamos en cobrado o pendiente
                    if (tramo.Pagado.Value)
                    {
                        aux.ImporteCobrado = tramo.Importe.Value;
                    }
                    else
                    {
                        aux.ImportePendiente = tramo.Importe.Value;
                    }

                    res.Add(aux);
                }
            }

            if (res.Count > 0)
            {
                //Recorremos cada uno de los registros con los valores ya acumulados y buscamos en la lista de servicios para meter el nombre de cada servicio además del ID
                foreach (AnalisisDTO a in res)
                {
                    encontrado = false;

                    for (int i = 0; i < servicios.Count && !encontrado; i++)
                    {
                        if (servicios[i].Id == a.ServicioId)
                        {
                            a.ServicioNombre = servicios[i].Nombre;
                            encontrado = true;
                        }
                    }
                }
            }

            return res;
        }
    }
}
