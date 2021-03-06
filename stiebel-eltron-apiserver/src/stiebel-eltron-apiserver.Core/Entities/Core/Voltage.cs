namespace stiebeleltronapiserver.Core.Entities.Core
{
    public record Voltage
    {
        public double Value;
        public string Unit => "V";
    }
}
