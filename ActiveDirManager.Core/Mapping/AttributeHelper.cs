using ActiveDirManager.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ActiveDirManager.Core.Mapping
{
  public static class AttributeHelper
  {
    public static List<string> GetDataNames(Type type, string propertyName)
    {
      var property = type.GetProperty(propertyName).GetCustomAttributes(true).Where(x => x.GetType().Name == "DataNamesAttribute").FirstOrDefault();
      if (property != null)
      {
        return ((DataNamesAttribute)property).ValueNames;
      }
      return new List<string>();
    }
  }
}
