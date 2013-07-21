using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Xemio.GameLibrary.Common;

namespace Xemio.Adventure.Worlds.Serialization.Json
{
    public static class JsonHelper
    {
        #region Methods
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
        /// <param name="jObject">The json object.</param>
        public static ObjectStorage CreateObjectStorage(JObject jObject)
        {
            ObjectStorage storage = new ObjectStorage();
            IList<string> keys = jObject
                .Properties()
                .Select(p => p.Name)
                .ToList();

            foreach (string key in keys)
            {
                JValue value = jObject[key].Value<JValue>();

                storage.Store(
                    key, value.Value);
            }

            return storage;
        }
        #endregion
    }
}
