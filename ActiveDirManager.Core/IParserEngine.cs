using System.Collections.Generic;


namespace ActiveDirManager.Core
{

  public interface IParserEngine
  {
    IList<string> ExtractRecords(char lineDelimiter, string csvText);
    IList<string> ExtractFields(char delimiter, char quote, string csvLine);
  }

}
