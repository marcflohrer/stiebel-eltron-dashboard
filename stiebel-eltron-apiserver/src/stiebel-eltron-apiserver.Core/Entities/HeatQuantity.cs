using stiebeleltronapiserver.Core.Entities.Core;

namespace stiebeleltronapiserver.Core.Entities
{
    public record HeatQuantity
    {
        /// <summary>
        /// Wärmemenge des Verdichters im Heizbetrieb seit 0:00 Uhr des aktuellen Tages.
        /// </summary>
        public Energy CompressorHeatingModeCurrentDay;

        /// <summary>
        /// Gesamtsumme der Wärmemenge des Verdichters im Heizbetrieb.
        /// </summary>
        public Energy CompressorHeatingModeTotal;

        /// <summary>
        /// Wärmemenge des Verdichters im Warmwasserbetrieb seit 0:00 Uhr des aktuellen Tages.
        /// </summary>
        public Energy CompressorHotWaterCurrentDay;

        /// <summary>
        /// Gesamtsumme der Wärmemenge des Verdichters im Warmwasserbetrieb
        /// </summary>
        public Energy CompressorHotWaterTotal;

        /// <summary>
        /// Gesamtsumme der Wärmemenge der Nachheizstufen im Heizbe-trieb.
        /// </summary>
        public Energy CompressorHeatingModeReheatingTotal;

        /// <summary>
        /// Gesamtsumme der Wärmemenge der Nachheizstufen im Warmwasserbetrieb.
        /// </summary>
        public Energy CompressorHotWaterReheatingTotal;
    }
}
