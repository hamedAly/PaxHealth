using Microsoft.EntityFrameworkCore;
using PH.Core;
using PH.Core.Domain;
namespace PH.Data;



public class PHealthDbContext : DbContext, IDbContext
{
    public DbSet<LabResult> LabResults { get; set; }
    public DbSet<MedicalDocument> MedicalDocuments { get; set; }

    public PHealthDbContext(DbContextOptions<PHealthDbContext> options)
        : base(options)
    {
    }

    public DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
    {
        return base.Set<TEntity>();
    }
}