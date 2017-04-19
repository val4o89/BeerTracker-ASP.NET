namespace UnitOfWork.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<TEntity>
    {
        void Add(TEntity entity);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        void DeleteRange(Expression<Func<TEntity, bool>> predicate);

        TEntity FindFirst(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> FindMany(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> GetAll();

        bool Any();
        bool Any(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> predicate);
    }
}
