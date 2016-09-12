using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EoraMarket.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;

        private IDbSet<TEntity> _entities;

        public IQueryable<TEntity> Table
        {
            get {
                throw new NotImplementedException();
            }
        }

        public IQueryable<TEntity> TableNoTracking
        {
            get {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            _context = context;
        }

        public TEntity GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

    }
}
