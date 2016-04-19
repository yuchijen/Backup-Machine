using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CopyLibTest.Helper;

namespace CopyLibTest
{
  public class BackUpByYearMonthDay : IBackUpMethod
  {

    public List<FileInfo> GetFileToCopyForWinForm(string[] extensions, DateTime setStartCopyDate, string fromPath, DateTime lastCopyDate, string toPath)
    {
      HashSet<string> hsYearMonth = new HashSet<string>();

      List<FileInfo> fileToCopy = new List<FileInfo>();

      FileHelper fileHelper = new FileHelper();

      foreach (var fi in fileHelper.GetListOfSearchFileType(fromPath, extensions))
      {
        try
        {
          //FileInfo fi = new FileInfo(file);
          string tempYearMonth = fileHelper.GetRevisedMonth(fi);

          // if set end copy date is min value, means by default : start copy date is last copy date
          if (setStartCopyDate == DateTime.MinValue)
          {
            if (lastCopyDate <= fi.LastWriteTime)
            {
              hsYearMonth.Add(tempYearMonth);
              fileToCopy.Add(fi);
            }
          }
          else
          {
            if (setStartCopyDate <= fi.LastWriteTime)
            {
              hsYearMonth.Add(tempYearMonth);
              fileToCopy.Add(fi);
            }
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
