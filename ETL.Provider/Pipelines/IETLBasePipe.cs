using ETL.Database.Pipelines;

namespace ETL.Provider.Pipelines;

public interface IETLBasePipe<TSource, TDestination>
{
    IETLBasePipelineProvider<TSource, TDestination> Provider { get; }
}
