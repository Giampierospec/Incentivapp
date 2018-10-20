using Incentivapp.Models;
using Incentivapp.Repository;

namespace Incentivapp.Utils
{
    public class UserUtil
    {
        private static GenericRepository<Usuario> _repo = new UnitOfWork(new Propietaria2Context()).UsuarioRepository;
        /// <summary>
        /// Revisa si el usuario esta loggeado
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool IsLogged(Usuario user) => user != null;
        /// <summary>
        /// Verifica si el usuario esta en el rol
        /// </summary>
        /// <param name="Role"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsInRole(string Role, int id) => _repo.Exists(x => x.Role.nombre == Role && x.idUsuario == id);
        /// <summary>
        /// retorna si tiene credenciales validos
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool HasValidCredentials(Usuario user) => _repo.Exists(x => x.email == user.email && x.password == user.password);
        /// <summary>
        /// Consigue al usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static Usuario GetUsuario(Usuario user) => _repo.GetSingle(x => x.email == user.email && x.password == user.password);

    }
}