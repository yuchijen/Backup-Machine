using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using PhotoBackup;

namespace CopyLibTest
{
  class Program
  {
    static void Main(string[] args)
    {
      string recordXml = "C:\\Syracuse\\2012Fall\\BackupMachine\\PhotoBackup\\BackUpRecord.xml";
      string fromPath = "F:\\DCIM\\101MSDCF";
      string toPath = "C:\\photo\\";

      BackupLibrary bl = new BackupLibrary(fromPath, toPath, recordXml,null);
      bl.BackUpEvent();



    }
  }
}
