using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDb.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {

        IQueryable<T> GetAllQueryable();
        IEnumerable<T> GetAll();
        T GetById(object Id);
        void Insert(T obj);
        void Update(T obj);
        void Delete(Object Id);
        void Save();
    }
}
