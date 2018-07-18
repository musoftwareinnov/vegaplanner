namespace vega.Controllers.Resources
{

    public class StateInitialiserStateResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OrderId { get; set; }  
        public int CompletionTime { get; set; }          //Days
        public int AlertToCompletionTime { get; set; }   //Days
        public int StateInitialiserId { get; set; }

    }
}