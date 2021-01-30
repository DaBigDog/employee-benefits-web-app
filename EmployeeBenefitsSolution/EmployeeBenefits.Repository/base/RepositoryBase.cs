using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using EmployeeBenefits.Database.Models;


namespace EmployeeBenefits.Repository
{
    /// <summary>
    /// Interface for base repository class.
    /// </summary>
    /// <typeparam name="T">T - class</typeparam>
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


    /// <summary>
    /// Base repository class. Abstracts and simplifies EF. 
    /// </summary>
    /// <typeparam name="T">T - class</typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbSet<T> dbSet;

        private EmployeeBenefitsContext dbContext;



        protected RepositoryBase(EmployeeBenefitsContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        /// <summary>
        /// Returns all entities.
        /// </summary>
        /// <returns>T - class entity</returns>
        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        /// <summary>
        /// Returns first entity meeting the where funciton.
        /// </summary>
        /// <param name="where">function - where Linq function</param>
        /// <returns>T - class entity</returns>
        public virtual T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault();
        }

        /// <summary>
        /// Returns first entity meeting the where funciton Asynchronously.
        /// </summary>
        /// <param name="where">function - where Linq function</param>
        /// <returns>T - class entity</returns>
        public virtual async Task<T> GetAsynch(Expression<Func<T, bool>> where)
        {
            return await dbSet.Where(where).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Gets enitity by primary key value.
        /// </summary>
        /// <param name="id">int - id</param>
        /// <returns>T - class entity</returns>
        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }


        /// <summary>
        /// Gets enitity by primary key value Asynchronously.
        /// </summary>
        /// <param name="id">int - id</param>
        /// <returns>T - class entity</returns>
        public virtual async Task<T> GetByIdAsynch(int id)
        {
            return await dbSet.FindAsync(id);
        }


        /// <summary>
        /// Gets enities matching Linq where function with specified child tables (foreign key entities).
        /// </summary>
        /// <param name="where">Linq where clause</param>
        /// <param name="relatedTables">string[] - names of child tables</param>
        /// <returns>IQueryable<T></returns>
        public virtual IQueryable<T> GetRelatedTablesExpression(Expression<Func<T, bool>> where, params string[] relatedTables)
        {
            IQueryable<T> query = dbSet;
            // for each table passed in we'll include it and aggregate to return relatedTables
            query = relatedTables.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(where);
        }


        /// <summary>
        /// Gets enities matching Linq where function.
        /// </summary>
        /// <param name="where">Linq where clause</param>
        /// <returns>IQueryable<T></returns>
        public virtual IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }

        /// <summary>
        /// Adds entitiy to DBSet.
        /// </summary>
        /// <param name="entity">class entity</param>
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Marks entity as modified and attaches to DBSet.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            this.dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Removes entity from DBSet
        /// </summary>
        /// <param name="entity">class entity</param>
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// Saves all changes to the database.
        /// </summary>
        public void Save()
        {
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Saves all changes to the database Asynchonously.
        /// </summary>
        public void SaveAsynch()
        {
            dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Marks entities matching Linq where function as Deleted.
        /// </summary>
        /// <param name="where">Linq function</param>
        public void RemoveRange(Expression<Func<T, bool>> where)
        {
            // This is a bad way to do this but in the interest of time...
            dbContext.RemoveRange(dbSet.Where(where).ToList());
        }

    }



}
