using System.Collections.Generic;
using HtmlAgilityPack;

namespace StiebelEltronApiServer.Services {
    public class XpathService : IXpathService {
        public string GetValueFor (HtmlDocument htmlDocument, ScrapingValue scrapingValue) {
            return htmlDocument.DocumentNode
                .SelectSingleNode (xpathOfElement[scrapingValue]) ?
                .InnerText;
        }

        private readonly IDictionary<ScrapingValue, string> xpathOfElement = new Dictionary<ScrapingValue, string> () {
            {
            ScrapingValue.TotalPowerConsumption, "/html[1]/body[1]/div[2]/div[1]/form[1]/div[1]/div[2]/table[1]/tr[3]/td[2]"
            }, {
            // RÜCKLAUFTEMPERATUR °C
            ScrapingValue.ReturnTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[1]/td[2]"
            }, {
            //VORLAUFTEMPERATUR
            ScrapingValue.InletTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[2]/td[2]"
            }, {
            //FROSTSCHUTZTEMPERATUR °C 
            ScrapingValue.AntiFreezeTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[3]/td[2]"
            }, {
            //AUSSENTEMPERATUR °C 
            ScrapingValue.OutdoorTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[4]/td[2]"
            }, {
            //FORTLUFTTEMPERATUR °C 
            ScrapingValue.ExhaustAirTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[5]/td[2]"
            }, {
            //VERDAMPFERTEMPERATUR °C 
            ScrapingValue.EvaporatorTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[6]/td[2]"
            }, {
            //VERDICHTEREINTRITTSTEMPERATUR °C 
            ScrapingValue.CompressorInletTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[7]/td[2]"
            }, {
            //ZWISCHENEINSPRITZUNGSTEMP °C
            ScrapingValue.IntermediateInjectionTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[8]/td[2]"
            }, {
            //HEISSGASTEMPERATUR °C 
            ScrapingValue.HotGasTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[9]/td[2]"
            //
            }, {
            //VERFLÜSSIGERTEMPERATUR °C 
            ScrapingValue.CondenserTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[10]/td[2]"
            //
            }, {
            //ÖLSUMPFTEMPERATUR °C 
            ScrapingValue.OilSumpTemperature,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[11]/td[2]"
            //
            }, {
            //DRUCK NIEDERDRUCK bar 
            ScrapingValue.LowPressure,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[12]/td[2]"
            //
            }, {
            //DRUCK MITTELDRUCK bar 
            ScrapingValue.PressureMedium,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[13]/td[2]"
            //
            }, {
            //DRUCK HOCHDRUCK bar 
            ScrapingValue.HighPressure,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[14]/td[2]"
            //
            }, {
            //WP WASSERVOLUMENSTROM l/min 
            ScrapingValue.WaterVolumeCurrent,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[15]/td[2]"
            //
            }, {
            //SPANNUNG INVERTER V 
            ScrapingValue.VoltageInverter,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[16]/td[2]"
            //
            }, {
            //ISTDREHZAHL VERDICHTER Hz 
            ScrapingValue.ActualSpeedDensifier,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[17]/td[2]"
            //
            }, {
            //SOLLDREHZAHL VERDICHTER Hz 
            ScrapingValue.SettingSpeedCompressed,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[18]/td[2]"
            //
            }, {
            //LÜFTERLEISTUNG REL % 
            ScrapingValue.FanPowerRel,
            "/html/body/div[2]/div/form/div/div[1]/table/tr[19]/td[2]"
            //
            }, {
            //VD HEIZEN TAG KWh 
            ScrapingValue.VaporizerHeatQuantityHeatingDay,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[1]/td[2]"
            //
            }, {
            //VD HEIZEN SUMME MWh 
            ScrapingValue.VaporizerHeatQuantityHeatingTotal,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[2]/td[2]"
            //
            }, {
            //VD WARMWASSER TAG KWh 
            ScrapingValue.VaporizerHeatQuantityHotWaterDay,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[3]/td[2]"
            //
            }, {
            //VD WARMWASSER SUMME MWh 
            ScrapingValue.VaporizerHeatQuantityHotWaterTotal,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[4]/td[2]"
            //
            }, {
            //NHZ HEIZEN SUMME  MWh 
            ScrapingValue.ReheatingStagesHeatQuantityHeatingSum,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[5]/td[2]"
            //
            }, {
            //NHZ WARMWASSER SUMME  MWh 
            ScrapingValue.ReheatingStagesHeatQuantityHotWaterTotal,
            "/html/body/div[2]/div/form/div/div[2]/table/tr[6]/td[2]"
            //
            }, {
            //VD HEIZEN TAG  KWh 
            ScrapingValue.PowerConsumptionHeatingDay,
            "/html/body/div[2]/div/form/div/div[3]/table/tr[1]/td[2]"
            //
            }, {
            //VD HEIZEN SUMME MWh 
            ScrapingValue.PowerConsumptionHeatingSum,
            "/html/body/div[2]/div/form/div/div[3]/table/tr[2]/td[2]"
            //
            }, {
            //VD WARMWASSER TAG KWh 
            ScrapingValue.PowerConsumptionHotWaterDay,
            "/html/body/div[2]/div/form/div/div[3]/table/tr[3]/td[2]"
            //
            }, {
            //VD WARMWASSER SUMME MWh 
            ScrapingValue.PowerConsumptionHotWaterSum,
            "/html/body/div[2]/div/form/div/div[3]/table/tr[4]/td[2]"
            //
            }, {
            //VD HEIZEN
            ScrapingValue.RuntimeVaporizerHeating,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[1]/td[2]"
            //
            }, {
            //VD WARMWASSER
            ScrapingValue.RuntimeVaporizerHotWater,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[2]/td[2]"
            //
            }, {
            //VD ABTAUEN
            ScrapingValue.RuntimeVaporizerDefrost,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[3]/td[2]"
            //
            }, {
            //NHZ 1
            ScrapingValue.ReheatingStages1,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[4]/td[2]"
            //
            }, {
            //NHZ 2
            ScrapingValue.ReheatingStages2,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[5]/td[2]"
            //
            }
            /*{
            //NHZ 1/2 
            }*/
            , {
            //ZEIT ABTAUEN
            ScrapingValue.DefrostTime,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[7]/td[2]"
            //
            }, {
            //STARTS ABTAUEN
            ScrapingValue.DefrostStarts,
            "/html/body/div[2]/div/form/div/div[4]/table/tr[8]/td[2]"
            }
        };
    }
}