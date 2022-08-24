using System;
using System.Collections.Generic;
using System.Linq;

namespace JSONProjectUI
{

    /// <summary>
    /// Class responsible for generating JSONObject(s)
    /// </summary>
    internal class ObjectFactory
    {
        /// <summary>
        /// Create a JSONObject from a given string
        /// </summary>
        /// <param name="str">input string representing a json</param>
        /// <returns>json object</returns>
        /// <exception cref="Exception">thrown when input string is invalid json format and cannot be converted</exception>
        public static JSONObject generateJsonObject(string str)
        {
            JSONObject jsonObject = new JSONObject();

            // get indexes for special characters
            Dictionary<int, int> bracketsDict = JSONParser.getBracketsDict(str);
            List<int> openBracketIndexes = bracketsDict.Keys.ToList();
            List<int> closingBracketIndexes = bracketsDict.Values.ToList();
            List<int> quoteIndexes = JSONParser.getAllIndexesOfChar(str, '"');
            List<int> commaIndexes = JSONParser.getAllIndexesOfChar(str, ',');

            // iterate through each char in the input str representing a json object
            int currIndex = 0;
            while (currIndex < str.Length)
            {
                if (openBracketIndexes.Contains(currIndex) || currIndex == 0) // start of object
                {
                    currIndex++;
                    continue;
                } else if (closingBracketIndexes.Contains(currIndex) && currIndex >= str.Length - 1) // end of object
                {
                    return jsonObject;
                }

                if (str[currIndex] == ' ') // skip over whitespace
                {
                    currIndex++;
                    continue;
                }

                // detect and handle key
                string key = getString(str, ref quoteIndexes, currIndex);
                if (String.IsNullOrEmpty(key))
                {
                    currIndex++;
                    continue;
                }

                // determine if value is string or json obj and get new index after processing
                ValType valType = getValType(commaIndexes, openBracketIndexes, closingBracketIndexes, quoteIndexes, ref currIndex, key, str);
                if (valType == ValType.STRING) // add the string key-value to the json object
                {
                    addStringValueToJsonObj(quoteIndexes, ref commaIndexes, ref jsonObject, ref currIndex, key, str);
                } else if (valType == ValType.JSON) // add the json object
                {
                    addJsonValueToJsonObj(bracketsDict, ref quoteIndexes, ref commaIndexes, ref jsonObject, ref currIndex, key, str);
                } else
                {
                    currIndex++;
                }
            }
            return jsonObject;
        }

        /// <summary>
        /// Determine what type of value the current index is marking the beginning of
        /// </summary>
        /// <param name="commaIndexes">list of all indexes commas appear in str</param>
        /// <param name="openBracketIndexes">list of all indexes open brackets appear in str</param>
        /// <param name="closingBracketIndexes">list of all indexes closed brackets appear in str</param>
        /// <param name="quoteIndexes">list of all indexes quotes appear in str</param>
        /// <param name="currIndex">current index searching in str</param>
        /// <param name="key">key corresponding to the value</param>
        /// <param name="str">input string of json object being created</param>
        /// <returns>value type</returns>
        private static ValType getValType(List<int> commaIndexes, List<int> openBracketIndexes, List<int> closingBracketIndexes, 
            List<int> quoteIndexes, ref int currIndex, string key, string str)
        {
            if (!commaIndexes.Any() && closingBracketIndexes.Count <= 0) // no more commas but still data in the file
            {
                return ValType.JSON;
            }
            else
            {
                // determine if there is a pair of quotes before the next comma or an opening bracket
                int closingQuoteIndex = currIndex + (key.Length + 1); // + 2 for quotes
                currIndex += String.IsNullOrEmpty(key) ? 1 : key.Length + 2;
                int nextCommaIndex = commaIndexes.Count() > 0 ? commaIndexes.First() : str.Length;
                for (int i = closingQuoteIndex + 1; i < nextCommaIndex; i++)
                {
                    if (quoteIndexes.Contains(i))
                    {
                        //valIsString = true;
                        currIndex = i; // account for opening quote for string value
                        return ValType.STRING; 
                    }
                    else if (openBracketIndexes.Contains(i))
                    {
                        //valIsJsonObj = true;
                        currIndex = i; // account for opening bracket of json object
                        return ValType.JSON;
                    }
                }
            }
            return ValType.OTHER;
        }

