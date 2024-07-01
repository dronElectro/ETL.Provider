namespace ETL.Provider.Pipelines;

public interface IEtlTransformedPipe<TSource, TDestination, TOldData, TNewData> :
    IETLExtractingPipeNode<TSource, TOldData>,
    IEtlTransformingPipeNode<TOldData, TNewData>,
    IETLLoadingPipeNode<TDestination, TNewData>,
    IETLBasePipe<TSource, TDestination>
{
}
