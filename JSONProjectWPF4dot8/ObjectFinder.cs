using System.Collections.Generic;

namespace JSONProjectWPF4dot8
{
    /// <summary>
    /// Class used to find objects in JSONObject
    /// </summary>
    internal class ObjectFinder
    {

        /// <summary>
        /// Get the index in the json where a key-value pair is found
        /// </summary>
        /// <param name="jsonObj">the json object to search in</param>
        /// <param name="kvp">the key-value pair to find</param>
        /// <returns>index of key-value pair if found, else -1</returns>
        public static int getIndexOfKeyValuePair(JSONObject jsonObj, KeyValuePair kvp)
        {
            List<KeyValuePair> entries = jsonObj.getAllEntries();

            int objectIndex = -1;
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i] == kvp)
                {
                    objectIndex = i;
                    break;
                }
            }
            return objectIndex;
        }

        /// <summary>
        /// Get the index in the json object where a key is found
        /// </summary>
        /// <param name="jsonObj">the json object to search in</param>
        /// <param name="kvp">the key to find</param>
        /// <returns>index of key if found, else -1</returns>
        public static int getIndexOfKey(JSONObject jsonObj, string key)
        {
            List<KeyValuePair> entries = jsonObj.getAllEntries();
            int keyIndex = -1;
            for (int i = 0; i < entries.Count; i++)
            {
                if (entries[i].getKey() == key)
                {
                    keyIndex = i;
                    break;
                }
            }
            return keyIndex;
        }
    }
}
