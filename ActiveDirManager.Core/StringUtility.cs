using System.Globalization;

namespace ActiveDirManager.Core
{
  public static class StringUtility
  {
    public static string ToCamelCase(string text)
    {
      TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
      return textInfo.ToTitleCase(text);
    }
  }
}
