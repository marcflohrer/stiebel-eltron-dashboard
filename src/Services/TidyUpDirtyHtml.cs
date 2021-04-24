using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static StiebelEltronApiServer.Services.Tags;

namespace StiebelEltronApiServer.Services {
    public class TidyUpDirtyHtml : ITidyUpDirtyHtml {
        private readonly IHtmlScanner _htmlParser;
        public TidyUpDirtyHtml (IHtmlScanner htmlParser) {
            _htmlParser = htmlParser;
        }
        private MatchCollection GetMatches (string input, string pattern) {
            var devTagRegex = new Regex (pattern);
            return devTagRegex.Matches (input);
        }

        public string GetTidyHtml (string dirtyHtml) {
            var tags = GetMatches (dirtyHtml, "<[A-z \"=\\/:.0-9%#!?;,)_(-]+[/]*>");

            var scanResult = _htmlParser.ParseTagTree (dirtyHtml, tags);
            var unclosedTags = scanResult.unclosedTags;
            var unopenedTags = scanResult.unopenedTags;

            var tidierHtml = dirtyHtml;
            var fragments = new List<string>();
            foreach(var unclosedTag in unclosedTags){
                fragments.Add(tidierHtml.Substring(0, unclosedTag.index));
                fragments.Add($"</{unclosedTag.tag}>");
                fragments.Add(tidierHtml.Substring(unclosedTag.index, tidierHtml.Length - unclosedTag.index));
            }
            if(!fragments.Any()){
                fragments.Add(tidierHtml);
            }
            var fragmentsCombined = string.Empty;
            foreach(string fragment in fragments){
                fragmentsCombined += fragment;
            }
            tidierHtml = fragmentsCombined;

            var tidierHtmlCharArray = tidierHtml.ToCharArray ();            
            dirtyHtml = RemoveUnopenedTags (tidierHtmlCharArray, unopenedTags);
            
            var tidyHtml = new string (tidierHtmlCharArray).Replace ("&nbsp;", string.Empty);
            return tidyHtml.Replace ("&copy;", string.Empty);
        }

        private static string RemoveUnopenedTags (char[] dirtyHtmlCharArray, IList<SubStringIndices> unopenedTags) {
            foreach (var unopenedTag in unopenedTags) {
                var i = unopenedTag.start;
                do {
                    dirtyHtmlCharArray[i] = ' ';
                }
                while (i++ < unopenedTag.end);
            }
            return new string (dirtyHtmlCharArray);
        }

        private static string CloseUnclosedTags (string dirtyHtml, Stack<TagContext> tagStack) {
            while (tagStack.TryPop (out var unclosedTag)) {
                dirtyHtml += "</" + unclosedTag + ">";
            }

            return dirtyHtml;
        }
    }
    
    public record SubStringIndices (int start, double end);

}