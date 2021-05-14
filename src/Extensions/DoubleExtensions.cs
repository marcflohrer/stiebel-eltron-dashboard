using Newtonsoft.Json;
using System.Collections.Generic;

namespace StiebelEltronApiServer.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToJson (this IEnumerable<double?> str) 
            => JsonConvert.SerializeObject(str);       
        public static string ToJson (this IEnumerable<double> str) 
            => JsonConvert.SerializeObject(str);                                
    }
}