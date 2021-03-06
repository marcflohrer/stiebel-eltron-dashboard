namespace stiebeleltronapiserver.Core.Entities.Core
{
    public record Energy
    {
        public double Value;
        public string Unit => "kWh";
    }
}
