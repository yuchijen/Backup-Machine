using System;
using System.IO;

namespace Utility
{
    public abstract class BaseResource
    {
        /// <summary>
        /// Gets the string value of the Embedded Resource
        /// To setup up a file as an Embedded Resource in visual studio right click on resource and set properties:
        ///     Build Action: Embedded Resource
        ///     Copy to output directory: Copy if newer
        /// </summary>
        /// <param name="fileName">The file name with extention of the Embeded Resource.
        /// </param>
        /// <param name="type">The type must be in the same Namespace as the embeded resource</param>
        /// <returns>The content of the file</returns>
        protected static string GetResource(string fileName, Type type)
        {
            using (var stream = type.Assembly.GetManifestResourceStream(type.Namespace + "." + fileName))
            {
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
            }
            return null;
        }
    }
}