        /// <summary>
        /// Add a string value to the json object
        /// </summary>
        /// <param name="quoteIndexes">list of all indexes quotes appear in str</param>
        /// <param name="commaIndexes">list of all indexes commas appear in str</param>
        /// <param name="jsonObject">JSONObject to add to</param>
        /// <param name="currIndex">current index searching in str</param>
        /// <param name="key">key of value being added</param>
        /// <param name="str">input string of json being created</param>
        private static void addStringValueToJsonObj(List<int> quoteIndexes, ref List<int> commaIndexes, 
            ref JSONObject jsonObject, ref int currIndex, string key, string str)
        {
            string strVal = getString(str, ref quoteIndexes, currIndex);
            KeyValuePair kvp = new KeyValuePair(key, strVal);
            jsonObject.addKeyValuePair(kvp);
            currIndex += strVal.Length + 2;
            if (commaIndexes.Count > 0)
            {
                commaIndexes.RemoveAt(0);
            }
        }

        /// <summary>
        /// Add a JSONObject value to the json object
        /// </summary>
        /// <param name="bracketsDict">dictionary of opening and corresponding closing brackets</param>
        /// <param name="quoteIndexes">list of all indexes quotes appear in str</param>
        /// <param name="commaIndexes">list of all indexes commas appear in str</param>
        /// <param name="jsonObject">JSONObject to add to</param>
        /// <param name="currIndex">current index searching in str</param>
        /// <param name="key">key of value being added</param>
        /// <param name="str">input string of json being created</param>
        private static void addJsonValueToJsonObj(Dictionary<int, int> bracketsDict, ref List<int> quoteIndexes, ref List<int> commaIndexes,
            ref JSONObject jsonObject, ref int currIndex, string key, string str)
        {
            int closingBracketIndex = bracketsDict[currIndex];
            int strLength = closingBracketIndex - currIndex + 1;
            string remainingStr = str.Substring(currIndex, strLength);
            JSONObject jsonObj2 = generateJsonObject(remainingStr);
            currIndex += remainingStr.Length; // skip over the newly added jsonobj

            // remove quotes from base json after processing subjson
            List<KeyValuePair> currkvps = jsonObj2.getAllEntries();
            for (int i = 0; i < currkvps.Count; i++)
            {
                for (int j = 0; j < 4; j++) // remove quotes from key-value strings in nested json obj
                {
                    quoteIndexes.RemoveAt(0);
                }
                if (i != currkvps.Count - 1)
                {
                    commaIndexes.RemoveAt(0); // remove separation commas from key-value strings in nested json obj
                }
            }
            KeyValuePair kvp2 = new KeyValuePair(key, jsonObj2);
            jsonObject.addKeyValuePair(kvp2);
            if (str[currIndex] == ',') // skip over comma at end of nested object if present
            {
                commaIndexes.RemoveAt(0);
                currIndex++;
            }
        }

        /// <summary>
        /// Get a string contained between a pair of quotation marks
        /// </summary>
        /// <param name="str">base string to search through</param>
        /// <param name="quoteIndexes">list of all indexes of quotation marks</param>
        /// <param name="currIndex">current index in base string search</param>
        /// <returns></returns>
        private static string getString(string str, ref List<int> quoteIndexes, int currIndex)
        {
            int openQuoteIndex = quoteIndexes.Contains(currIndex) ? currIndex : -1;
            if (openQuoteIndex == -1)
            {
                return String.Empty;
            }
            quoteIndexes.Remove(currIndex);
            int closingQuoteIndex = quoteIndexes.First();
            quoteIndexes.Remove(closingQuoteIndex);
            int keyLength = closingQuoteIndex - openQuoteIndex;
            return str.Substring(openQuoteIndex + 1, keyLength - 1);
        }

        /// <summary>
        /// Types of values inferred from string
        /// </summary>
        enum ValType
        {
            JSON,
            STRING,
            OTHER
        }
    }
}
