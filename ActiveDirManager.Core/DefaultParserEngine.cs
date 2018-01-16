﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ActiveDirManager.Core
{
  /// <summary>
  /// Parser Engine that handles escaped fields
  /// </summary>
  public sealed class DefaultParserEngine : IParserEngine
  {
    #region Private Members

    private char _delimiter { get; set; }
    private char _quote { get; set; }

    #endregion

    #region IParserEngine methods


    public IList<string> ExtractFields(char delimiter, char quote, string csvLine)
    {

      var fieldValues = new List<string>();

      this._delimiter = delimiter;
      this._quote = quote;

      if (csvLine != string.Empty)
      {
        AnalyzeField(fieldValues, csvLine, false);
      }
      
      var fieldValuesList = new List<string>(fieldValues);
      return fieldValuesList;
    }

    public IList<string> ExtractRecords(char lineDelimiter, string csvText)
    {
      var lines =  csvText.Split(lineDelimiter);
      var linesList = new List<string>(lines);
      return linesList;
    }

    #endregion

    #region Private Methods

    private void AnalyzeField(List<string> lineArray, string text, bool insideQuotes)
    {
      var delimiterLocation = text.IndexOf(this._delimiter);
      var quoteLocation = text.IndexOf(this._quote.ToString());
      var endPoint = 0;


      var fieldValue = string.Empty;

      if (text.Length == 0 || (delimiterLocation == -1 && quoteLocation == -1))
      {

        if (text.Length > 0)
        { 
          fieldValue = text.Substring(0).Replace("\n", string.Empty).Replace("\r", string.Empty);
          lineArray.Add(fieldValue);
        }

        return;
      }

      if (quoteLocation == -1 || delimiterLocation < quoteLocation && delimiterLocation != -1 && insideQuotes == false)
      {
        //if (delimiterLocation == -1)
        //{
        //    fieldValue = text.Replace("\"", string.Empty).Replace("\r", string.Empty);

        //    lineArray.Add(fieldValue);

        //    AnalyzeField(lineArray, string.Empty, false);

        //}
        //else
        //{
        // delimiter found
        fieldValue = text.Substring(0, delimiterLocation);

        endPoint = delimiterLocation;
        //}
      }
      else if ((delimiterLocation > quoteLocation && insideQuotes == false) || delimiterLocation == -1)
      {
        if (quoteLocation == 0)
        {
          // we've found start quote
          var nextQuoteLocation = text.Substring(1).IndexOf(this._quote.ToString());
          fieldValue = text.Substring(1, nextQuoteLocation);
          endPoint = nextQuoteLocation + 2; // acounts for ", (vs just ,)
        }
        else
        {
          fieldValue = text.Substring(0, quoteLocation);
          endPoint = quoteLocation;
        }
      }


      if (fieldValue != string.Empty)
      {
        lineArray.Add(fieldValue);
        string workingString = text.Substring(endPoint + 1);
        AnalyzeField(lineArray, workingString, false);
      }
      return;

    }
  

    #endregion

  }
}