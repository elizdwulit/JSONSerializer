using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JSONProject
{
    /// <summary>
    /// Class responsible for saving an output to a new file
    /// </summary>
    internal class FileSaver
    {
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
        /// <returns>the name of the file that was saved</returns>
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
                sb.Append(outputFileName);
                sb.Append(".txt"); // save as txt file
                fileSaveName = sb.ToString();

                // write to new file
                FileStream fileStream = new FileStream(fileSaveName, FileMode.Create, FileAccess.Write);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.BaseStream.Seek(0, SeekOrigin.End);

                // for each line in json object write
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

                // print val
                Object val = kvp.getVal();
                if (val is string)
                {
                    streamWriter.Write("\"" + val);
                    if (i != keyValuePairs.Count - 1)
                    {
                        streamWriter.WriteLine("\",");
                    }
                }
                else // val is JSONObject
                {
                    streamWriter.WriteLine("{");
                    writeJson((val as JSONObject).getAllEntries());
                    streamWriter.WriteLine("}" + (i != keyValuePairs.Count - 1 ? "," : ""));
                }
            }
        }
    }
}
