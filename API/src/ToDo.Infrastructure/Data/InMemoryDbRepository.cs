using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDo.Domain.Contracts;
using ToDo.Domain.Entities;

namespace ToDo.Infrastructure.Data
{
    public class InMemoryDbRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext context;
        private readonly DbSet<T> entities;
        public InMemoryDbRepository(DbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public T Get<T1>(string id)
        {
            return entities.SingleOrDefault(s => s.Id == id);
        }

        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Add(entity);
            context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");

            entities.Update(entity);
            context.SaveChanges();
        }
        public void Delete(string id)
        {
            if (id == null) throw new ArgumentNullException("entity");

            T entity = entities.SingleOrDefault(s => s.Id == id);
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
