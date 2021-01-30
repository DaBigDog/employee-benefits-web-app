using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EmployeeBenefits.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeBenefits.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        T Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAll();
        Task<T> GetAsynch(Expression<Func<T, bool>> where);
        IQueryable<T> GetRelatedTablesExpression(Expression<Func<T, bool>> where, params string[] relatedTables);
        T GetById(int id);
        Task<T> GetByIdAsynch(int id);
        void Save();
        void SaveAsynch();
        void Update(T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> where);

        void RemoveRange(Expression<Func<T, bool>> where);
    }

    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbSet<T> dbSet;

        private EmployeeBenefitsContext dbContext;



        protected RepositoryBase(EmployeeBenefitsContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }


        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault();
        }

        public virtual async Task<T> GetAsynch(Expression<Func<T, bool>> where)
        {
            return await dbSet.Where(where).FirstOrDefaultAsync();
        }


        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<T> GetByIdAsynch(int id)
        {
            return await dbSet.FindAsync(id);
        }


        public virtual IQueryable<T> GetRelatedTablesExpression(Expression<Func<T, bool>> where, params string[] relatedTables)
        {
            IQueryable<T> query = dbSet;
            // for each table passed in we'll include it and aggregate to return relatedTables
            query = relatedTables.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(where);
        }



        public virtual IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }


        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }


        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }


        public void Save()
        {
            dbContext.SaveChanges();
        }

        public void SaveAsynch()
        {
            dbContext.SaveChangesAsync();
        }

        // This is a bad way to do this but in the interest of time...
        public void RemoveRange(Expression<Func<T, bool>> where)
        {
            dbContext.RemoveRange(dbSet.Where(where).ToList());
        }

    }



}
