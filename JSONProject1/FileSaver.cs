using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JSONProject
{
    /// <summary>
    /// Class responsible for saving a JSONObject to a new text file
    /// </summary>
    internal class FileSaver
    {
        // Writer used for writing to the new text file
        private StreamWriter streamWriter;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public FileSaver()
        {
            // empty constructor
        }

        /// <summary>
        /// Save the contents of the input JSON Object into a new text file
        /// </summary>
        /// <param name="jsonObj">json object to save</param>
        /// <param name="outputFileName">Desired name for the output file</param>
        /// <returns>The name of the file that was saved. Usually the outputFileName param with ".txt" appended</returns>
        public string saveFile(JSONObject jsonObj, string outputFileName)
        {
            if (jsonObj == null)
            {
                return "";
            }

            StringBuilder sb = new StringBuilder();
            string fileSaveName = outputFileName;

            try
            {
                // Construct the name of the file to save the JSONObject to
                sb.Append(outputFileName);
                sb.Append(".txt"); // save as txt file
                fileSaveName = sb.ToString();

                // Write to the new file
                FileStream fileStream = new FileStream(fileSaveName, FileMode.Create, FileAccess.Write);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);

                // for each line in json object write the line contents
                streamWriter.WriteLine("{");
                List<KeyValuePair> keyValuePairs = jsonObj.getAllEntries();
                writeJson(keyValuePairs);
                streamWriter.WriteLine("}");

                streamWriter.Flush();
                streamWriter.Close();
                fileStream.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("FileSaver.saveFile -- Exception saving file: " + e.Message);
                return null;
            }

            return fileSaveName;
        }

        /// <summary>
        /// Writes key-value pairs to a file
        /// </summary>
        /// <param name="keyValuePairs">key-value pairs to write</param>
        private void writeJson(List<KeyValuePair> keyValuePairs)
        {
            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                KeyValuePair kvp = keyValuePairs[i];

                // print key
                string key = kvp.getKey();
                streamWriter.Write("\"" + key + "\": ");

                Object val = kvp.getVal();

                // If the value of the key-value pair is a string, write it to the file surrounded by double quotation marks
                if (val is string)
                {
                    streamWriter.Write("\"" + val);

                    // if this is not the last key-value pair in the list, append a comma
                    if (i != keyValuePairs.Count - 1)
                    {
                        streamWriter.WriteLine("\",");
                    }
                }
                else // If the value is a JSONObject, write it to the file surrounded by curly braces
                {
                    streamWriter.WriteLine("{");
                    writeJson((val as JSONObject).getAllEntries());

                    // if this is not the last key-value pair in the list, append a comma along with the closing curly brace
                    streamWriter.WriteLine("}" + (i != keyValuePairs.Count - 1 ? "," : ""));
                }
            }
        }
    }
}
