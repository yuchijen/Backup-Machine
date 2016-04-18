using System;
using Utility;

namespace CopyLibTest.Template
{
    public class Resources : BaseResource
    {
        /// <summary>
        /// This class must be in the same folder as the embedded resource
        /// </summary>
        private static readonly Type _type = typeof(Resources);


        private static string _BackUpRecord;
        /// <summary>
        /// 
        /// </summary>
        public static string BackUpRecord_Default
        {
            get
            {
                // If the private variable has already been set do not read it from the file system again. 
              if (String.IsNullOrEmpty(_BackUpRecord))
                _BackUpRecord = GetResource("BackUpRecord_Default.xml", _type);
              return _BackUpRecord;
            }
        }
        

    }
}
