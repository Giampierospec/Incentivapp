using Incentivapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Incentivapp.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T: class

    {
        private Propietaria2Context _db;

        public GenericRepository(Propietaria2Context db)
        {
            _db = db;
        }
        /// <summary>
        /// Agrega una nueva entidad
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Added;
        }
        /// <summary>
        /// Verifica si existe basado en una condicion
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool Exists(Func<T, bool> where)
        {
            return _db
                    .Set<T>()
                    .Any(where);
        }

        /// <summary>
        /// Consigue toda la lista
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return _db
                    .Set<T>()
                    .ToList();
        }
        /// <summary>
        /// Consigue una lista en base a unos parametros
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> where)
        {
            return _db
                    .Set<T>()
                    .Where(where)
                    .ToList();
        }
        /// <summary>
        /// Retorna una sola instancia del objeto
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T GetSingle(Func<T, bool> where)
        {
            return _db
                    .Set<T>()
                    .FirstOrDefault(where);
        }
        /// <summary>
        /// Remueve la entidad
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        }
        /// <summary>
        /// Returns a transformed list
        /// </summary>
        /// <typeparam name="U"></typeparam>
        /// <param name="tr"></param>
        /// <returns></returns>
        public List<U> Transform<U>(Func<T, U> tr)
            where U: class
        {
            return _db.Set<T>()
                       .Select(tr)
                       .ToList();
        }

        /// <summary>
        /// Actualiza la entidad
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public T UpperCaseValues(T entity)
        {
            var props = entity.GetType().GetProperties();
            foreach (var prop in props)
            {
                if(prop.PropertyType.Name == "String")
                    prop.SetValue(entity, prop.GetValue(entity).ToString().ToUpper());
            }
            return entity;
        }
    }
}