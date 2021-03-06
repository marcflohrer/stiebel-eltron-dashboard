namespace stiebeleltronapiserver.Core.Entities
{
    public record HeatPump
    {
        public HeatQuantity HeatQuantity;
        public PowerConsumption PowerConsumption;
        public ProcessData ProcessData;
        public Runtime Runtime;
        public Starts Starts;
    }
}
