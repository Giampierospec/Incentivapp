using Incentivapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Incentivapp.Repository
{
    public class UnitOfWork : IDisposable
    {
        private Propietaria2Context _db;

        public GenericRepository<Premio> PremioRepository { get; }
        public GenericRepository<TipoPremio> TipoPremioRepository { get; }
        public GenericRepository<Medicion> MedicionRepository { get; }
        public GenericRepository<Role> RolRepository { get; }
        public GenericRepository<Usuario> UsuarioRepository { get; }
        /// <summary>
        /// Inicializa los repositorios
        /// </summary>
        /// <param name="db"></param>
        public UnitOfWork(Propietaria2Context db)
        {
            _db = db;
            PremioRepository = new GenericRepository<Premio>(db);
            TipoPremioRepository = new GenericRepository<TipoPremio>(db);
            MedicionRepository = new GenericRepository<Medicion>(db);
            RolRepository = new GenericRepository<Role>(db);
            UsuarioRepository = new GenericRepository<Usuario>(db);
        }
        /// <summary>
        /// Guarda los cambios en la base de datos
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}