using System.Text.RegularExpressions;

namespace StiebelEltronApiServer.Services
{
    public interface IHtmlScanner
    {
        ParseResult ParseTagTree(string dirtyHtml, MatchCollection tags);
    }
}