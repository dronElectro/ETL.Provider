using ETL.Database.ExtractingStream;
using ETL.Database.Pipelines;
using System.Linq.Expressions;

namespace ETL.Provider.Pipelines;

public sealed class EtlTransformedPipe<TSource, TDestination, TOldData, TNewData> : IEtlTransformedPipe<TSource, TDestination, TOldData, TNewData>
{
    public Func<TSource, CancellationToken, Task<TOldData>> ExtractExpression { get; private set; }

    public Func<TOldData, CancellationToken, Task<TNewData>> TransformExpression { get; private set; }

    public Func<TDestination, TNewData, CancellationToken, Task> LoadExpression { get; private set; }

    public IETLBasePipelineProvider<TSource, TDestination> Provider { get; private set; }

    private EtlTransformedPipe(
        Func<TSource, CancellationToken, Task<TOldData>> extractExpression,
        Func<TOldData, CancellationToken, Task<TNewData>> transformExpression,
        IETLBasePipelineProvider<TSource, TDestination> provider)
    {
        ExtractExpression = extractExpression;
        TransformExpression = transformExpression;
        Provider = provider;
    }

    public static EtlTransformedPipe<TSource, TDestination, TOldData, TNewData> FromBasePipe<TNewData>(
        IETLPipe<TSource, TDestination, TOldData> basePipe,
        Func<TOldData, CancellationToken, Task<TNewData>> transform)
    {
        return new EtlTransformedPipe<TSource, TDestination, TOldData, TNewData>(
            basePipe.ExtractExpression,
            transform,
            basePipe.Provider);
    }

    public void AddLoadingLogic(Func<TDestination, TNewData, CancellationToken, Task> load)
    {
        LoadExpression = load;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TOldData data = await Provider.Extract(ExtractExpression, cancellationToken);

        TNewData newData = await Provider.Transform(TransformExpression, data, cancellationToken);

        await Provider.Load(LoadExpression, newData, cancellationToken);
    }
}
