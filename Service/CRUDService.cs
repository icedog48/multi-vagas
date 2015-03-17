using Model;
using Service.Interfaces;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public abstract class CRUDService<T>: ICRUDService<T> where T : Entity
    {
        protected IRepository<T> repository;

        public CRUDService(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public void Add(T obj)
        {
            this.repository.Add(obj);
        }

        public void Update(T obj)
        {
            this.repository.Update(obj);
        }

        public void Remove(T obj)
        {
            this.repository.Remove(obj);
        }

        public IEnumerable<T> GetByFilter(Filters.IFilter<T> filter)
        {
            var result = filter.Apply(repository.Items);

            return result.ToList();
        }

        public T GetById(int id)
        {
            var list = from item in repository.Items
                                  where item.Id.Equals(id)
                                  select item;

            if (!list.Any()) throw new InvalidOperationException();

            return list.First();
        }

        public IEnumerable<T> GetAll()
        {
            return repository.Items.ToList();
        }
    }
}
