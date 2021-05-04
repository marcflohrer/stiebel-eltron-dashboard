using System.Collections.Generic;
using System.Text.RegularExpressions;
using static StiebelEltronApiServer.Services.HtmlServices.Tags;

namespace StiebelEltronApiServer.Services.HtmlServices
{
    public class HtmlScanner : IHtmlScanner
    {
        private readonly Regex openingTagNameRegex = new Regex("(?<=<)[A-z]+");
        public static readonly Regex ClosingTagNameRegex = new Regex("(?<=</)[A-z]+");
        private readonly Regex tagIdRegex = new Regex("(?<= [A-z]+=\")[A-z ]+(?=\")");

        public ParseResult ParseTagTree(string dirtyHtml, MatchCollection tags)
        {
            var currentPosition = 0;
            var tagStack = new Stack<TagContext>();
            var unopenedTags = new List<SubStringIndices>() as IList<SubStringIndices>;
            var unclosedTags = new List<UnclosedTag>() as IList<UnclosedTag>;

            foreach (Match match in tags)
            {
                currentPosition = match.Index;
                // skip self-closing tags
                if (match.Value.EndsWith("/>"))
                {
                    continue;
                }
                // skip comments
                if (match.Value.StartsWith("<!--"))
                {
                    continue;
                }
                if (match.Value.StartsWith("</"))
                {
                    var tagName = ClosingTagNameRegex.Match(match.Value);
                    // skip <?xml tag
                    if (string.IsNullOrEmpty(tagName.Value))
                    {
                        continue;
                    }
                    var tempParseResult = TagMismatchDetector.DetectUnmatchedTags(tagStack, match, currentPosition, unopenedTags, unclosedTags);
                    unopenedTags = tempParseResult.unopenedTags;
                    unclosedTags = tempParseResult.unclosedTags;
                }
                else if (match.Value.StartsWith("<"))
                {
                    var tagName = openingTagNameRegex.Match(match.Value);
                    // skip <?xml tag
                    if (string.IsNullOrEmpty(tagName.Value))
                    {
                        continue;
                    }
                    tagStack.Push(new TagContext(tagName.Value, tagIdRegex.Match(match.Value)));
                }
            }

            return new ParseResult(unclosedTags, unopenedTags);
        }


    }
    public record ParseResult(IList<UnclosedTag> unclosedTags, IList<SubStringIndices> unopenedTags);
}