using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Utility
{
    public class Parser
    {
        /// <summary>
        /// Replaces any variable inside of the string with the corresponding dictionary key.
        /// </summary>
        /// <param name="fileContents">String with variables ($className.variableName) to replace</param>
        /// <param name="variableDictionary">Keys must be in the format className.variableName</param>
        /// <returns>String with all the variables ($className.variableName) replaced</returns>
        /// <example>($BASE.NOW) will be replaced with the current date time</example>
        public static string Replace(string fileContents, Dictionary<string, string> variableDictionary)
        {
            var regexPattern = new Regex(@"\(\$(.+?)\.(.+?)\)", RegexOptions.Singleline);
            var variableList = regexPattern.Matches(fileContents);

            foreach (Match variable in variableList)
            {
                var key = variable.Groups[1].Value + "." + variable.Groups[2].Value;
                
                //VariableDictionary's value if it exists & not null else string.Empty
                var value = variableDictionary.ContainsKey(key)
                                ? variableDictionary[key] ?? string.Empty
                                : string.Empty;

                fileContents = fileContents.Replace(variable.Value, value);

            }
            return fileContents;
        }

        /// <summary>
        /// Creates a new dictionary with base properties that are not part of existing entities. 
        /// </summary>
        /// <returns>New Guid, Now (date time)</returns>
        public static Dictionary<string, string> GetVariableDictionary()
        {
            var rd = new Random();
            var rand = rd.Next(10000, 99999).ToString();

            var dictionary = new Dictionary<string, string>
                                 {
                                     {"DateTime.Now", DateTime.Now.ToString("yyyy/MM/dd")},
                                     //{"DateTime.Previous", DateTime.Now.ToString("yyyyMMddHHmm")},
                                     //{"String.Status", "OK"}
                                 };
            return dictionary;

        }


        /// <summary>
        /// Dictionary populated with provider, patient, and base properties & values
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        //public static Dictionary<string, string> GetVariableDictionary(Contracts.Provider provider,
        //                                                            Patient.Contracts.Patient patient = null)
        //{
        //    var dictionary = GetVariableDictionary();
        //    // Add the property name/value pairs for the Provider object to the dictionary
        //    if (provider != null)
        //    {
        //        // ReSharper disable AccessToStaticMemberViaDerivedType - If this is over written we want the GetPropertyDictionary from ProviderSetup
        //        Entity.Provider.ProviderSetup.AddObjectProperties(provider, ref dictionary);
        //        // ReSharper restore AccessToStaticMemberViaDerivedType
        //    }
        //    // Add the property name/value pairs for the Patient object to the dictionary
        //    if (patient != null)
        //        Entity.Patient.PatientSetup.AddPropertiesAsVariables(patient, ref dictionary);
        //    return dictionary;

        //}

    }
}
