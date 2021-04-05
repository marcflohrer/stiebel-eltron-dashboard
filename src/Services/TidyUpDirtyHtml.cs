
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static StiebelEltronApiServer.Services.Tags;

namespace StiebelEltronApiServer.Services
{
    public class TidyUpDirtyHtml : ITidyUpDirtyHtml
    {
        
        private MatchCollection GetMatches(string input, string pattern)
        {
            var devTagRegex = new Regex(pattern);
            return devTagRegex.Matches(input);
        }

        public string GetTidyHtml(string dirtyHtml)
        {
            var tags = GetMatches(dirtyHtml, "<[A-z \"=\\/:.0-9%#!?;,)_(-]+[/]*>");
            var tagStack = new Stack<TagContext>();

            var openingTagNameRegex = new Regex("(?<=<)[A-z]+");
            var closingTagNameRegex = new Regex("(?<=</)[A-z]+");
            var tagIdRegex = new Regex("(?<= [A-z]+=\")[A-z ]+(?=\")");
            var currentPosition = 0;
            var unopenedTags = new List<SubStringIndices>();
            foreach (Match match in tags)
            {
                currentPosition = match.Index;
                // skip self-closing tags
                if(match.Value.EndsWith("/>"))
                {
                    continue;
                }
                // skip comments
                if(match.Value.StartsWith("<!--"))
                {
                    continue;
                }
                if(match.Value.StartsWith("</"))
                {
                    var tagName = closingTagNameRegex.Match(match.Value);
                    // skip <?xml tag
                    if(string.IsNullOrEmpty(tagName.Value))
                    {
                        continue;
                    }
                    if(!Tags.TagMap.ContainsKey(tagName.Value)){
                        throw new Exception("Unknown tag: " + tagName.Value);
                    }
                    var tag = Tags.TagMap[tagName.Value];
                    var recentOpenTag = tagStack.Peek().tag;
                    if(tag == recentOpenTag)
                    {
                        tagStack.Pop();
                    }
                    else if(tag != recentOpenTag)
                    {
                        var endPosition = currentPosition + match.Value.Length - 1;
                        Console.WriteLine("Found unopened tag at position: " + currentPosition + ", end position: " + endPosition);
                        unopenedTags.Add(new SubStringIndices(currentPosition, endPosition));
                    }
                }else if(match.Value.StartsWith("<"))
                {
                    var tagName = openingTagNameRegex.Match(match.Value);
                    // skip <?xml tag
                    if(string.IsNullOrEmpty(tagName.Value))
                    {
                        continue;
                    }
                    if(!Tags.TagMap.ContainsKey(tagName.Value)){
                        throw new Exception("Unknown tag: " + tagName.Value);
                    }
                    var tag = Tags.TagMap[tagName.Value];
                    var tagId = tagIdRegex.Match(match.Value);
                    tagStack.Push(new TagContext(tag, tagId.Value));
                }
            }
            var dirtyHtmlCharArray = dirtyHtml.ToCharArray();
            foreach(var unopenedTag in unopenedTags)
            {
                var i = unopenedTag.start;
                do{
                    dirtyHtmlCharArray[i] = ' ';
                }
                while(i++ < unopenedTag.end);
            }
            dirtyHtml = new string(dirtyHtmlCharArray);
            while(tagStack.TryPop(out var unclosedTag))
            {
                dirtyHtml += "</"+unclosedTag+">";
            }
            
            dirtyHtml = dirtyHtml.Replace("&nbsp;", string.Empty);
            return dirtyHtml.Replace("&copy;", string.Empty);
        }
    }

    public record SubStringIndices(int start, double end);
}
