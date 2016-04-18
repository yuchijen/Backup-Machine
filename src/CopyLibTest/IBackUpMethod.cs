using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CopyLibTest
{
  interface IBackUpMethod
  {
    List<FileInfo> GetFileToCopyForWinForm(string[] extensions, DateTime setStartCopyDate, string fromPath, DateTime lastCopyDate, string toPath);
    
  }
}
