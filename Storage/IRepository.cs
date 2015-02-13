using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Items { get; }

        void Add(TEntity entity);

        void Add(ICollection<TEntity> entities);

        void Remove(TEntity entity);

        void Remove(ICollection<TEntity> entities);

        void Update(TEntity entity);

        void Update(ICollection<TEntity> entities);

        TEntity Get(object id); 
    }
}
