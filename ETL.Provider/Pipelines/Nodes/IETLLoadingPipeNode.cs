using System.Linq.Expressions;

namespace ETL.Provider.Pipelines;

public interface IETLLoadingPipeNode<TDestination, TData>
{
    Func<TDestination, TData, CancellationToken, Task> LoadExpression { get; }

    void AddLoadingLogic(Func<TDestination, TData, CancellationToken, Task> load);

    Task StartAsync(CancellationToken cancellationToken);
}
