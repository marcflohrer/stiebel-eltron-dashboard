using System.Text.RegularExpressions;

namespace StiebelEltronApiServer.Services
{
    public interface IHtmlParser
    {
        ParseResult ParseTagTree(string dirtyHtml, MatchCollection tags);
    }
}