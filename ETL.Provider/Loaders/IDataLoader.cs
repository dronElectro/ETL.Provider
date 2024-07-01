namespace ETL.Database.DataMigrating.Loaders;

public interface IDataLoader<TDestination>
{
    Task Load<T>(
        Func<TDestination, T, CancellationToken, Task> action,
        T pipeData,
        CancellationToken cancellationToken);
}
