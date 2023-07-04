using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JSONProjectWPF4dot8
{
    /// <summary>
    /// Represents an object in a json
    /// </summary>
    [Serializable]
    internal class JSONObject
    {
        /// <summary>
        /// List of KeyValuePairs in this JSONObject
        /// </summary>
        private List<KeyValuePair> entries = new List<KeyValuePair>();

        /// <summary>
        /// Empty constructor
        /// </summary>
        public JSONObject()
        {
            // empty constructor
        }

        /// <summary>
        /// Create a deep copy of the JSONObject
        /// </summary>
        /// <returns>copy of JSONObject</returns>
        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }

        /// <summary>
        /// Get all key-value pairs in the json object
        /// </summary>
        /// <returns>all entries</returns>
        public List<KeyValuePair> getAllEntries()
        {
            return this.entries;
        }

        /// <summary>
        /// Find the nested json object and create a new key-value pair and add it to the json object
        /// </summary>
        /// <param name="findKey">key of nested json object to add to</param>
        /// <param name="newKey">new key to add</param>
        /// <param name="value">string value to add</param>
        /// <returns>true if add successful, else false</returns>
        public bool addKeyValuePair(string findKey, string newKey, string value)
        {
            KeyValuePair foundKvp = getKeyValuePair(findKey);
            if (foundKvp != null)
            {
                Object valueObj = foundKvp.getVal();
                if (valueObj is JSONObject)
                {
                    KeyValuePair newKvp = null;

                    // If the value contains curly braces, treat as a new JSON object
                    if (value.Contains("{") && value.Contains("}") && value.Contains(":")) // treat as new json object
                    {
                        JSONObject jsonVal = JSONParser.getJsonObject(value);
                        newKvp = new KeyValuePair(newKey, (JSONObject)jsonVal);
                    }
                    else // If the value does not contain curly braces, just treat as a string value
                    {
                        newKvp = new KeyValuePair(newKey, value);
                    }

                    // Check if there is an entry for the new key to add. If one exists, do not attempt to add it again (no duplicates)
                    if (((JSONObject)valueObj).getKeyValuePair(newKey) != null)
                    {
                        Console.WriteLine("JSONObject.addKeyValuePair -- " + newKey + " already exists in json object. Use modify method to change it.");
                        return false;
                    }

                    // Add the new key-value pair to the existing json object
                    ((JSONObject)valueObj).addKeyValuePair(newKvp);
                    int objectIndex = ObjectFinder.getIndexOfKeyValuePair(this, foundKvp);
                    this.entries[objectIndex] = (new KeyValuePair(findKey, valueObj)); // replace existing value with new object

                }
                else  // If the object to add to is not a JSON, add the key-value to the existing list of Key-Value pairs
                {
                    this.entries.Remove(foundKvp);
                    this.entries.Add(new KeyValuePair(findKey, valueObj));
                }
            }
            return true;
        }

        /// <summary>
        /// Create a new key-value pair and add it to the json object
        /// </summary>
        /// <param name="key">new key to add</param>
        /// <param name="value">string value to add</param>
        /// <returns>true if add successful, else false</returns>
        public bool addKeyValuePair(string key, string value)
        {
            if (getKeyValuePair(key) != null)
            {
                Console.WriteLine("JSONObject.addKeyValuePair -- " + key + " already exists in json object. Use modify method to change it.");
                return false;
            }
            if (this.getKeyValuePair(key) != null)
            {
                Console.WriteLine("JSONObject.addKeyValuePair -- " + key + " already exists in json object. Use modify method to change it.");
                return false;
            }

            // If the value contains curly braces, treat as a new JSON object
            if (value.Contains("\"") && value.Contains("{") && value.Contains("}") && value.Contains(":")) // treat as new json object
            {
                JSONObject jsonVal = JSONParser.getJsonObject(value);
                KeyValuePair kvp = new KeyValuePair(key, (JSONObject)jsonVal);
                addKeyValuePair(kvp);
            }
            else // If the value does not contain curly braces, just treat as a string value
            {
                KeyValuePair kvp = new KeyValuePair(key, value);
                addKeyValuePair(kvp);
            }
            return true;
        }

        /// <summary>
        /// Add a key-value pair to the jsonobject
        /// </summary>
        /// <param name="kvp">key-value pair to add</param>
        public void addKeyValuePair(KeyValuePair kvp)
        {
            entries.Add(kvp);
        }

        /// <summary>
        /// Remove a key-value pair from a jsonobject
        /// </summary>
        /// <param name="key">key corresponding to the key-value pair to remove</param>
        /// <returns>true if entry successfully removed, false if key not found or delete unsuccessful</returns>
        public bool removeKeyValuePair(string key)
        {
            // Determine if entry associated with the specified key actually exists
            bool found = getKeyValuePair(key) != null;
            if (!found)
            {
                Console.WriteLine("JSONObject.removeKeyValuePair -- Input key was not found in source file. Nothing to remove.");
                return false;
            }
            foreach (KeyValuePair kvp in entries)
            {
                if (key == kvp.getKey())
                {
                    return entries.Remove(kvp);
                }
            }
            return true;
        }

        /// <summary>
        /// Replace a value corresponding to a given key
        /// </summary>
        /// <param name="key">key corresponding to value to replace</param>
        /// <param name="replacementVal">value to substitute in</param>
        /// <returns>true if modify successful, else false</returns>
        public bool modifyKeyValuePair(string key, string replacementVal)
        {
            int keyIndex = ObjectFinder.getIndexOfKey(this, key);
            if (keyIndex != -1)
            {
                KeyValuePair kvp = new KeyValuePair(key, replacementVal);
                entries[keyIndex] = kvp;
            }
            else
            {
                Console.WriteLine("JSONObject.modifyKeyValuePair -- Input key was not found in source file. Nothing to remove.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Get the key-value pair corresponding to a given key
        /// </summary>
        /// <param name="key">key to find the key-value pair of</param>
        /// <returns>KeyValuePair corresponding to key</returns>
        public KeyValuePair getKeyValuePair(string key)
        {
            foreach (KeyValuePair kvp in entries)
            {
                if (key == kvp.getKey())
                {
                    return kvp;
                }
            }
            return null; // key not found
        }

        /// <summary>
        /// Get the total number of key-value pairs in a a JSONObject (including nested JSONObjects)
        /// </summary>
        /// <param name="jsonObj">base JSONObject</param>
        /// <returns></returns>
        public static int getNumKeyValuePairs(JSONObject jsonObj)
        {
            if (jsonObj == null)
            {
                return 0;
            }

            int count = 0;
            foreach (KeyValuePair kvp in jsonObj.getAllEntries())
            {
                if (kvp.getVal() is JSONObject)
                {
                    count += 1 + getNumKeyValuePairs((JSONObject)kvp.getVal());
                }
                else
                {
                    count++;
                }
            }
            return count;
        }
    }
}
