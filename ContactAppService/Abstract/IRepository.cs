using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactAppService.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object Id);
        T Insert(T obj);
        void Delete(T obj);
        T Update(T obj);
        void Save();
    }

}
