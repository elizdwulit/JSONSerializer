using System;
using System.IO;
using System.Text.RegularExpressions;

namespace JSONProject
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
        public string convertFileToString(string filePath)
        {
            string fileContents = string.Empty;

            try
            {
                fileContents = Regex.Replace(File.ReadAllText(filePath), @"[\r\n\t ]+", " ");
            }
            catch (Exception e)
            {
                Console.WriteLine("InputReader.convertFileToText -- Exception converting file to text: " + e.Message);
                return string.Empty;
            }

            return fileContents;
        }
    }
}
