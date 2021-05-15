using System.Collections.Generic;
using HtmlAgilityPack;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public class XpathService : IXpathService
    {
        public string GetValueFor(HtmlDocument htmlDocument, Metric scrapingValue)
        {
            return htmlDocument.DocumentNode
                .SelectSingleNode(xpathOfElement[scrapingValue])?
                .InnerText;
        }

        private readonly IDictionary<Metric, string> xpathOfElement = new Dictionary<Metric, string>() {
            {
            // VD HEIZEN SUMME MWh   
            Metric.TotalPowerConsumption, "/html[1]/body[1]/div[2]/div[1]/form[1]/div[1]/div[2]/table[1]/tr[3]/td[2]"
            }, {
            // RÜCKLAUFTEMPERATUR °C
            Metric.ReturnTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[2]/td[2]"
            }, {
            //VORLAUFTEMPERATUR
            Metric.InletTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[3]/td[2]"
            }, {
            //FROSTSCHUTZTEMPERATUR °C 
            Metric.AntiFreezeTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[4]/td[2]"
            }, {
            //AUSSENTEMPERATUR °C 
            Metric.OutdoorTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[5]/td[2]"
            }, {
            //FORTLUFTTEMPERATUR °C 
            Metric.ExhaustAirTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[6]/td[2]"
            }, {
            //VERDAMPFERTEMPERATUR °C 
            Metric.EvaporatorTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[7]/td[2]"
            }, {
            //VERDICHTEREINTRITTSTEMPERATUR °C 
            Metric.CompressorInletTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[8]/td[2]"
            }, {
            //ZWISCHENEINSPRITZUNGSTEMP °C
            Metric.IntermediateInjectionTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[9]/td[2]"
            }, {
            //HEISSGASTEMPERATUR °C 
            Metric.HotGasTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[10]/td[2]"
            //
            }, {
            //VERFLÜSSIGERTEMPERATUR °C 
            Metric.CondenserTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[11]/td[2]"
            //
            }, {
            //ÖLSUMPFTEMPERATUR °C 
            Metric.OilSumpTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[12]/td[2]"
            //
            }, {
            //DRUCK NIEDERDRUCK bar 
            Metric.LowPressure,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[13]/td[2]"
            //
            }, {
            //DRUCK MITTELDRUCK bar 
            Metric.PressureMedium,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[14]/td[2]"
            //
            }, {
            //DRUCK HOCHDRUCK bar 
            Metric.HighPressure,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[15]/td[2]"
            //
            }, {
            //WP WASSERVOLUMENSTROM l/min 
            Metric.WaterVolumeCurrent,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[16]/td[2]"
            //
            }, {
            //SPANNUNG INVERTER V 
            Metric.VoltageInverter,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[17]/td[2]"
            //
            }, {
            //ISTDREHZAHL VERDICHTER Hz 
            Metric.ActualSpeedDensifier,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[18]/td[2]"
            //
            }, {
            //SOLLDREHZAHL VERDICHTER Hz 
            Metric.SettingSpeedCompressed,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[19]/td[2]"
            //
            }, {
            //LÜFTERLEISTUNG REL % 
            Metric.FanPowerRel,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[20]/td[2]"
            //
            }, {
            //VD HEIZEN TAG KWh 
            Metric.VaporizerHeatQuantityHeatingDay,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[2]/td[2]"
            //
            }, {
            //VD HEIZEN SUMME MWh 
            Metric.VaporizerHeatQuantityHeatingTotal,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[3]/td[2]"
            //
            }, {
            //VD WARMWASSER TAG KWh 
            Metric.VaporizerHeatQuantityHotWaterDay,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[4]/td[2]"
            //
            }, {
            //VD WARMWASSER SUMME MWh 
            Metric.VaporizerHeatQuantityHotWaterTotal,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[5]/td[2]"
            //
            }, {
            //NHZ HEIZEN SUMME  MWh 
            Metric.ReheatingStagesHeatQuantityHeatingSum,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[6]/td[2]"
            //
            }, {
            //NHZ WARMWASSER SUMME  MWh 
            Metric.ReheatingStagesHeatQuantityHotWaterTotal,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[7]/td[2]"
            //
            }, {
            //VD HEIZEN TAG  KWh 
            Metric.PowerConsumptionHeatingDay,
            "/html/body/div[2]/div/form/div/div[3]/table/tr[2]/td[2]"
            //
            }, {
            //VD HEIZEN SUMME MWh 
            Metric.PowerConsumptionHeatingSum,
            "/html/body/div[2]/div/form/div/div[3]/table/tr[3]/td[2]"
            //
            }, {
            //VD WARMWASSER TAG KWh 
            Metric.PowerConsumptionHotWaterDay,
            "/html/body/div[2]/div/form/div/div[3]/table/tr[4]/td[2]"
            //
            }, {
            //VD WARMWASSER SUMME MWh 
            Metric.PowerConsumptionHotWaterSum,
            "/html/body/div[2]/div/form/div/div[3]/table/tr[5]/td[2]"
            //
            }, {
            //VD HEIZEN
            Metric.RuntimeVaporizerHeating,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[2]/td[2]"
            //
            }, {
            //VD WARMWASSER
            Metric.RuntimeVaporizerHotWater,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[3]/td[2]"
            //
            }, {
            //VD ABTAUEN
            Metric.RuntimeVaporizerDefrost,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[4]/td[2]"
            //
            }, {
            //NHZ 1
            Metric.ReheatingStages1,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[5]/td[2]"
            //
            }, {
            //NHZ 2
            Metric.ReheatingStages2,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[6]/td[2]"
            //
            }
            /*{
            //NHZ 1/2 
            }*/
            , {
            //ZEIT ABTAUEN
            Metric.DefrostTime,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[8]/td[2]"
            //
            }, {
            //STARTS ABTAUEN
            Metric.DefrostStarts,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[9]/td[2]"
            }
        };
    }
}