using System.Text.RegularExpressions;

namespace StiebelEltronApiServer.Services.HtmlServices
{
    public interface IHtmlScanner
    {
        ParseResult ParseTagTree(string dirtyHtml, MatchCollection tags);
    }
}