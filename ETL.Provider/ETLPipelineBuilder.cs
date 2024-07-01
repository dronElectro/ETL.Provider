using ETL.Database.ExtractingStream;
using ETL.Provider.Pipelines;
using System.Linq.Expressions;

namespace ETL.Database.Pipelines;

public static class ETLPipelineBuilder
{
    public static IETLPipe<TSource, TDestination, TIn> Extract<TSource, TDestination, TIn>(
        this IETLBasePipelineProvider<TSource, TDestination> context,
        Func<TSource, CancellationToken, Task<TIn>> func)
    {
        return new ETLPipe<TSource, TDestination, TIn>(context, func);
    }

    public static IEtlTransformedPipe<TSource, TDestination, TIn, TOut> Transform<TSource, TDestination, TIn, TOut>(
        this IETLPipe<TSource, TDestination, TIn> pipeline,
        Func<TIn, CancellationToken, Task<TOut>> func)
    {
        return EtlTransformedPipe<TSource, TDestination, TIn, TOut>.FromBasePipe(pipeline, func);
    }

    public static IETLLoadingPipeNode<TDestination, TOut> Load<TDestination, TOut>(
        this IETLLoadingPipeNode<TDestination, TOut> pipeline,
        Func<TDestination, TOut, CancellationToken, Task> func)
    {
        pipeline.AddLoadingLogic(func);

        return pipeline;
    }


    public static async Task StartAsync<TDestination, TOut>(
        this IETLLoadingPipeNode<TDestination, TOut> pipeline,
        CancellationToken cancellationToken)
    {
        await pipeline.StartAsync(cancellationToken);
    }
}