using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Xemio.GameLibrary.Common;

namespace Xemio.Adventure.Worlds.Serialization
{
    public static class JsonHelper
    {
        #region Methods
        /// <summary>
        /// Gets the keys for the specified object.
        /// </summary>
        /// <param name="obj">The obj.</param>
        public static IList<string> GetKeys(JObject obj)
        {
            return obj
                .Properties()
                .Select(p => p.Name)
                .ToList();
        }
        /// <summary>
        /// Creates an object storage.
        /// </summary>
        /// <param name="token">The token.</param>
        public static ObjectStorage CreateObjectStorage(JToken token)
        {
            return JsonHelper.CreateObjectStorage(token.Value<JObject>());
        }
        /// <summary>
        /// Creates an object storage.
        /// </summary>
        /// <param name="obj">The json object.</param>
        public static ObjectStorage CreateObjectStorage(JObject obj)
        {
            ObjectStorage storage = new ObjectStorage();
            IList<string> keys = JsonHelper.GetKeys(obj);

            foreach (string key in keys)
            {
                JToken token = obj[key];

                if (token is JObject)
                {
                    storage.Store(key, token as JObject);
                }
                else
                {
                    JValue value = token.Value<JValue>();
                    storage.Store(key, value.Value);
                }
            }

            return storage;
        }
        #endregion
    }
}
