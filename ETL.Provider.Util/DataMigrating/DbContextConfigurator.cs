using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ETL.Database.DataMigrating;

/// <inheritdoc/>
internal class DbContextConfigurator<TDbContext>
    : IDbContextConfigurator<TDbContext>
        where TDbContext : DbContext
{
    private readonly string _dbConnectionStringName;
    private readonly IConfiguration _configuration;
    private readonly DbContextOptionsBuilder<TDbContext> _optionsBuilder;

    /// <summary>
    /// Конструктор
    /// </summary>
    public DbContextConfigurator(string dbConnectionStringName, IConfiguration configuration)
    {
        _dbConnectionStringName = dbConnectionStringName;
        _configuration = configuration;
        _optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
    }

    /// <inheritdoc/>
    public void Configure(Action<DbContextOptionsBuilder<TDbContext>> builder)
    {
        builder.Invoke(_optionsBuilder);
    }

    /// <inheritdoc/>
    public TDbContext Build()
    {
        string? sourceConnectionString = _configuration.GetConnectionString(_dbConnectionStringName);

        if (string.IsNullOrWhiteSpace(sourceConnectionString))
        {
            throw new ArgumentNullException($"Не указана строка подключения для БД {_dbConnectionStringName}.");
        }

        _optionsBuilder.UseNpgsql(sourceConnectionString);

        return (TDbContext)Activator.CreateInstance(typeof(TDbContext), _optionsBuilder.Options)!;
    }
}
