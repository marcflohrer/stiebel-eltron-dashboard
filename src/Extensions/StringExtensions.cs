using Newtonsoft.Json;
using System.Collections.Generic;

namespace StiebelEltronApiServer.Extensions
{
    public static class StringExtensions
    {
        public static string ToJson (this IEnumerable<string> str) 
            => JsonConvert.SerializeObject(str);
    }
}