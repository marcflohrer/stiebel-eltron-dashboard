using System;
using System.IO;

namespace StiebelEltronApiServerTests
{
    public static class ServiceWeltMockData
    {
        public static string HeatPumpWebsite => File.ReadAllText(@"html/HeatPumpWebsite.html");
        public static string HeatPumpWebsiteTidiedUp => File.ReadAllText(@"html/HeatPumpWebsiteTidiedUp.html");
        public static string LoginWebSite => File.ReadAllText(@"html/LoginWebsite.html");

        public static string GetHtml(int testDataIndex)
        {
            int maxIndex = 16;
            if(testDataIndex > 0 && testDataIndex <= maxIndex)
            {
                return File.ReadAllText(@"html/TestSnippet" + testDataIndex + ".html");
            }
            else
            {
                throw new IndexOutOfRangeException("Test Data Index must be in [1,"+maxIndex+"]. Index found: " + testDataIndex);
            }
        }
    }
}
