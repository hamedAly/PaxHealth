using Microsoft.EntityFrameworkCore;
using PH.Core;
using PH.Core.Data;

namespace PH.Data.Repo;

public class EntityRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly IDbContext _context;
    private DbSet<T> _entities;

    public EntityRepository(IDbContext context)
    {
        _context = context;
    }

    public virtual T GetById(object id)
    {
        return Entities.Find(id);
    }

    public virtual void Insert(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Entities.Add(entity);
        _context.SaveChanges();
    }

    public virtual void Update(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        _context.SaveChanges();
    }

    public virtual void Delete(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        Entities.Remove(entity);
        _context.SaveChanges();
    }

     
    public virtual IQueryable<T> GetAll()
    {
        return Entities;
    }

    protected virtual DbSet<T> Entities
    {
        get
        {
            if (_entities == null)
                _entities = _context.Set<T>();
            return _entities;
        }
    }

}