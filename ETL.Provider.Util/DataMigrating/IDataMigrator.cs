using ETL.Database.Source;
using ETL.Database.Target;

namespace ETL.Database.DataMigrating;

/// <summary>
/// Сервис для миграции данных из одной БД в другую
/// </summary>
internal interface IDataMigrator
{
    /// <summary>
    /// Добавляем основную логику переноса данных
    /// </summary>
    /// <param name="logicImplementation"> Лямбда, описывающая логику </param>
    void AddLogic(Func<SourceDbContext, TargetDbContext, Task> logicImplementation);

    /// <summary>
    /// Запускаем миграцию данных
    /// </summary>
    /// <param name="cancellationToken"> <see cref="CancellationToken"/> </param>
    Task RunAsync(CancellationToken cancellationToken);
}
