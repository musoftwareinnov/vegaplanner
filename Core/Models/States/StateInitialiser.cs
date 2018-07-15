using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using vega.Core.Models.Generic;
using vega.Core.Models.States;

namespace vega.Core.Models
{
    public class StateInitialiser : IdNameProperty
    {
        public ICollection<StateInitialiserState> States { get; set; }

        public StateInitialiser()
        {
            States = new Collection<StateInitialiserState>();

        }
    }
}