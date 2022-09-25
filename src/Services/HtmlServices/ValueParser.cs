namespace StiebelEltronDashboard.Services.HtmlServices
{
    using System.Globalization;
    using System.Text;

    public class ValueParser : IValueParser
    {
        public (double Value, string Unit) GetValueWithUnit(string rawValue)
        {
            var isNegative = rawValue.StartsWith("-");
            if (isNegative)
            {
                rawValue = rawValue.Substring(1, rawValue.Length - 1);
            }

            var sourceSeparator = SourceDecimalSeparator(rawValue);
            var targetSeparator = TargetDecimalSeparator();
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
            var result = ParseDouble(number.ToString());
            if (isNegative)
            {
                result *= -1;
            }
            return (result, unit.ToString());
        }

        public static double ParseDouble(string value)
            => double.Parse(value, NumberStyles.AllowDecimalPoint, NumberFormatInfo.InvariantInfo);

        private static char SourceDecimalSeparator(string rawValue)
        {
            var comma = NumberDecimalSeparatorComma();
            var dot = TargetDecimalSeparator();
            var detectedDecimalSeparator = dot;
            if (rawValue.Contains(comma))
            {
                detectedDecimalSeparator = comma;
            }
            return detectedDecimalSeparator;
        }

        private static char NumberDecimalSeparatorComma() => ToChar(new CultureInfo("de-DE").NumberFormat.NumberDecimalSeparator);
        private static char TargetDecimalSeparator() => ToChar(new CultureInfo("en-GB").NumberFormat.NumberDecimalSeparator);

        // Convert.ToChar would return a unicode char that is not needed here.
        private static char ToChar(string s) => s[0];
    }
}
