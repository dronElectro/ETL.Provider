using Microsoft.EntityFrameworkCore;

namespace ETL.Database.DataMigrating.Extractors;

public sealed class DatabaseExtractor<TDbContext> : IDataExtractor<TDbContext>
    where TDbContext : DbContext
{
    private readonly TDbContext _context;

    public DatabaseExtractor(TDbContext context)
    {
        _context = context;
    }

    public async Task<T> Extract<T>(
        Func<TDbContext, CancellationToken, Task<T>> func,
        CancellationToken cancellationToken)
    {
        return await func(_context, cancellationToken);
    }
}