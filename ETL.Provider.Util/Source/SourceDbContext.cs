using ETL.Database.Util.Source;
using Microsoft.EntityFrameworkCore;

namespace ETL.Database.Source;

public partial class SourceDbContext : DbContext
{
    public DbSet<SourceData> SourceData { get; set; }

    public SourceDbContext()
    {
    }

    public SourceDbContext(DbContextOptions<SourceDbContext> options)
        : base(options)
    {
    }
}
