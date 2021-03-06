namespace stiebeleltronapiserver.Core.Entities.Core
{
    public record Temperature
    {
        public double Value;
        public string Unit => "°C";
    }
}
