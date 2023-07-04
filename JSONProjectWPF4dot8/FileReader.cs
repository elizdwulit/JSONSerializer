using System;
using System.IO;
using System.Text.RegularExpressions;

namespace JSONProjectWPF4dot8
{
    /// <summary>
    /// Class responsible for recieving and reading a file
    /// </summary>
    internal class FileReader
    {
        /// <summary>
        /// Empty Constructor
        /// </summary>
        public FileReader()
        {
            // empty constructor
        }

        /// <summary>
        /// Reads a file from the input file path and returns its contents as a string
        /// </summary>
        /// <param name="filePath">path to file</param>
        /// <returns>string of file contents</returns>
        public static string convertFileToString(string filePath)
        {
            string fileContents = string.Empty;

            try
            {
                // Replace all whitespace with a single space
                fileContents = Regex.Replace(File.ReadAllText(filePath), @"[\r\n\t ]+", " ");
            }
            catch (Exception e)
            {
                Console.WriteLine("FileReader.convertFileToText -- Exception converting file to text: " + e.Message);
                return string.Empty;
            }

            return fileContents;
        }
    }
}
