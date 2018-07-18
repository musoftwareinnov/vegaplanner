using vega.Controllers.Resources;
using vega.Core.Models.States;

namespace vega.Core
{
    public interface IStateInitialiserStateRepository
    {
          void AddBeginning(StateInitialiserState stateInitialiserState);
          void AddAfter(StateInitialiserState stateInitialiserState, int InsertAfterStateOrderId);
    }
}