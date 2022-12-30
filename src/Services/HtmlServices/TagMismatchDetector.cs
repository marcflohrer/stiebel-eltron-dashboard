using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static StiebelEltronDashboard.Services.HtmlServices.Tags;
using Serilog;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public static class TagMismatchDetector
    {
        public static ParseResult DetectUnmatchedTags(Stack<TagContext> tagStack,
            SubStringIndices tag,
            int currentPosition,
            IList<SubStringIndices> unopenedTags,
            IList<UnclosedTag> unclosedTags)
        {
            var tagName = HtmlScanner.ClosingTagNameRegex.Match(tag.tag);

            var recentOpenTag = tagStack.Peek();
            if (tagName.Value == recentOpenTag.tag)
            {
                tagStack.Pop();
            }
            else if (tagName.Value != recentOpenTag.tag)
            {
                var tagList = tagStack.ToArray();
                var tempTagStack = new Stack<TagContext>(tagList);
                var foundOpeningTag = false;
                TagContext top;
                while (tempTagStack.Count > 0)
                {
                    top = tempTagStack.Peek();
                    if (tagName.Value == top.tag)
                    {
                        foundOpeningTag = true;
                        break;
                    }
                    tempTagStack.Pop();
                }

                if (!foundOpeningTag)
                {
                    unopenedTags.Add(tag);
                }
                else
                {
                    unclosedTags.Add(new UnclosedTag(recentOpenTag.tag, currentPosition));
                }
            }
            return new ParseResult(unclosedTags, unopenedTags);
        }
    }
}
