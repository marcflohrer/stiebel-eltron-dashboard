using System.Text.RegularExpressions;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public static class Tags
    {
        public record TagContext(string tag, Match id);
        public record UnclosedTag(string tag, int index);
    }
}