namespace stiebeleltronapiserver.Core.Entities.Core
{
    public record Frequency
    {
        public double Value;
        public string Unit => "Hz";
    }
}
