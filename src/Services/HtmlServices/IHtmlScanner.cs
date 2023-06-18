using System.Collections.Generic;

namespace StiebelEltronDashboard.Services.HtmlServices
{
    public interface IHtmlScanner
    {
        ParseResult ParseTagTree(string dirtyHtml, IList<SubStringIndices> tags);
    }
}
