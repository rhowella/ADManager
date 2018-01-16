using System;

namespace ActiveDirManager.Core
{
  [Serializable]
  public class ImportFile
  {
    public string FilePath;
    public string FileName;
    public bool HasHeader;
  }
}
