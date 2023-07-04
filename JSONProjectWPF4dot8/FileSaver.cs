using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JSONProjectWPF4dot8
{
    /// <summary>
    /// Class responsible for saving an output to a new file
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
        /// <returns>the name of the file that was saved</returns>
        public string saveFile(JSONObject jsonObj, string outputFileName)
        {
            if (jsonObj == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            string fileSaveName = outputFileName;

            try
            {
                // Construct the name of the file to save the JSONObject to
                sb.Append(outputFileName);
                fileSaveName = sb.ToString();

                // write to new file
                FileStream fileStream = new FileStream(fileSaveName, FileMode.Create, FileAccess.Write);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);

                // for each line in json object write the line contents
                //streamWriter.WriteLine("{");
                List<KeyValuePair> keyValuePairs = jsonObj.getAllEntries();
                writeTree(keyValuePairs, 1, false);
                //streamWriter.WriteLine("}");

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
        /// Write the JSONObject tree containing the input KeyValuePair entries
        /// </summary>
        /// <param name="entries">key-value pairs to write</param>
        /// <param name="tabIndex">Current index of tabs (needed to display correct indentation)</param>
        /// <param name="isNestedJson">flag indicating if this json is contained in a parent json</param>
        private void writeTree(List<KeyValuePair> entries, int tabIndex, bool isNestedJson)
        {
            int currEntry = 0;
            streamWriter.WriteLine("");
            foreach (KeyValuePair kvp in entries)
            {
                // Add spaces to look like tabs
                currEntry = currEntry + 1;
                for (int i = 0; i < tabIndex; i++)
                {
                    streamWriter.Write("     ");
                    if (i == tabIndex - 1)
                    {
                        streamWriter.Write("+");
                    }
                }

                // Write the key surrounded by double quotes
                streamWriter.Write("\"");
                streamWriter.Write(kvp.getKey());
                streamWriter.Write("\"");
                streamWriter.Write(": ");

                Object val = kvp.getVal();

                // If value is another JSON Object, write that subtree
                if (val is JSONObject)
                {
                    writeTree(((JSONObject)val).getAllEntries(), tabIndex + 1, true);
                }
                else // If value is not a nested JSON, just write it surrounded by double quotes
                {
                    streamWriter.Write("\"");
                    streamWriter.Write((string)val);
                    streamWriter.Write("\"");
                    streamWriter.WriteLine("");
                }
                if (!isNestedJson)
                {
                    streamWriter.WriteLine("");
                }
            }
            // add spaces to look like tabs
            for (int i = 0; i < tabIndex - 1; i++)
            {
                streamWriter.Write("    ");
                if (i == tabIndex - 2 && !isNestedJson)
                {
                    // Prefix key-value pairs with a "+" sign for readability
                    streamWriter.Write("+");
                }
            }
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

                // If the value of the key-value pair is a string, write it to the file surrounded by double quotation marks
                Object val = kvp.getVal();
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
                    streamWriter.WriteLine("}" + (i != keyValuePairs.Count - 1 ? "," : ""));
                }
            }
        }
    }
}
