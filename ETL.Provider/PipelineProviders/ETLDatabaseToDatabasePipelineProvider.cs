using ETL.Database.DataMigrating.Extractors;
using ETL.Database.DataMigrating.Loaders;
using ETL.Database.DataMigrating.Transformers;
using Microsoft.EntityFrameworkCore;

namespace ETL.Database.Pipelines;

public class ETLDatabaseToDatabasePipelineProvider<TSourceDbContext, TDestinationDbContext>
    : IETLDatabaseToDatabasePipelineProvider<TSourceDbContext, TDestinationDbContext>
    where TSourceDbContext : DbContext
    where TDestinationDbContext : DbContext
{
    private readonly IDataExtractor<TSourceDbContext> _extractor;
    private readonly IDataLoader<TDestinationDbContext> _loader;
    private readonly IDataTransformer _transformer;

    public ETLDatabaseToDatabasePipelineProvider(
        IDataExtractor<TSourceDbContext> extractor,
        IDataLoader<TDestinationDbContext> loader,
        IDataTransformer transformer)
    {
        _extractor = extractor;
        _loader = loader;
        _transformer = transformer;
    }

    public async Task<TSourceData> Extract<TSourceData>(Func<TSourceDbContext, CancellationToken, Task<TSourceData>> func, CancellationToken cancellationToken)
    {
        return await _extractor.Extract(func, cancellationToken);
    }

    public async Task Load<TDestinationData>(Func<TDestinationDbContext, TDestinationData, CancellationToken, Task> action, TDestinationData data, CancellationToken cancellationToken)
    {
        await _loader.Load(action, data, cancellationToken);
    }

    public async Task<TDestinationData> Transform<TSourceData, TDestinationData>(Func<TSourceData, CancellationToken, Task<TDestinationData>> func, TSourceData data, CancellationToken cancellationToken)
    {
        return await _transformer.Transform(func, data, cancellationToken);
    }
}
