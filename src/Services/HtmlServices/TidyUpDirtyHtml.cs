using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static StiebelEltronDashboard.Services.HtmlServices.Tags;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public class TidyUpDirtyHtml : ITidyUpDirtyHtml
    {
        private readonly IHtmlScanner _htmlParser;
        public TidyUpDirtyHtml(IHtmlScanner htmlParser)
        {
            _htmlParser = htmlParser;
        }

        private IList<SubStringIndices> GetMatches(string input, string pattern)
        {
            var tagRegex = new Regex(pattern);
            var matches = tagRegex.Matches(input);
            var tags = new List<SubStringIndices>();

            foreach (Match match in matches)
            {
                var start = match.Index;
                var end = start + match.Length - 1;
                var tag = match.Value;
                tags.Add(new SubStringIndices(start, end, tag));
            }

            return tags;
        }

        public string GetTidyHtml(string dirtyHtml)
        {
            // Parse the input string for all HTML tags
            var tags = ParseTags(dirtyHtml);

            // Parse the tag tree and get a list of unopened and unclosed tags
            var parseResult = _htmlParser.ParseTagTree(dirtyHtml, tags);
            var unopenedTags = parseResult.unopenedTags;
            var unclosedTags = parseResult.unclosedTags;

            // Remove all unopened tags from the input string
            dirtyHtml = RemoveUnopenedTags(dirtyHtml.ToCharArray(), unopenedTags);

            // Close all unclosed tags in the input string
            dirtyHtml = CloseUnclosedTags(dirtyHtml, unclosedTags);

            // Replace &nbsp; and &copy; characters with empty strings
            var tidyHtml = dirtyHtml.Replace("&nbsp;", string.Empty).Replace("&copy;", string.Empty);

            return tidyHtml;
        }

        private IList<SubStringIndices> ParseTags(string input)
        {
            var tags = new List<SubStringIndices>();
            var matches = Regex.Matches(input, "<[A-z \"=\\/:.0-9%#!?;,)_(-]+[/]*>");
            foreach (Match match in matches)
            {
                var tagStart = match.Index;
                var tagEnd = tagStart + match.Length - 1;
                tags.Add(new SubStringIndices(tagStart, tagEnd, match.Value));
            }
            return tags;
        }

        private static string RemoveUnopenedTags(char[] dirtyHtmlCharArray, IList<SubStringIndices> unopenedTags)
        {
            foreach (var unopenedTag in unopenedTags)
            {
                var i = unopenedTag.start;
                do
                {
                    dirtyHtmlCharArray[i] = ' ';
                }
                while (i++ < unopenedTag.end);
            }
            return new string(dirtyHtmlCharArray);
        }

        public string CloseUnclosedTags(string dirtyHtml, IList<UnclosedTag> unclosedTags)
        {
            foreach (var unclosedTag in unclosedTags)
            {
                dirtyHtml = dirtyHtml.Insert(unclosedTag.position, $"</{unclosedTag.tag}>");
            }

            return dirtyHtml;
        }
    }

    public record SubStringIndices(int start, int end, string tag);
}
