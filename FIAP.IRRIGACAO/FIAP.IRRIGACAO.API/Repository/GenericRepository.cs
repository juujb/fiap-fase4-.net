using FIAP.IRRIGACAO.API.Model;
using Microsoft.EntityFrameworkCore;

namespace FIAP.IRRIGACAO.API.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IEnumerable<T> GetAllPaged(int pageNumber, int pageSize)
        {
            return _dbSet
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public virtual T? FindById(long id)
        {
            return _dbSet.Find(id);
        }

        public virtual T? Create(T model)
        {
            _dbSet.Add(model);
            _context.SaveChanges();

            return model;
        }

        public virtual void Update(T model)
        {
            _dbSet.Update(model);
            _context.SaveChanges();
        }

        public virtual T? DeleteAndReturn(long id)
        {
            var entity = _dbSet.Find(id);

            if (entity == null)
                return null;

            _dbSet.Remove(entity);
            _context.SaveChanges();

            return entity;
        }
    }

}
