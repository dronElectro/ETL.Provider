using ETL.Database.DataMigrating.Extractors;
using ETL.Database.Loaders;
using ETL.Database.Transformers;
using Microsoft.EntityFrameworkCore;

namespace ETL.Database.Pipelines;

public interface IETLDatabaseToDatabasePipelineProvider<TSourceDbContext, TDestinationDbContext>
    : IETLPipelineProvider<DatabaseExtractor<TSourceDbContext>, TSourceDbContext, BaseDataTransformer, DatabaseLoader<TDestinationDbContext>, TDestinationDbContext>
    where TSourceDbContext : DbContext
    where TDestinationDbContext : DbContext
{
}
