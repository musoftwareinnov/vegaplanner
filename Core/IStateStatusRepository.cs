using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;
using vega.Core.Models.States;

namespace vega.Core
{
    public interface IStateStatusRepository
    {
        Task<List<StateStatus>> GetStateStatusList ();
    }
}