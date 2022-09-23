using System;
using System.Text;
using System.Threading;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public class ValueParser : IValueParser
    {
        public (double Value, string Unit) GetValueWithUnit(string rawValue)
        {
            var isNegative = rawValue.StartsWith("-");  
            if(isNegative)
            {
                rawValue = rawValue.Substring(1,rawValue.Length-1);
            }

            var sourceSeparator = ',';
            var targetSeparator = Convert.ToChar(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator);
            var decimalValue = rawValue.Trim().Replace(sourceSeparator, targetSeparator);
            var number = new StringBuilder();
            var unit = new StringBuilder();
                      
            foreach (var d in decimalValue)
            {
                var isNumeric = int.TryParse(d.ToString(), out _);
                var isDot = d == targetSeparator;
                if (isNumeric || isDot)
                {
                    number.Append(d);
                }
                else
                {
                    unit.Append(d);
                }
            }
            var result = double.Parse(number.ToString());
            if(isNegative){
                result *= -1;
            }
            return (result, unit.ToString());
        }
    }
}
