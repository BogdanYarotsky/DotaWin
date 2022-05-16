using System.Globalization;

namespace DotaWin.API.Utilities;

static class Utilities
{
    public static string ToTitleCase(this string title)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
    }
}
