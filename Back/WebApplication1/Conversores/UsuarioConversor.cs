using NaureBack.DTOs;
using NaureBack.Models;

namespace NaureBack.Converters
{
    public class UsuarioConversor
    {
        public UsuarioDTO? UsuarioDTO { get; }
        public IList<UsuarioDTO>? ListaUsuariosDTO { get; }
        public Usuario? Usuario { get; }
        
        public UsuarioConversor(Usuario usuario)
        {
            UsuarioDTO = UsuarioToUsuarioDTO(usuario);
        }
        public UsuarioConversor(IList<Usuario> usuarios)
        {
            ListaUsuariosDTO = new List<UsuarioDTO>();

            foreach (Usuario u in usuarios)
            {
                ListaUsuariosDTO.Add(UsuarioToUsuarioDTO(u));
            }
        }

        private UsuarioDTO UsuarioToUsuarioDTO(Usuario usuario)
        {
            UsuarioDTO res = new UsuarioDTO()
            {
                Id = usuario.Id,
                Apellidos = usuario.Apellidos,
                CuentaBancaria = usuario.CuentaBancaria,
                Direccion = usuario.Direccion,
                Email = usuario.Email,
                Empresa = usuario.Empresa,
                EsAdmin = usuario.EsAdmin,
                IdLocalidad = usuario.IdLocalidad,
                Login  = usuario.Login,
                Nombre = usuario.Nombre,
                Password    = usuario.Password,
                Telefono = usuario.Telefono
            };

            if (usuario.IdLocalidadNavigation != null)
            {
                LocalidadConversor localidadConversor = new LocalidadConversor(usuario.IdLocalidadNavigation);
                res.Localidad = localidadConversor.LocalidadDTO;
            }

            return res;
        }
    }
}
