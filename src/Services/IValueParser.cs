namespace StiebelEltronApiServer.Services
{
    public interface IValueParser
    {
        public (double Value, string Unit) GetValueWithUnit(string rawValue);
    }
}