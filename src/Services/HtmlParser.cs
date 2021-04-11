using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static StiebelEltronApiServer.Services.Tags;

namespace StiebelEltronApiServer.Services
{
    public class HtmlParser : IHtmlParser
    {
        public ScanResult ParseTagTree(string dirtyHtml, MatchCollection tags){
            var currentPosition = 0;
            
            var openingTagNameRegex = new Regex("(?<=<)[A-z]+");
            var closingTagNameRegex = new Regex("(?<=</)[A-z]+");
            var tagIdRegex = new Regex("(?<= [A-z]+=\")[A-z ]+(?=\")");

            var tagStack = new Stack<TagContext>();
            var unopenedTags = new List<SubStringIndices>();

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
                    var tagName = closingTagNameRegex.Match(match.Value);
                    // skip <?xml tag
                    if (string.IsNullOrEmpty(tagName.Value))
                    {
                        continue;
                    }
                    var tag = tagName.Value;
                    var recentOpenTag = tagStack.Peek().tag;
                    if (tag == recentOpenTag)
                    {
                        tagStack.Pop();
                    }
                    else if (tag != recentOpenTag)
                    {
                        var endPosition = currentPosition + match.Value.Length - 1;
                        Console.WriteLine("Found unopened tag at position: " + currentPosition + ", end position: " + endPosition);
                        unopenedTags.Add(new SubStringIndices(currentPosition, endPosition));
                    }
                }
                else if (match.Value.StartsWith("<"))
                {
                    var tagName = openingTagNameRegex.Match(match.Value);
                    // skip <?xml tag
                    if (string.IsNullOrEmpty(tagName.Value))
                    {
                        continue;
                    }
                    if (!Tags.TagMap.ContainsKey(tagName.Value))
                    {
                        Console.WriteLine("Custom tag detected: " + tagName.Value);
                    }
                    var tag = tagName.Value;
                    var tagId = tagIdRegex.Match(match.Value);
                    tagStack.Push(new TagContext(tag, tagId.Value));
                }
            }

            return new ScanResult(tagStack, unopenedTags);
        }
    }
    public record ScanResult(Stack<TagContext> tagStack, IReadOnlyList<SubStringIndices> unopenedTags);
}