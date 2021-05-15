namespace StiebelEltronDashboard.Services
{
    public interface IUnitService
    {
        public double GetBaseUnitValue((double value, string unit) input);
    }
}