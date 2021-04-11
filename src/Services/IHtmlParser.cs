using System.Text.RegularExpressions;

namespace StiebelEltronApiServer.Services
{
    public interface IHtmlParser
    {
        ScanResult ParseTagTree(string dirtyHtml, MatchCollection tags);
    }
}