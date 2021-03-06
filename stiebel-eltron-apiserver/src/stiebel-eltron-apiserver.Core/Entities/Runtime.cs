using System;
namespace stiebeleltronapiserver.Core.Entities
{
    public record Runtime
    {
        public TimeSpan CompressorHeatingMode;
        public TimeSpan CompressorHotWater;
        public TimeSpan CompressorDefrost;
        public TimeSpan EmergencyAuxiliaryHeating1;
        public TimeSpan EmergencyAuxiliaryHeating2;
        public TimeSpan EmergencyAuxiliaryHeating12;
        public TimeSpan Defrost;
        public int DefrostCount;
    }
}
