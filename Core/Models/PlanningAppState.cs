using System;
using vega.Core.Models.Generic;
using vega.Core.Models.States;

namespace vega.Core.Models
{
    public class PlanningAppState
    {
        public int Id { get; set; }
        public int StateInitialiserStateId { get; set; }
        public StateInitialiserState state { get; set; }
        public DateTime DueByDate { get; set; }

        public DateTime? CompletionDate { get; set; }

        public int StateStatusId { get; set; }
        public StateStatus StateStatus { get; set; }
    }
}