using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static StiebelEltronApiServer.Services.Tags;

namespace StiebelEltronApiServer.Services
{
    public static class TagMismatchDetector
    {
        public static ParseResult DetectUnmatchedTags(Stack<TagContext> tagStack, Match match, int currentPosition, in IList<SubStringIndices> unopenedTags, in IList<UnclosedTag> unclosedTags)
        {
            var tagName = HtmlParser.ClosingTagNameRegex.Match(match.Value);

            var tag = tagName.Value;
            var recentOpenTag = tagStack.Peek();
            if (tag == recentOpenTag.tag)
            {
                tagStack.Pop();
            }
            else if (tag != recentOpenTag.tag)
            {
                var tagList = tagStack.ToArray();
                var tempTagStack = new Stack<TagContext>(tagList);
                var foundOpeningTag = false;
                var endPosition = 0;
                TagContext top;
                while (tempTagStack.Count > 0)
                {
                    top = tempTagStack.Peek();
                    if (tag == top.tag)
                    {
                        foundOpeningTag = true;
                        break;
                    }
                    tempTagStack.Pop();
                }
                endPosition = currentPosition + match.Value.Length - 1;

                if (!foundOpeningTag)
                {
                    Console.WriteLine("[DEBUG] Found unopened tag at position: " + currentPosition + ", end position: " + endPosition);
                    unopenedTags.Add(new SubStringIndices(currentPosition, endPosition));
                }
                else
                {
                    Console.WriteLine("[DEBUG] Found unclosed tag at position: " + currentPosition + ", end position: " + endPosition);
                    unclosedTags.Add(new UnclosedTag(recentOpenTag.tag, currentPosition));
                }
            }
            return new ParseResult(unclosedTags, unopenedTags);
        }
    }
}