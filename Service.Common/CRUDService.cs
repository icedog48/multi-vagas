using FluentValidation;
using Model.Common;
using Model.Common.Interfaces;
using Service.Common.Interfaces;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public abstract class CRUDService<T> : ICRUDService<T> where T : Entity
    {
        protected IRepository<T> repository;
        protected AbstractValidator<T> validator;

        public CRUDService(IRepository<T> repository, AbstractValidator<T> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public virtual void Add(T obj)
        {
            var result = validator.Validate(obj);

            if (!result.IsValid) throw new ValidationException(result.Errors);

            this.repository.Add(obj);
        }

        public virtual void Update(T obj)
        {
            var result = validator.Validate(obj);

            if (!result.IsValid) throw new ValidationException(result.Errors);

            this.repository.Update(obj);
        }

        public virtual void Remove(T obj)
        {
            this.repository.Remove(obj);
        }

        public virtual IEnumerable<T> GetByFilter(Service.Common.Filters.IFilter<T> filter)
        {
            return filter.Apply(repository.Items); 
        }

        public virtual T GetById(int id)
        {
            var list = from item in repository.Items
                                  where item.Id.Equals(id)
                                  select item;

            if (!list.Any()) throw new InvalidOperationException();

            return list.First();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return repository.Items.ToList();
        }
    }
}
