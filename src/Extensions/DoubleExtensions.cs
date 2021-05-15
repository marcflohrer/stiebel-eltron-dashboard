using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace StiebelEltronApiServer.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToJson (this IEnumerable<double?> str) 
            => JsonConvert.SerializeObject(str);       
        public static string ToJson (this IEnumerable<double> str) 
            => JsonConvert.SerializeObject(str);        

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }                            
    }
}