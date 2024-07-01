using Microsoft.EntityFrameworkCore;

namespace ETL.Database.DataMigrating;

/// <summary>
/// Конфигуратор контекста базы данных
/// </summary>
internal interface IDbContextConfigurator<TDbContext>
    where TDbContext : DbContext
{
    /// <summary>
    /// Конфигурируем контекст
    /// </summary>
    void Configure(Action<DbContextOptionsBuilder<TDbContext>> builder);

    /// <summary>
    /// Собираем контекст
    /// </summary>
    TDbContext Build();
}
