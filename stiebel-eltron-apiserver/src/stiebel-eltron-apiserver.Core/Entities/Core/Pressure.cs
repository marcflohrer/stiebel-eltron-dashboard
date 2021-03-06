namespace stiebeleltronapiserver.Core.Entities.Core
{
    public record Pressure
    {
        public double Value;
        public string Unit => "bar";
    }
}
