using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.Text;
using Utility;
using CopyLibTest.Template;
using System.Threading;
using CopyLibTest.Helper;
using CopyLibTest.Enum;

namespace CopyLibTest
{
  public class CopyMeta
  {
    public string FullName;
    public string DestnationFile;
    public bool Overwrite;
  }

  public class BackupLibrary
  {
    #region property 
    
    private string _fromPath;
    private string _toPath;
    private string _xmlPath;
    private string[] _extension;
    private DateTime _setStartCopyDate;
    private DateTime _lastCopyDate;

    public string FromPath
    {
      get { return _fromPath; }
      set { value = _fromPath; }
    }
    public string ToPath
    {
      get { return _toPath; }
      set { value = _toPath; }
    }
    public string RecordXml
    {
      get { return _xmlPath; }
      set { value = _xmlPath; }
    }

    public string[] ExtensionType
    {
      get { return _extension; }
      set { value = _extension; }
    }

    #endregion 

    private FileHelper fileHelper;

    #region Contructor

    public BackupLibrary(string fromPath, string toPath, string xmlPath, string[] extension)
    {
      fileHelper = new FileHelper();
      _fromPath = fromPath;
      _toPath = toPath;
      _xmlPath = xmlPath;

      string[] extensions = extension == null ? new[] { ".jpg", ".jpeg" } : extension;
      _extension = extensions;
      _lastCopyDate = getCopyStartDate();
    }

    public BackupLibrary(string fromPath, string toPath, string xmlPath)
    {
      fileHelper = new FileHelper();
      _fromPath = fromPath;
      _toPath = toPath;
      _xmlPath = xmlPath;
      _lastCopyDate = getCopyStartDate();
    }

    
    #endregion



    /// <summary>
    /// Backup event for pure application 
    /// </summary>
    public void BackUpEvent()
    {
      var files = Directory.GetFiles(_fromPath, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".JPG") || s.EndsWith(".jpg") || s.EndsWith(".JPEG") || s.EndsWith(".jpeg"));
      HashSet<string> hsYearMonth = new HashSet<string>();

      List<FileInfo> fileToCopy = new List<FileInfo>();

      foreach (var file in files)
      {
        try
        {
          FileInfo fi = new FileInfo(file);
          string tempYearMonth = fileHelper.GetRevisedMonth(fi);
          if (_lastCopyDate <= fi.LastWriteTime)
          {
            hsYearMonth.Add(tempYearMonth);
            fileToCopy.Add(fi);
          }
        }
        catch (FileNotFoundException ex)
        {
          throw new FileNotFoundException();
        }
      }

      fileHelper.CreateFolder(hsYearMonth, _toPath);

      copyToDest(fileToCopy, _toPath);
      if (fileToCopy.Count > 0)
      {
        WriteXML(_xmlPath);
      }

    }

    
    public bool checkCameraAttached(string fromPath)
    {
      return Directory.Exists(fromPath);
    }


    #region For WinForm

    public List<FileInfo> GetFileToCopy(BackUpMethod enumBackUpMethod, string[] extensions, DateTime setStartCopyDate)
    {
      IBackUpMethod iBackUpMethod;

      switch (enumBackUpMethod)
      {
        case BackUpMethod.ByThisMonth:
          iBackUpMethod = new BackUpByThisMonth();
          break;
        case BackUpMethod.ByYearMonth:
          iBackUpMethod = new BackUpByYearMonthDay();
          break;
        default:
          iBackUpMethod = new BackUpByYearMonthDay();          
          break;
      }
      return iBackUpMethod.GetFileToCopyForWinForm(extensions, setStartCopyDate, _fromPath, _lastCopyDate, _toPath);
    }


