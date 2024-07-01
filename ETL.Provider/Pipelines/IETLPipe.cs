using ETL.Provider.Pipelines;

namespace ETL.Database.ExtractingStream;

public interface IETLPipe<TSource, TDestination, TData> :
    IETLExtractingPipeNode<TSource, TData>,
    IETLLoadingPipeNode<TDestination, TData>,
    IETLBasePipe<TSource, TDestination>
{
}