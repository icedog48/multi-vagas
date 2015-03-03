using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Context;

namespace Storage.NHibernate
{
    public class NHibernateRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private enum Operation
        {
            ADD, UPDATE, REMOVE
        }

        private readonly ISessionFactory _SessionFactory;

        public NHibernateRepository(ISession session)
        {
            Session = session;
            _SessionFactory = session.SessionFactory;            
        }

        public ISession Session { get; set; }       

        public IQueryable<TEntity> Items
        {
            get { return Session.Query<TEntity>(); }
        }

        public void Add(TEntity entity)
        {
            Session.SaveOrUpdate(entity);
        }

        public void Add(ICollection<TEntity> entities)
        {
            this.ExecuteBatch(entities, Operation.ADD);
        }

        public void Remove(TEntity entity)
        {
            try
            {
                Session.BeginTransaction();

                Session.Delete(entity);

                Session.Transaction.Commit();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Remove(ICollection<TEntity> entities)
        {
            this.ExecuteBatch(entities, Operation.REMOVE);
        }

        public void Update(TEntity entity)
        {
            Session.BeginTransaction();

            if (Session.Contains(entity))
                Session.Update(entity);
            else
                Session.Merge(entity);

            Session.Transaction.Commit();
        }

        public void Update(ICollection<TEntity> entities)
        {
            this.ExecuteBatch(entities, Operation.UPDATE);
        }

        public TEntity Get(object id)
        {
            return Session.Get<TEntity>(id);
        }

        private void ExecuteBatch(ICollection<TEntity> entities, Operation operation)
        {
            var session = _SessionFactory.OpenStatelessSession();

            session.SetBatchSize(500);

            var tran = session.BeginTransaction();
            try
            {
                foreach (var entity in entities)
                {
                    switch (operation)
                    {
                        case Operation.ADD:
                            session.Insert(entity);
                            break;
                        case Operation.UPDATE: 
                            session.Update(entity);
                            break;
                        case Operation.REMOVE:
                            session.Delete(entity);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }                        
                }

                tran.Commit();
            }
            catch (Exception)
            {
                tran.Rollback();
                throw;
            }
        }
    }
}
