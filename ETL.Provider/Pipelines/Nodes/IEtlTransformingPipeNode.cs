using System.Linq.Expressions;

namespace ETL.Provider.Pipelines;

public interface IEtlTransformingPipeNode<TOldData, TNewData>
{
    Func<TOldData, CancellationToken, Task<TNewData>> TransformExpression { get; }
}
