namespace stiebeleltronapiserver.Core.Entities.Core
{
    public record Percentage
    {
        public double Value;
        public string Unit => "%";
    }
}
