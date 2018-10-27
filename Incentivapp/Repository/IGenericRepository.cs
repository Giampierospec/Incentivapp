using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Incentivapp.Repository
{
    public interface IGenericRepository<T>
    where T:class
    {
        List<T> GetAll();
        List<T> GetList(Expression<Func<T, bool>> where);
        T GetSingle(Func<T, bool> where);
        List<U> Transform<U>(Func<T,U> tr) where U: class;
        bool Exists(Func<T, bool> where);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        T UpperCaseValues(T entity);

    }
}
