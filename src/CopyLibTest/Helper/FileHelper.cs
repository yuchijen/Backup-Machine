using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Utility;

namespace CopyLibTest.Helper
{
  public class FileHelper
  {

    /// <summary>
    /// get List fileinfo according to input string array with file extension type
    /// </summary>
    /// <param name="fromPath">fromPath</param>
    /// <param name="extensions">extensions string array</param>
    /// <returns></returns>
    public List<FileInfo> GetListOfSearchFileType(string fromPath, string[] extensions)
    {
      DirectoryInfo di = new DirectoryInfo(fromPath);

      List<FileInfo> fileInfos =
          di.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly)
               .Where(f => extensions.Contains(f.Extension.ToLower()))
               .ToList();

      return fileInfos;
    }

    /// <summary>
    /// create yearMonth folder
    /// </summary>
    /// <param name="yearMonth">year and month digit</param>
    public void CreateFolder(HashSet<string> yearMonth, string toPath)
    {
      foreach (var folder in yearMonth)
      {
        string newPath = Path.Combine(toPath, folder);
        if (!Directory.Exists(newPath))
        {
          Directory.CreateDirectory(newPath);
        }
      }
    }


    /// <summary>
    /// get the correct month name string e.g. 01, 02...
    /// </summary>
    /// <param name="destFileInfo"></param>
    /// <returns></returns>
    public string GetRevisedMonth(FileInfo destFileInfo)
    {
      string reviseMonth = destFileInfo.LastWriteTime.Month.ToString();
      if (reviseMonth.Length == 1)
      {
        reviseMonth = "0" + reviseMonth;
      }
      return destFileInfo.LastWriteTime.Year.ToString() + reviseMonth;
    }



  }
}
