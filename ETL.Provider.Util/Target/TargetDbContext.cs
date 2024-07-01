using ETL.Database.Util.Target;
using Microsoft.EntityFrameworkCore;

namespace ETL.Database.Target;

public partial class TargetDbContext : DbContext
{
    public DbSet<TargetData> TargetData { get; set; }

    public TargetDbContext()
    {
    }

    public TargetDbContext(DbContextOptions<TargetDbContext> options)
        : base(options)
    {
    }
}
