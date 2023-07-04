using System;

namespace JSONProject
{
    /// <summary>
    /// Class used to insert/delete/modify a value in an JSONObject
    /// </summary>
    internal class ObjectModifier
    {
        /// <summary>
        /// Add a key-value pair to a json object and return the modified json object
        /// </summary>
        /// <param name="jsonObj">base json object</param>
        /// <param name="key">key to add</param>
        /// <param name="value">value to add</param>
        /// <returns>new json object</returns>
        public JSONObject addKeyValuePair(JSONObject jsonObj, string key, string value)
        {
            bool success = jsonObj.addKeyValuePair(key, value);
            if (!success)
            {
                Console.WriteLine("ObjectModifier.addKeyValuePair -- Failed to add to JSON Object");
                return null;
            }
            return jsonObj;
        }

        /// <summary>
        /// Add a key-value pair to a json object, nested in the first passed in key json object section
        /// </summary>
        /// <param name="parentKey">key of nested json object to add to</param>
        /// <param name="newChildKey">new key to add</param>
        /// <param name="value">string value to add</param>
        /// <returns>new JSONObject with new key-value pair added</returns>
        public JSONObject addKeyValuePair(JSONObject jsonObj, string parentKey, string newChildKey, string value)
        {
            KeyValuePair kvp = new KeyValuePair(newChildKey, value);
            bool success = jsonObj.addKeyValuePair(parentKey, newChildKey, value);
            if (!success)
            {
                Console.WriteLine("ObjectModifier.addKeyValuePair -- Failed to add to JSON Object");
                return null;
            }
            return jsonObj;
        }

        /// <summary>
        /// Remove a key-value pair from a json object and return the modified json object
        /// </summary>
        /// <param name="jsonObj">base json object</param>
        /// <param name="key">key of entry to delete</param>
        /// <returns>JSONObject with key-value pair deleted</returns>
        public JSONObject deleteKeyValuePair(JSONObject jsonObj, string key)
        {
            bool success = jsonObj.removeKeyValuePair(key);
            if (!success)
            {
                Console.WriteLine("ObjectModifier.deleteKeyValuePair -- Failed to delete from JSON Object");
                return null;
            }
            return jsonObj;
        }

        /// <summary>
        /// Replace a string value corresponding to a given key with a new replacement value
        /// </summary>
        /// <param name="jsonObj">json object to modify</param>
        /// <param name="key">key corresponding to changing value</param>
        /// <param name="replacementVal">new value</param>
        /// <returns>JSONObject with value modified</returns>
        public JSONObject modifyStringValue(JSONObject jsonObj, string key, string replacementVal)
        {
            bool success = jsonObj.modifyKeyValuePair(key, replacementVal);
            if (!success)
            {
                Console.WriteLine("ObjectModifier.modifyStringValue -- Failed to modify JSON Object");
                return null;
            }
            return jsonObj;
        }
    }
}
