using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static StiebelEltronApiServer.Services.Tags;

namespace StiebelEltronApiServer.Services {
    public class TidyUpDirtyHtml : ITidyUpDirtyHtml {
        private readonly IHtmlParser _htmlParser;
        public TidyUpDirtyHtml (IHtmlParser htmlParser) {
            _htmlParser = htmlParser;
        }
        private MatchCollection GetMatches (string input, string pattern) {
            var devTagRegex = new Regex (pattern);
            return devTagRegex.Matches (input);
        }

        public string GetTidyHtml (string dirtyHtml) {
            var tags = GetMatches (dirtyHtml, "<[A-z \"=\\/:.0-9%#!?;,)_(-]+[/]*>");

            var scanResult = _htmlParser.ParseTagTree (dirtyHtml, tags);
            var tagStack = scanResult.tagStack;
            var unopenedTags = scanResult.unopenedTags;

            var dirtyHtmlCharArray = dirtyHtml.ToCharArray ();
            dirtyHtml = RemoveUnopenedTags (dirtyHtmlCharArray, unopenedTags);
            dirtyHtml = CloseUnclosedTags (dirtyHtml, tagStack);

            dirtyHtml = dirtyHtml.Replace ("&nbsp;", string.Empty);
            return dirtyHtml.Replace ("&copy;", string.Empty);
        }

        private static string RemoveUnopenedTags (char[] dirtyHtmlCharArray, IReadOnlyList<SubStringIndices> unopenedTags) {
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