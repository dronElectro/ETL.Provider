using ETL.Database.ExtractingStream;
using ETL.Database.Pipelines;
using System.Linq.Expressions;

namespace ETL.Provider.Pipelines;

public sealed class ETLPipe<TSource, TDestination, TData> : IETLPipe<TSource, TDestination, TData>
{
    public Func<TSource, CancellationToken, Task<TData>> ExtractExpression { get; private set; }

    public Func<TDestination, TData, CancellationToken, Task> LoadExpression { get; private set; }

    public IETLBasePipelineProvider<TSource, TDestination> Provider { get; private set; }

    public ETLPipe(
        IETLBasePipelineProvider<TSource, TDestination> provider,
        Func<TSource, CancellationToken, Task<TData>> extract)
    {
        Provider = provider;
        ExtractExpression = extract;
    }

    public void AddLoadingLogic(Func<TDestination, TData, CancellationToken, Task> load)
    {
        LoadExpression = load;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TData data = await Provider.Extract(ExtractExpression, cancellationToken);

        await Provider.Load(LoadExpression, data, cancellationToken);
    }
}
