using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccessLayer.Generic
{
    public class GenericRepository<TEntity> where TEntity : class
    {

        #region VARIABLES

        internal DbContext context;
        internal DbSet<TEntity> dbSet;

        #endregion

        #region CONSTRUCTORS

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        #endregion

        #region METHODS

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? skip = null, int? take = null)
        {
            IQueryable<TEntity> query = dbSet;

            int iSkip = skip ?? 0;
            int iTake = (take > 1000 ? 1000 : take) ?? 1000;

            if (filter != null)
                query = query.Where(filter);


            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
            {
                return orderBy(query)
                    .Skip(() => iSkip)
                    .Take(() => iTake)
                    .ToList();
            }
            else
            {
                return query
                    .Skip(() => iSkip)
                    .Take(() => iTake)
                    .ToList();
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int? skip = null, int? take = null)
        {
            IQueryable<TEntity> query = dbSet;

            int iSkip = skip ?? 0;
            int iTake = (take > 1000 ? 1000 : take) ?? 1000;

            if (filter != null)
                query = query.Where(filter);


            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
            {
                return await orderBy(query)
                    .Skip(() => iSkip)
                    .Take(() => iTake)
                    .ToListAsync();
            }
            else
            {
                return query
                    .Skip(() => iSkip)
                    .Take(() => iTake)
                    .ToList();
            }
        }
 
        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) { return orderBy(query).FirstOrDefault(); }
            else { return query.FirstOrDefault(); }
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) { return await orderBy(query).FirstOrDefaultAsync(); }
            else { return await query.FirstOrDefaultAsync(); }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<TEntity> GetByIDAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetRawSQL(string query, params object[] parameters)
        {
            return dbSet.SqlQuery(query, parameters).AsQueryable();
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);

            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual long GetTotalRows()
        {
            return dbSet.LongCount();
        }

        #endregion

    }
}
