using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using CopyLibTest;
using System.Xml.Linq;
using CopyLibTest.Enum;

namespace BackUpWindow
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      checkEnableBackupButton();
    }

    string defaultRecordXmlPath = "C:\\Users\\" + Environment.UserName + "\\Documents\\PhotoBKMachine";
    string recordXml = "C:\\Users\\" + Environment.UserName + "\\Documents\\PhotoBKMachine\\BackUpRecord.xml";
    //string recordXml = ".\\BackUpRecord.xml";
    string fromPath = "C:\\Syracuse\\2012Fall\\BackupMachine\\TestPhotoPath";
    //string fromPath = "F:\\DCIM\\101MSDCF";
    string toPath = "C:\\photo\\";

    private string[] getExtensionTypeArray()
    {
      List<string> listOfExtensionTypes = new List<string>();

      CheckBox[] checkBoxs = new CheckBox[] { Jpg, Tif, Bmp, Gif, Png };

      foreach (var check in checkBoxs)
      {
        if (check.Checked)
        {
          if (check.Name.ToLower() == "jpg")
          {
            listOfExtensionTypes.Add(".jpg");
            listOfExtensionTypes.Add(".jpeg");
          }
          else
          {
            listOfExtensionTypes.Add("." + check.Name.ToLower());
          }
        }
      }
      return listOfExtensionTypes.ToArray();

    }

    private void btnBackUp_Click(object sender, EventArgs e)
    {
      listBox1.Items.Clear();
      btnBackUp.Enabled = false;
      btnCopyFrom.Enabled = false;
      btnCopyTo.Enabled = false;

      //check RecordXML path and file exist
      checkRecordXmlPathFile();

      UpdateProgress("starting....");
      try
      {
        BackupLibrary backupLibrary = new BackupLibrary(fromPath, toPath, recordXml);        
        backgroundWorker1.RunWorkerAsync(backupLibrary);
      }
      catch (Exception ex) 
      {
        UpdateProgress(ex.Message);
      }
    }

    private void checkRecordXmlPathFile() {
      //check RecordXML path and file exist
      if (!File.Exists(recordXml))
      {
        try
        {
          Directory.CreateDirectory(defaultRecordXmlPath);
          using (StreamWriter sw = new StreamWriter(recordXml))
          {
            StringBuilder sb = new StringBuilder("<?xml version=\"1.0\" ?>\n");
            sb.AppendLine("<history>");
            sb.AppendLine("<lastBackUp backUpTime=\"2004/02/09\">");
            sb.AppendLine("<StartDate>2004/01/12</StartDate>");
            sb.AppendLine("<EndDate>2004/02/09</EndDate>");
            sb.AppendLine("<status>OK</status> \n </lastBackUp>");
            sb.AppendLine("</history>");
            sw.Write(sb.ToString()); //write a line of text to the file               
            sw.Close();
          }
        }
        catch (Exception ex)
        {
          UpdateProgress(ex.Message);
        }
      }
    }

    public void UpdateProgress(string message)
    {
      var controlListBox = listBox1;
      if (controlListBox.InvokeRequired)
      {
        //controlListBox.Invoke(new DelegateUpdateProgress(updateListBox), message);
        controlListBox.Invoke(new Action(() => this.listBox1.Items.Insert(0, message)));
      }
      else
      {
        this.listBox1.Items.Insert(0, message);
      }
    }

    private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      UpdateProgress("complete");
      btnBackUp.Enabled = true;
      btnCopyFrom.Enabled = true;
      btnCopyTo.Enabled = true;

    }

    private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
    {
      BackgroundWorker worker = sender as BackgroundWorker;
      var backupLibrary = (BackupLibrary)e.Argument;

      DateTime setStartCopyDate = DateTime.MinValue;
      if (checkBoxStartCopyDate.Checked)
      {
        setStartCopyDate = dateTimePickerEndCopyDate.Value;
      }
      try
      {
        var bkMethod = (BackUpMethod) Enum.Parse(typeof(BackUpMethod), cBBkMethod.SelectedItem.ToString(), true);
        List<FileInfo> listFileInfo = backupLibrary.GetFileToCopy(bkMethod, getExtensionTypeArray(), setStartCopyDate);

        //List<FileInfo> listFileInfo = backupLibrary.GetFileToCopyForWinForm(getExtensionTypeArray(), setStartCopyDate);
        if (listFileInfo.Count == 0)
        {
          UpdateProgress("No New Photo");
          return;
        }

        for (int i = 0; i < listFileInfo.Count; i++)
        {
          backupLibrary.CopyToDestForWinForm(listFileInfo[i]);
          UpdateProgress("Copying " + listFileInfo[i].Name);

          Thread.Sleep(150);
          float total = listFileInfo.Count;
          float current = i + 1;
          int progress = (int)((current / total) * 100);
          
          backgroundWorker1_ProgressChanged(this, new ProgressChangedEventArgs(progress, i));
        }

        backupLibrary.WriteToXMLForWinform();
        e.Result = "done";
      }
      catch (Exception ex)
      {
        UpdateProgress(ex.Message);
      }
    }

    private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
      int progress = e.ProgressPercentage;
      progressBar1.Value = progress;
    }

    private void btnCopyFrom_Click(object sender, EventArgs e)
    {
      DialogResult result = folderBrowserDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        // The user selected a folder and pressed the OK button.
        // We print the number of files found.       
        fromPath = folderBrowserDialog1.SelectedPath;
        tbCopyFrom.Text = fromPath;
      }
      checkEnableBackupButton();
    }

    private void btnCopyTo_Click(object sender, EventArgs e)
    {
      DialogResult result = folderBrowserDialog1.ShowDialog();
      if (result == DialogResult.OK)
      {
        toPath = folderBrowserDialog1.SelectedPath;
        tbCopyTo.Text = toPath;
      }
      checkEnableBackupButton();
    }

    private void checkEnableBackupButton()
    {
      cBBkMethod.DataSource = Enum.GetValues(typeof(BackUpMethod));
      btnBackUp.Enabled = (!(string.IsNullOrEmpty(tbCopyFrom.Text) || string.IsNullOrEmpty(tbCopyTo.Text)));
      checkBoxStartCopyDate.Checked = false;
      dateTimePickerEndCopyDate.Enabled = false;
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        dateTimePickerEndCopyDate.Enabled = checkBoxStartCopyDate.Checked;      
    }











    //public void BackUpEvent()
    //{
    //  var files = Directory.GetFiles(fromPath, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".JPG") || s.EndsWith(".jpg") || s.EndsWith(".JPEG") || s.EndsWith(".jpeg"));
    //  HashSet<string> hsYearMonth = new HashSet<string>();
    //  List<FileInfo> fileToCopy = new List<FileInfo>();

    //  foreach (var file in files)
    //  {
    //    try
    //    {
    //      FileInfo fi = new FileInfo(file);
    //      string tempYearMonth = getRevisedMonth(fi);
    //      if (getCopyStartDate() <= fi.LastWriteTime)
    //      {
    //        hsYearMonth.Add(tempYearMonth);
    //        fileToCopy.Add(fi);
    //      }
    //    }
    //    catch (FileNotFoundException ex)
    //    {
    //      break;
    //    }
    //  }

    //  CreateFolder(hsYearMonth, toPath);

    //  copyToDest(fileToCopy, toPath);
    //  if (fileToCopy.Count > 0)
    //  {
    //    listBox1.Items.Insert(0, "no xml record now");
    //    //WriteXML(recordXml);
    //  }

    //}


    //private DateTime getCopyStartDate()
    //{
    //  //lbMsg.Items.Clear();
    //  //string xmlPath = recordXml;

    //  XDocument doc1 = XDocument.Load(recordXml);
    //  var q1 = (from x1 in
    //              doc1.Descendants()
    //            where (x1.Attributes("backUpTime").Count() > 0)
    //            select x1).First();

    //  DateTime ret = DateTime.Today.AddYears(-10);

    //  ret = Convert.ToDateTime(q1.FirstAttribute.Value);
    //  //lbMsg.Items.Add("Start copy photo after: " + ret.ToString("yyyy/MM/dd"));

    //  return ret;
    //}




    //private void CreateFolder(HashSet<string> yearMonth, string toPath)
    //{
    //  foreach (var folder in yearMonth)
    //  {
    //    string newPath = Path.Combine(toPath, folder);
    //    if (!Directory.Exists(newPath))
    //    {
    //      Directory.CreateDirectory(newPath);
    //    }
    //  }
    //}

    //private void copyTest()
    //{

    //  for (int i = 0; i < 100; i++)
    //  {
    //    CopyMeta copyMeta = new CopyMeta { FullName = "FullName" + i, DestnationFile = "DestnationFile" + i, Overwrite = true };
    //    Thread newThread = new Thread(new ParameterizedThreadStart(CopyWorkThread));
    //    newThread.Start(copyMeta);

    //  }
    //}


    //private void copyToDest(IList<FileInfo> files, string toPath)
    //{
    //  if (files.Count == 0)
    //  {
    //    return;
    //  }

    //  foreach (var fi in files)
    //  {
    //    try
    //    {
    //      string tempYearMonth = getRevisedMonth(fi);
    //      string destFile = Path.Combine(toPath, tempYearMonth, fi.Name);
    //      //File.Copy(fi.FullName, destFile, true);

    //      CopyMeta copyMeta = new CopyMeta { FullName = fi.FullName, DestnationFile = destFile, Overwrite = true };
    //      Thread newThread = new Thread(new ParameterizedThreadStart(CopyWorkThread));
    //      newThread.Start(copyMeta);

    //    }
    //    catch (DirectoryNotFoundException ex)
    //    {
    //      break;
    //    }
    //    catch (FileNotFoundException e)
    //    {
    //      break;
    //    }
    //  }
    //}


    //void CopyWorkThread(object param)
    //{
    //  //Thread.Sleep(3000);
    //  CopyMeta copyMeta = (CopyMeta)param;
    //  //UpdateProgress("copying..." + copyMeta.FullName + " to " + copyMeta.DestnationFile);

    //  File.Copy(copyMeta.FullName, copyMeta.DestnationFile, copyMeta.Overwrite);

    //  UpdateProgress("copying..." + copyMeta.FullName + " to " + copyMeta.DestnationFile);
    //  Console.WriteLine("[Thread {0}]:copying...{1} to {2}", Thread.CurrentThread.ManagedThreadId, copyMeta.FullName, copyMeta.DestnationFile);

    //}


    //protected delegate void DelegateUpdateProgress(string message);
    //private void updateListBox(string message)
    //{
    //  var control1 = listBox1;
    //  control1.Items.Insert(0,message);
    //}


    ///// <summary>
    ///// get the correct month name string e.g. 01, 02...
    ///// </summary>
    ///// <param name="destFileInfo"></param>
    ///// <returns></returns>
    //private string getRevisedMonth(FileInfo destFileInfo)
    //{
    //  string reviseMonth = destFileInfo.LastWriteTime.Month.ToString();
    //  if (reviseMonth.Length == 1)
    //  {
    //    reviseMonth = "0" + reviseMonth;
    //  }
    //  return destFileInfo.LastWriteTime.Year.ToString() + reviseMonth;
    //}


  }
}
