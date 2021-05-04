using System.Text.RegularExpressions;

namespace StiebelEltronApiServer.Services.HtmlServices
{
    public static class Tags
    {
        public record TagContext(string tag, Match id);
        public record UnclosedTag(string tag, int index);
    }
}