namespace ETL.Database.DataMigrating.Extractors;

public interface IDataExtractor<TSource>
{
    Task<T> Extract<T>(
        Func<TSource, CancellationToken, Task<T>> func,
        CancellationToken cancellationToken);
}
