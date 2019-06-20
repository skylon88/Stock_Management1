using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Core.Data
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UnitOfWork _unitOfWork;

        protected GenericRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }

        public virtual IQueryable<T> GetAll()
        {
            Refresh();
            IQueryable<T> query = _unitOfWork.DatabaseContext.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            Refresh();
            IQueryable<T> query = _unitOfWork.DatabaseContext.Set<T>().Where(predicate);
            return query;
        }

        public virtual void Add(T entity)
        {
            _unitOfWork.DatabaseContext.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _unitOfWork.DatabaseContext.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _unitOfWork.DatabaseContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _unitOfWork.Commit();
        }

        public virtual void Refresh()
        {
            var context = ((IObjectContextAdapter)_unitOfWork.DatabaseContext).ObjectContext;
            var refreshableObjects = _unitOfWork.DatabaseContext.ChangeTracker.Entries().Select(c => c.Entity).ToList();
            context.Refresh(RefreshMode.StoreWins, refreshableObjects);
        }
    }
}
