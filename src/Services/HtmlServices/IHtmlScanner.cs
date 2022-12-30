using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public interface IHtmlScanner
    {
        ParseResult ParseTagTree(string dirtyHtml, IList<SubStringIndices> tags);
    }
}