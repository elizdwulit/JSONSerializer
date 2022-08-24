using System;
using System.Collections.Generic;
using System.Linq;

namespace JSONProjectWPF4dot8
{
    /// <summary>
    /// Provides a way to parse a string as a JSONObject
    /// </summary>
    internal class JSONParser
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public JSONParser()
        {
            // empty constructor
        }

        /// <summary>
        /// Parse and return a json object from a given string
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>json object of string</returns>
        public static JSONObject getJsonObject(string str)
        {
            JSONObject jsonObj = null;
            try
            {
                validateJsonString(str);
                jsonObj = ObjectFactory.generateJsonObject(str);

                // can only validate num colons after key-value pairs are generated
                int colonCount = str.Count(s => s == ':');
                int kvpCount = JSONObject.getNumKeyValuePairs(jsonObj);
                if (colonCount > kvpCount)
                {
                    throw new Exception("Input json string invalid. Too many colons.");
                }
                else if (colonCount < kvpCount)
                {
                    throw new Exception("Input json string invalid. Missing one or more colons.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("JSONParser.getJsonObject -- Exception generating json object: " + e.Message);
                return null;
            }

            return jsonObj;
        }

        /// <summary>
        /// Get a list of all indexes where a given character appears in a given string
        /// </summary>
        /// <param name="str">String to search</param>
        /// <param name="c">Character to find</param>
        /// <returns>List of indexes in the source string that character appears</returns>
        public static List<int> getAllIndexesOfChar(string str, char c)
        {
            List<int> indexes = new List<int>();

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == c)
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }

        /// <summary>
        /// Create  dictionary of opening curly braces and their corresponding curly braces
        /// </summary>
        /// <param name="str">string to create dictionary from</param>
        /// <returns>dictonary (index of opening bracket, index of corresponding closing bracket)</returns>
        public static Dictionary<int, int> getBracketsDict(string str)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < str.Length; i++)
            {
                switch (str[i])
                {
                    case '{':
                        stack.Push(i);
                        break;
                    case '}':
                        int index = stack.Any() ? stack.Pop() : -1;
                        if (index != -1)
                        {
                            dict.Add(index, i);
                        }
                        break;
                    default:
                        break;
                }
            }

            return dict;
        }

        /// <summary>
        /// Determines if given string can generate a JSONObject
        /// </summary>
        /// <param name="str">input string</param>
        /// <returns>true if string can be translated, else false</returns>
        /// <exception cref="Exception">exception if calculations throw error</exception>
        private static bool validateJsonString(string str)
        {
            // get and validate brackets
            int openBracketCount = str.Count(s => s == '{');
            int closedBracketCount = str.Count(s => s == '}');
            if (openBracketCount != closedBracketCount)
            {
                throw new Exception("Input json string invalid. Not all brackets are complete.");
            }

            // get and verify each quote is part of a pair
            int quoteCount = str.Count(s => s == '"');
            if (quoteCount % 2 != 0)
            {
                throw new Exception("Input json string invalid. Not an even amount of quotation marks.");
            }

            return true;
        }
    }
}
