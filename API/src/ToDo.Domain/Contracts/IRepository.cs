using System.Collections.Generic;

namespace ToDo.Domain.Contracts
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get<T1>(string id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(string id);
        

    }
}
