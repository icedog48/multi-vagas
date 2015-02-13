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

        private ISession _Session;

        public NHibernateRepository(ISessionFactory sessionFactory)
        {
            _SessionFactory = sessionFactory;
        }

        private ISession GetSession
        {
            get
            {
                if (_Session == null)
                {
                    try
                    {
                        _Session = _SessionFactory.GetCurrentSession();
                    }
                    catch (HibernateException)
                    {
                        _Session = _SessionFactory.OpenSession();
                    }
                    catch (Exception)
                    {
                        _Session = _SessionFactory.OpenSession();
                    }
                }
                return _Session;
            }
        }

        public IQueryable<TEntity> Items
        {
            get { return GetSession.Query<TEntity>(); }
        }

        public void Add(TEntity entity)
        {
            GetSession.SaveOrUpdate(entity);
        }

        public void Add(ICollection<TEntity> entities)
        {
            this.Save(entities, Operation.ADD);
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ICollection<TEntity> entities)
        {
            this.Save(entities, Operation.REMOVE);
        }

        public void Update(TEntity entity)
        {
            var session = GetSession;

            if (session.Contains(entity))
                session.Update(entity);
            else
                session.Merge(entity);
        }

        public void Update(ICollection<TEntity> entities)
        {
            this.Save(entities, Operation.UPDATE);
        }

        public TEntity Get(object id)
        {
            return GetSession.Get<TEntity>(id);
        }

        private void Save(ICollection<TEntity> entities, Operation operation)
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
