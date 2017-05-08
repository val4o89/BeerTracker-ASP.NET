namespace BeerTracker.Web.Tests.Mocked
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using UnitOfWork.Contracts;

    public class FakeRepository<T> : IRepository<T> where T : class
    {
        private List<T> entities;

        public FakeRepository()
        {
            this.entities = new List<T>();
        }

        public void Add(T entity)
        {
            this.entities.Add(entity);
        }

        public bool Any()
        {
            return this.entities.Any();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return this.entities.AsQueryable().Where(predicate) == null ? false : true;
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            T entity = this.entities.AsQueryable().FirstOrDefault(predicate);
            this.entities.Remove(entity);
        }

        public void DeleteRange(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public T FindFirst(Expression<Func<T, bool>> predicate)
        {
            return this.entities.AsQueryable().FirstOrDefault(predicate);
        }

        public IQueryable<T> FindMany(Expression<Func<T, bool>> predicate)
        {
            return this.entities.AsQueryable().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return this.entities.AsQueryable();
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> predicate)
        {
            return this.entities.AsQueryable().Select(predicate);
        }
    }
}
