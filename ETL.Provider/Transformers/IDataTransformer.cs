namespace ETL.Database.DataMigrating.Transformers;

public interface IDataTransformer
{
    Task<TTransformedData> Transform<TSourceData, TTransformedData>(
        Func<TSourceData, CancellationToken, Task<TTransformedData>> func,
        TSourceData sourceData,
        CancellationToken cancellationToken);
}
