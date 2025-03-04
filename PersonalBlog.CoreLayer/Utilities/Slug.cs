namespace PersonalBlog.CoreLayer.Utilities
{
    public static class Slug
    {
        public static string toSlug(this string text)
        {
            return text.Trim().ToLower()
                              .Replace(" ", "-")
                              .Replace("@", "")
                              .Replace("!", "")
                              .Replace("#", "")
                              .Replace("%", "")
                              .Replace("$", "")
                              .Replace("^", "")
                              .Replace("*", "")
                              .Replace("(", "")
                              .Replace(")", "")
                              .Replace("+", "")
                              .Replace("=", "")
                              .Replace("/", "")
                              .Replace(@"\", "");
        }
    }

}

