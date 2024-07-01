using ETL.Database.DataMigrating.Transformers;

namespace ETL.Database.Transformers;

public class BaseDataTransformer : IDataTransformer
{
    public async Task<TTransformedData> Transform<TSourceData, TTransformedData>(
        Func<TSourceData, CancellationToken, Task<TTransformedData>> func,
        TSourceData pipeData,
        CancellationToken cancellationToken)
    {
        return await func(pipeData, cancellationToken);
    }
}
