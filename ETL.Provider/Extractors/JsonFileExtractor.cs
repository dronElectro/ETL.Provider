namespace ETL.Database.DataMigrating.Extractors;

internal class JsonFileExtractor : IDataExtractor<object>
{
    public Task<T> Extract<T>(Func<object, CancellationToken, Task<T>> func, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
