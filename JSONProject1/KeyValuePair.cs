using System;

namespace JSONProject
{
    /// <summary>
    /// Represents a key-value entry in a json
    /// </summary>
    internal class KeyValuePair
    {
        private string key;
        private Object val;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public KeyValuePair()
        {
            // empty constructor
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value corresponding to key</param>
        public KeyValuePair(string key, Object value)
        {
            this.key = key;
            this.val = value;
        }

        /// <summary>
        /// Get key
        /// </summary>
        /// <returns>key</returns>
        public string getKey()
        {
            return this.key;
        }

        /// <summary>
        /// Sets key
        /// </summary>
        /// <param name="key">value to set as key</param>
        public void setKey(string key)
        {
            this.key = key;
        }

        /// <summary>
        /// Get value
        /// </summary>
        /// <returns>value</returns>
        public Object getVal()
        {
            return this.val;
        }

        /// <summary>
        /// Sets value
        /// </summary>
        /// <param name="value">value to set as value</param>
        public void setVal(Object value)
        {
            this.val = value;
        }

    }
}
