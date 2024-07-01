using System.Linq.Expressions;

namespace ETL.Provider.Pipelines;

public interface IETLExtractingPipeNode<TSource, TData>
{
    Func<TSource, CancellationToken, Task<TData>> ExtractExpression { get; }
}
