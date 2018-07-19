namespace vega.Controllers.Resources
{
    public class SaveStateInitialiserStateResource
    {
        public int StateInitialiserId { get; set; }
        public string Name { get; set; }

        public int CompletionTime { get; set; }

        public int AlertToCompletionTime { get; set; }
        public int InsertAfterStateOrderId { get; set; }

    }
}