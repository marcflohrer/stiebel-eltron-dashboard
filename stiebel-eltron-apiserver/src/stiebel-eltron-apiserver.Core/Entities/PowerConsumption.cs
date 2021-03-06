namespace stiebeleltronapiserver.Core.Entities
{
    public record PowerConsumption
    {
        /// <summary>
        /// Elektrische Leistung des Verdichters im Heizbetrieb seit 0:00 Uhr des aktuellen Tages.
        /// </summary>
        public double HeatingModeCurrentDay;

        /// <summary>
        /// Gesamtsumme der elektrischen Leistung des Verdichters im Heizbetrieb.
        /// </summary>
        public double HeatingModeTotal;

        /// <summary>
        /// Elektrische Leistung des Verdichters im Warmwasserbetrieb seit 0:00 Uhr des aktuellen Tages.
        /// </summary>
        public double HotWaterCurrentDay;

        /// <summary>
        /// Gesamtsumme der Elektrischen Leistung des Verdichters im Warmwasserbetrieb.
        /// </summary>
        public double HotWaterTotal;
    }
}
