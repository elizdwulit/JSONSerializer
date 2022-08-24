using System;
using System.Collections.Generic;

namespace JSONProject
{   
    /// <summary>
    /// Class containing the Main method used to run the program
    /// </summary>
    internal class JSONSerializer
    {
        public static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("No command specified. Exiting.");
                Console.WriteLine("Supported Commands:");
                Console.WriteLine("sourcefilename add <key> <newKey> <value>");
                Console.WriteLine("sourcefilename add <key> <newKey> <value>");
                Console.WriteLine("sourcefilename modify <key> <replacement string value>");
                Console.WriteLine("sourcefilename delete <key>");
                Console.WriteLine();
                return;
            }

            // check if file name provided
            string inputFileName = args[0];
            if (String.IsNullOrEmpty(inputFileName)) {
                Console.WriteLine("No input file name provided");
                return;
            }

            // check that valid command provided
            string command = args[1];
            if (!command.Equals("add") && !command.Equals("modify") && !command.Equals("delete"))
            {
                Console.WriteLine("ERROR - Unknown command");
                return;
            }

            JSONObject jsonObj = null;
            switch (command)
            {
                case "add":
                    jsonObj = add(args, inputFileName);
                    break;
                case "modify":
                    jsonObj = modify(args, inputFileName);
                    break;
                case "delete":
                    jsonObj = delete(args, inputFileName);
                    break;
                default:
                    break;
            }
            if (jsonObj != null)
            {
                printJsonToConsole(jsonObj, 1);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Create and add to a JSONObject
        /// </summary>
        /// <param name="args">command line args</param>
        /// <param name="inputFileName">name of input text file</param>
        /// <returns>JSONObject</returns>
        private static JSONObject add(string[] args, string inputFileName)
        {
            if (args.Length != 4 && args.Length != 5)
            {
                Console.WriteLine("Json Add - Not enough arguments");
                return null;
            }
            Console.WriteLine("Adding...");

            ObjectModifier objModifier = new ObjectModifier();
            FileReader fileReader = new FileReader();
            FileSaver fileSaver = new FileSaver();
            string fileOutputName = string.Empty;

            string inputText = fileReader.convertFileToString(inputFileName);
            JSONObject jsonObj = JSONParser.getJsonObject(inputText);
            if (jsonObj != null)
            {
                if (args.Length == 4) // just adding to base json
                {
                    fileOutputName = "jsonAddResultEnd";
                    string newKey = args[2];
                    string value = args[3];
                    jsonObj = objModifier.addKeyValuePair(jsonObj, newKey, value);
                }
                else // add to primary list
                {
                    string findKey = args[2];
                    string newKey = args[3];
                    string value = args[4];
                    fileOutputName = "jsonAddResultNested";
                    jsonObj = objModifier.addKeyValuePair(jsonObj, findKey, newKey, value);
                }

                if (jsonObj != null)
                {
                    fileSaver.saveFile(jsonObj, fileOutputName);
                    Console.WriteLine(jsonObj != null ? "Result saved to file: " + fileOutputName : "");
                    Console.WriteLine("Modified JSON:");
                }
                return jsonObj;
            }
            return null;
        }

        /// <summary>
        /// Create and modify a JSONObject
        /// </summary>
        /// <param name="args">command line args</param>
        /// <param name="inputFileName">name of input text file</param>
        /// <returns>JSONObject</returns>
        private static JSONObject modify(string[] args, string inputFileName)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Json Modify - Not enough arguments");
                return null;
            }

            Console.WriteLine("Modifying...");

            ObjectModifier objModifier = new ObjectModifier();
            FileReader fileReader = new FileReader();
            FileSaver fileSaver = new FileSaver();

            string inputText = fileReader.convertFileToString(inputFileName);
            JSONObject jsonObj = JSONParser.getJsonObject(inputText);
            string fileOutputName = "jsonModifyResult";
            if (jsonObj != null)
            {
                string key = args[2];
                string value = args[3];
                jsonObj = objModifier.modifyStringValue(jsonObj, key, value);
                if (jsonObj != null)
                {
                    fileSaver.saveFile(jsonObj, fileOutputName);
                    Console.WriteLine(jsonObj != null ? "Result saved to file: " + fileOutputName : "");
                    Console.WriteLine("Modified JSON:");
                }
                return jsonObj;
            }
            else
            {
                Console.WriteLine("Failed to parse JSON.");
                return null;
            }
        }

        /// <summary>
        /// Create and delete from a JSONObject
        /// </summary>
        /// <param name="args">command line args</param>
        /// <param name="inputFileName">name of input text file</param>
        /// <returns>JSONObject</returns>
        private static JSONObject delete(string[] args, string inputFileName)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Json Delete - Not enough arguments");
                return null;
            }
            Console.WriteLine("Deleting...");

            ObjectModifier objModifier = new ObjectModifier();
            FileReader fileReader = new FileReader();
            FileSaver fileSaver = new FileSaver();
            string fileOutputName = string.Empty;

            string inputText = fileReader.convertFileToString(inputFileName);
            JSONObject jsonObj = JSONParser.getJsonObject(inputText);
            fileOutputName = "jsonDeleteResult";
            if (jsonObj != null)
            {
                string key = args[2];
                jsonObj = objModifier.deleteKeyValuePair(jsonObj, key);
                if (jsonObj != null)
                {
                    fileSaver.saveFile(jsonObj, fileOutputName);
                    Console.WriteLine(jsonObj != null ? "Result saved to file: " + fileOutputName : "");
                    Console.WriteLine("Modified JSON:");
                }
                return jsonObj;
            }
            else
            {
                Console.WriteLine("Failed to parse JSON.");
                return null;
            }
        }

        /// <summary>
        /// Print a json object to the console
        /// </summary>
        /// <param name="jsonObj">json object to print</param>
        /// <param name="tabIndex"></param>
        private static void printJsonToConsole(JSONObject jsonObj, int tabIndex)
        {
            List<KeyValuePair> entries = jsonObj.getAllEntries();

            int currEntry = 0;
            Console.WriteLine("{");
            foreach (KeyValuePair kvp in entries)
            {
                currEntry = currEntry + 1;
                for (int i = 0; i < tabIndex; i++)
                {
                    Console.Write("\t");
                }

                Console.Write("\"");
                Console.Write(kvp.getKey());
                Console.Write("\"");
                Console.Write(": ");
                
                Object val = kvp.getVal();
                if (val is JSONObject) {
                    printJsonToConsole((JSONObject)val, tabIndex + 1);
                    Console.Write(currEntry != entries.Count ? "," : "");
                } 
                else
                {
                    Console.Write("\"");
                    Console.Write((string)val);
                    Console.Write("\"");
                    Console.Write(currEntry != entries.Count ? "," : "");
                }
                Console.Write("\n");
            }
            for (int i = 0; i < tabIndex - 1; i++)
            {
                Console.Write("\t");
            }
            Console.Write("}");
        }
    }
}
