using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static StiebelEltronApiServer.Services.Tags;

namespace StiebelEltronApiServer.Services
{
    public class HtmlParser : IHtmlParser
    {
        public ParseResult ParseTagTree(string dirtyHtml, MatchCollection tags){
            var currentPosition = 0;
            
            var openingTagNameRegex = new Regex("(?<=<)[A-z]+");
            var closingTagNameRegex = new Regex("(?<=</)[A-z]+");
            var tagIdRegex = new Regex("(?<= [A-z]+=\")[A-z ]+(?=\")");

            var tagStack = new Stack<TagContext>();
            var unopenedTags = new List<SubStringIndices>();
            var unclosedTags = new List<UnclosedTag>();

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
                        while(tempTagStack.Count > 0){
                            top = tempTagStack.Peek();
                            if(tag == top.tag){
                                foundOpeningTag = true;
                                break;
                            }
                            top = tempTagStack.Pop();     
                        }
                        endPosition = currentPosition + match.Value.Length - 1;
                        Console.WriteLine("[DEBUG] Found unopened tag at position: " + currentPosition + ", end position: " + endPosition);
                        
                        if(!foundOpeningTag){
                            unopenedTags.Add(new SubStringIndices(currentPosition, endPosition));
                        }else{
                            unclosedTags.Add(new UnclosedTag(recentOpenTag.tag, currentPosition));
                        }
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
                    var tag = tagName.Value;
                    var tagId = tagIdRegex.Match(match.Value);
                    tagStack.Push(new TagContext(tag, tagId));
                }
            }

            return new ParseResult(unclosedTags, unopenedTags);
        }
    }
    public record ParseResult(IReadOnlyList<UnclosedTag> unclosedTags, IReadOnlyList<SubStringIndices> unopenedTags);
}