    /// <summary>
    /// collect fileInfos to winform
    /// </summary>
    public List<FileInfo> GetFileToCopyForWinForm(string[] extensions, DateTime setStartCopyDate)
    {
      HashSet<string> hsYearMonth = new HashSet<string>();

      List<FileInfo> fileToCopy = new List<FileInfo>();

      _setStartCopyDate = setStartCopyDate;

      foreach (var fi in fileHelper.GetListOfSearchFileType(_fromPath, extensions))
      {
        try
        {
          //FileInfo fi = new FileInfo(file);
          string tempYearMonth = fileHelper.GetRevisedMonth(fi);

          // if set end copy date is min value, means by default : start copy date is last copy date
          if (_setStartCopyDate == DateTime.MinValue)
          {
            if (_lastCopyDate <= fi.LastWriteTime)
            {
              hsYearMonth.Add(tempYearMonth);
              fileToCopy.Add(fi);
            }
          }
          else
          {
            if (_setStartCopyDate <= fi.LastWriteTime)
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
      fileHelper.CreateFolder(hsYearMonth, _toPath);

      return fileToCopy;
    }



    /// <summary>
    /// copy single file to destination 
    /// </summary>
    /// <param name="fileInfo">fileInfo</param>
    /// <param name="toPath"></param>
    public void CopyToDestForWinForm(FileInfo fileInfo)
    {
      try
      {
        string tempYearMonth = fileHelper.GetRevisedMonth(fileInfo);
        string destFile = Path.Combine(_toPath, tempYearMonth, fileInfo.Name);
        File.Copy(fileInfo.FullName, destFile, true);

      }
      catch (DirectoryNotFoundException ex)
      {
        throw new DirectoryNotFoundException();
      }
      catch (FileNotFoundException e)
      {
        throw new FileNotFoundException();
      }

    }

    public void WriteToXMLForWinform()
    {
      WriteXML(_xmlPath);
    }

    #endregion


    #region private methods

    protected void WriteXML(string recordXml)
    {
      StringBuilder sb_source = new StringBuilder();
      Dictionary<string, string> dicVariable = Parser.GetVariableDictionary();
      replaceVariable(dicVariable);
      
      try
      {
        using (StreamReader sr = new StreamReader(recordXml))
        {
          String line;
          while ((line = sr.ReadLine()) != null)
          {
            if (line.Trim().Contains("<history>"))
            {
              sb_source.Append(line).Append("\n");
              sb_source.Append(Parser.Replace(CopyLibTest.Template.Resources.BackUpRecord_Default, dicVariable)).Append("\n");
            }
            else
            {
              sb_source.Append(line).Append("\n");
            }
          }
          sr.Close();
        }

        using (StreamWriter sw = new StreamWriter(recordXml))
        {
          sw.Write(sb_source.ToString()); //write a line of text to the file               
          sw.Close();
        }
        
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
        //lbMsg.Items.Add(ex.Message.ToString());
      }
    }

    private void replaceVariable(Dictionary<string, string> dic)
    {
      if (_setStartCopyDate == DateTime.MinValue)
      {
        dic.Add("DateTime.Previous", _lastCopyDate.ToString("yyyy/MM/dd"));
      }
      else
      {
        dic.Add("DateTime.Previous", _setStartCopyDate.ToString("yyyy/MM/dd"));
      }
      dic.Add("String.Status", "OK");
    }


    private void copyToDest(IList<FileInfo> files, string toPath)
    {
      if (files.Count == 0)
      {
        return;
      }

      foreach (var fi in files)
      {
        try
        {
          string tempYearMonth = fileHelper.GetRevisedMonth(fi);
          string destFile = Path.Combine(toPath, tempYearMonth, fi.Name);
          //File.Copy(fi.FullName, destFile, true);

          CopyMeta copyMeta = new CopyMeta { FullName = fi.FullName, DestnationFile = destFile, Overwrite = true };
          Thread newThread = new Thread(new ParameterizedThreadStart(CopyWorkThread));
          newThread.Start(copyMeta);

          //ProgressMark.ProgressedFiles++;
        }
        catch (DirectoryNotFoundException ex)
        {
          break;
        }
        catch (FileNotFoundException e)
        {
          break;
        }
      }
    }

    private void CopyWorkThread(object param)
    {
      CopyMeta copyMeta = (CopyMeta)param;
      File.Copy(copyMeta.FullName, copyMeta.DestnationFile, copyMeta.Overwrite);
      Console.WriteLine("[Thread {0}]:copying...{1} to {2}", Thread.CurrentThread.ManagedThreadId, copyMeta.FullName, copyMeta.DestnationFile);

    }


    private DateTime getCopyStartDate()
    {
      try
      {
        XDocument doc1 = XDocument.Load(_xmlPath);
        var q1 = (from x1 in
                    doc1.Descendants()
                  where (x1.Attributes("backUpTime").Count() > 0)
                  select x1).First();

        DateTime ret = DateTime.Today.AddYears(-10);

        ret = Convert.ToDateTime(q1.FirstAttribute.Value);

        return ret;
      }
      catch (FileNotFoundException fileNotFound)
      {
        throw new FileNotFoundException("Cannot find Record XML: " + _xmlPath + "; " + fileNotFound);
      }
      catch (Exception ex)
      {
        throw new FileNotFoundException(ex.Message);      
      }
    }

    
    #endregion





  }
}