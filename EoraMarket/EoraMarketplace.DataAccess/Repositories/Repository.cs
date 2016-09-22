using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoraMarketplace.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private IDbSet<TEntity> _entities;

        public IQueryable<TEntity> Table
        {
            get {
                return this._entities;
            }
        }
        
        public IQueryable<TEntity> TableNoTracking
        {
            get {
                return this._entities.AsNoTracking();
            }
        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            this._context = context;
            this._entities = context.Set<TEntity>();
        }

        public TEntity GetById(int id)
        {
            return _entities.Find(id);
        }

        public TEntity Insert(TEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            this._entities.AddOrUpdate(entity);
            this._context.SaveChanges();

            return entity;
        }

        public void Delete(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            this._entities.Remove(entity);
            this._context.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            if(entity == null)
                throw new ArgumentNullException("entity");

            this._entities.AddOrUpdate(entity);
            this._context.SaveChanges();

            return entity;
        }
    }
}
