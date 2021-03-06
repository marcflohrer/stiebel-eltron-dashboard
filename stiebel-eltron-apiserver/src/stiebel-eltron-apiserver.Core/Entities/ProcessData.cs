using stiebeleltronapiserver.Core.Entities.Core;

namespace stiebeleltronapiserver.Core.Entities
{
    public record ProcessData
    {
        /// <summary>
        /// RÜCKLAUFTEMPERATUR
        /// </summary>
        public Temperature ReturnTemperature;

        /// <summary>
        /// VORLAUFTEMPERATUR
        /// </summary>
        public Temperature InletTemperature;

        /// <summary>
        /// FORTLUFTTEMPERATUR
        /// </summary>
        public Temperature ExhaustAirTemperature;

        /// <summary>
        /// VERDAMPFERTEMPERATUR
        /// </summary>
        public Temperature EvaporatorTemperature;

        /// <summary>
        /// VERDICHTEREINTRITTSTEMPERATUR
        /// </summary>
        public Temperature CompressorInletTemperature;

        /// <summary>
        /// ZWISCHENEINSPRITZUNGSTEMPERATUR
        /// </summary>
        public Temperature IntermediateInjectionTemperature;

        /// <summary>
        /// HEISSGASTEMPERATUR
        /// </summary>
        public Temperature HotGasTemperature;

        /// <summary>
        /// VERFLÜSSIGERTEMPERATUR
        /// </summary>
        public Temperature CondenserTemperature;

        /// <summary>
        /// ÖLSUMPFTEMPERATUR
        /// </summary>
        public Temperature OilSumpTemperature;

        /// <summary>
        /// NIEDERDRUCK
        /// </summary>
        public Pressure LowPressure;

        /// <summary>
        /// MITTELDRUCK
        /// </summary>
        public Pressure MediumPressure;

        /// <summary>
        /// HOCHDRUCK
        /// </summary>
        public Pressure HighPressure;

        /// <summary>
        /// WASSERVOLUMENSTROM
        /// </summary>
        public VolumeFlow WaterVolumeFlow;

        /// <summary>
        /// SPANNUNG INVERTER
        /// </summary>
        public Voltage VoltageInverter;

        /// <summary>
        /// ISTDREHZAHL VERDICHTER
        /// </summary>
        public Frequency CompressorActualSpeed;

        /// <summary>
        /// SOLLDREHZAHL VERDICHTER
        /// </summary>
        public Frequency CompressorSetSpeed;

        /// <summary>
        /// LÜFTERLEISTUNG REL
        /// </summary>
        public Percentage FanPower;
    }
}
