﻿using Incentivapp.Models;
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
        /// Actualiza la entidad
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
    }
}