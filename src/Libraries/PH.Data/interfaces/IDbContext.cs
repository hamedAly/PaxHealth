using Microsoft.EntityFrameworkCore;
using PH.Core;

namespace PH.Data;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
    int SaveChanges();
}