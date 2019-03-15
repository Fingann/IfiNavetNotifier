using System;
using HtmlAgilityPack;

namespace IfiNavetNotifier.Extentions
{
    public static class HtmlDocumentExtentions
    {
        public static string GetInnerText(this HtmlDocument doc, string xPath)
        {
            return doc?.DocumentNode?.SelectSingleNode(xPath)?.InnerText ?? string.Empty;
        }
    }
}