using System.Collections.Generic;
using System.Threading.Tasks;
using vega.Core.Models;

namespace vega.Core
{
    public interface IStateInitialiserRepository
    {
          Task<StateInitialiser> GetStateInitialiser(int id);

        Task<ICollection<StateInitialiser>> GetStateInitialisers();

        //   void Add(StateInitialiser StateInitialiser);

        //   void Remove(StateInitialiser StateInitialiser);
    }
}