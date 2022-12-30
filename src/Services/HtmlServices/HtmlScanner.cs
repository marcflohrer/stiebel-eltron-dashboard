using System.Collections.Generic;
using System.Text.RegularExpressions;
using static StiebelEltronDashboard.Services.HtmlServices.Tags;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public class HtmlScanner : IHtmlScanner
    {
        private readonly Regex openingTagNameRegex = new Regex("(?<=<)[A-z]+");
        public static readonly Regex ClosingTagNameRegex = new Regex("(?<=</)[A-z]+");
        private readonly Regex tagIdRegex = new Regex("(?<= [A-z]+=\")[A-z ]+(?=\")");

        public ParseResult ParseTagTree(string dirtyHtml, IList<SubStringIndices> tags)
        {
            var currentPosition = 0;
            var tagStack = new Stack<TagContext>();
            var unopenedTags = new List<SubStringIndices>() as IList<SubStringIndices>;
            var unclosedTags = new List<UnclosedTag>() as IList<UnclosedTag>;

            foreach (var tag in tags)
            {
                currentPosition = tag.start;
                // skip self-closing tags
                if (tag.tag.EndsWith("/>"))
                {
                    continue;
                }
                // skip comments
                if (tag.tag.StartsWith("<!--"))
                {
                    continue;
                }
                if (tag.tag.StartsWith("</"))
                {
                    var tagName = ClosingTagNameRegex.Match(tag.tag);
                    // skip <?xml tag
                    if (string.IsNullOrEmpty(tagName.Value))
                    {
                        continue;
                    }
                    var tempParseResult = TagMismatchDetector.DetectUnmatchedTags(tagStack, tag, currentPosition, unopenedTags, unclosedTags);
                    unopenedTags = tempParseResult.unopenedTags;
                    unclosedTags = tempParseResult.unclosedTags;
                }
                else if (tag.tag.StartsWith("<"))
                {
                    var tagName = openingTagNameRegex.Match(tag.tag);
                    // skip <?xml tag
                    if (string.IsNullOrEmpty(tagName.Value))
                    {
                        continue;
                    }
                    tagStack.Push(new TagContext(tagName.Value, tagIdRegex.Match(tag.tag)));
                }
            }

            return new ParseResult(unclosedTags, unopenedTags);
        }
    }
    public record ParseResult(IList<UnclosedTag> unclosedTags, IList<SubStringIndices> unopenedTags);
}