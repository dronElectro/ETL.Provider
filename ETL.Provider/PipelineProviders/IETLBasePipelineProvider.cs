namespace ETL.Database.Pipelines;

public interface IETLBasePipelineProvider<TSource, TDestination>
{
    internal Task<TSourceData> Extract<TSourceData>(Func<TSource, CancellationToken, Task<TSourceData>> func, CancellationToken cancellationToken);

    internal Task<TDestinationData> Transform<TSourceData, TDestinationData>(Func<TSourceData, CancellationToken, Task<TDestinationData>> func, TSourceData data, CancellationToken cancellationToken);

    internal Task Load<TDestinationData>(Func<TDestination, TDestinationData, CancellationToken, Task> action, TDestinationData data, CancellationToken cancellationToken);
}
