using ETL.Database.DataMigrating.Loaders;
using Microsoft.EntityFrameworkCore;

namespace ETL.Database.Loaders;

public class DatabaseLoader<TDbContext> : IDataLoader<TDbContext>
    where TDbContext : DbContext
{
    private readonly TDbContext _context;

    public DatabaseLoader(TDbContext context)
    {
        _context = context;
    }

    public async Task Load<T>(
        Func<TDbContext, T, CancellationToken, Task> action,
        T data,
        CancellationToken cancellationToken)
    {
        await action(_context, data, cancellationToken);
    }
}
