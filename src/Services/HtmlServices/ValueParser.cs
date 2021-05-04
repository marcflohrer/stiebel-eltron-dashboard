using System;
using System.Text;

namespace StiebelEltronApiServer.Services.HtmlServices
{
    public class ValueParser : IValueParser
    {
        public (double Value, string Unit) GetValueWithUnit(string rawValue)
        {
            var decimalValue = rawValue.Trim().Replace(',', '.');
            var number = new StringBuilder();
            var unit = new StringBuilder();
            foreach (var d in decimalValue)
            {
                var isNumeric = int.TryParse(d.ToString(), out _);
                var isDot = d == '.';
                if (isNumeric || isDot)
                {
                    number.Append(d);
                }
                else
                {
                    unit.Append(d);
                }
            }
            return (double.Parse(number.ToString()), unit.ToString());
        }
    }
}