namespace stiebeleltronapiserver.Core.Entities.Core
{
    public record VolumeFlow
    {
        public double Value;
        public string Unit => "l/min";
    }
}
