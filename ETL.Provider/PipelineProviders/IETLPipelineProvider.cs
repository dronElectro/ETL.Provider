using ETL.Database.DataMigrating.Extractors;
using ETL.Database.DataMigrating.Loaders;
using ETL.Database.DataMigrating.Transformers;

namespace ETL.Database.Pipelines;

public interface IETLPipelineProvider<TExtractor, TSource, TTransformer, TLoader, TDestination> : IETLBasePipelineProvider<TSource, TDestination>
    where TExtractor : IDataExtractor<TSource>
    where TTransformer : IDataTransformer
    where TLoader : IDataLoader<TDestination>
{

}