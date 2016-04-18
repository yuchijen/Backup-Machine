using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CopyLibTest.Helper;

namespace CopyLibTest
{
  public class BackUpByThisMonth : IBackUpMethod
  {
    public List<System.IO.FileInfo> GetFileToCopyForWinForm(string[] extensions, DateTime setStartCopyDate, string fromPath, DateTime lastCopyDate, string toPath)
    {
      HashSet<string> hsYearMonth = new HashSet<string>();

      List<FileInfo> fileToCopy = new List<FileInfo>();

      FileHelper fileHelper = new FileHelper();

      string reviseMonth = DateTime.Now.Month.ToString().Length == 1 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
      string tempYearMonth = DateTime.Now.Year.ToString() + reviseMonth;
      
      hsYearMonth.Add(tempYearMonth);

      foreach (var fi in fileHelper.GetListOfSearchFileType(fromPath, extensions))
      {
        try
        {
          DateTime thisMonthDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

          if (thisMonthDate <= fi.LastWriteTime)
          {
            fileToCopy.Add(fi);
          }
        }
        catch (FileNotFoundException ex)
        {
          throw new FileNotFoundException(ex.Message);
        }
      }
      
      fileHelper.CreateFolder(hsYearMonth, toPath);
      return fileToCopy;
    }
  }
}
