using HtmlAgilityPack;

namespace PersonalBlog.CoreLayer.Utilities;

public static class StripHtml
{
    public static string HtmltoText(this string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        return doc.DocumentNode.InnerText;
    }
}