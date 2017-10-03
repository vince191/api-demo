using DataAccessLayer.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Generic
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {

        #region PROTECTED MEMBERS

        protected DatabaseModel context;

        #endregion

        #region PRIVATE MEMBERS 
         
        private bool disposed = false;

        private GenericRepository<User> userRepo;
        private GenericRepository<Product> productRepo;

        #endregion

        #region PROPERTIES

        public GenericRepository<User> Users
        {
            get
            {
                if (this.userRepo == null)
                    this.userRepo = new GenericRepository<User>(context);

                return userRepo;
            }
        }

        public GenericRepository<Product> Products
        {
            get
            {
                if (this.productRepo == null)
                    this.productRepo = new GenericRepository<Product>(context);

                return productRepo;
            }
        }

        #endregion

        #region CONSTRUCTORS

        public UnitOfWork(DatabaseModel db)
        {
            context = db;

            // Log EF queries to Console for debugging
            context.Database.Log = Console.WriteLine;

            // Not saving anything so can safely disable this
            context.Configuration.ValidateOnSaveEnabled = false;

            // Not using lazy loadig in project so it can be safely disabled
            context.Configuration.LazyLoadingEnabled = false;
        }

        #endregion

        #region METHODS

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }

        #endregion

        #region GC

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    context.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }

}
