using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Services
{
    public class PalabraClaveServicio
    {
        //Lista donde se almacenarán las palabras clave permitidas en el back
        private IList<PalabraClaveDTO> listaPalabraClaveDTOs;

        public PalabraClaveServicio()
        {
            listaPalabraClaveDTOs = new List<PalabraClaveDTO>();
        }

        /// <summary>
        /// Metemos una nueva pabla clave permitida y le damos validez durante 10 minutos
        /// </summary>
        /// <param name="palabraClave">Palabla clave permitida a insertar en la lista</param>
        public void Insertar(string palabraClave)
        {
            PalabraClaveDTO nuevo = new PalabraClaveDTO()
            {
                PalabraClave = palabraClave,
                FechaTope = DateTime.Now.AddMinutes(10)
            };

            listaPalabraClaveDTOs.Add(nuevo);
        }

        /// <summary>
        /// Comprobamos si existe una palabla clave y el tiempo de validez está dentro del tiempo actual
        /// En caso de buscar una palabra y estar activa, le incrementamos el tiempo de validez. Esto quiere decir
        /// que se están haciendo peticiones al back para un usuario concreto. El usuario está usando la web y puede que 
        /// lo siga haciendo en los próximos minutos
        /// </summary>
        /// <param name="palabraClave">Palabra clave para comprobar validez</param>
        /// <returns>True si la palabra existe y está activa. False si la palabra no existe o no está ya en tiempo de validez</returns>
        public bool Existe(string palabraClave)
        {
            bool res = false;

            foreach(PalabraClaveDTO p in listaPalabraClaveDTOs)
            {
                if (p.PalabraClave == palabraClave && p.FechaTope > DateTime.Now)
                {
                    res = true;
                    p.FechaTope = DateTime.Now.AddMinutes(10);
                }
            }

            return res;
        }
    }
}
