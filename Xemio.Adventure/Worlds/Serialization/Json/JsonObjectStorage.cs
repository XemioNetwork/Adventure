using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Xemio.GameLibrary.Common;

namespace Xemio.Adventure.Worlds.Serialization.Json
{
    public class JsonObjectStorage : ObjectStorage
    {
        #region Methods
        /// <summary>
        /// Retrieves the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public override T Retrieve<T>(string key)
        {
            object obj = base.Retrieve(key);
            if (obj is JValue)
            {
                return (obj as JValue).Value<T>();
            }

            return base.Retrieve<T>(key);
        }
        #endregion
    }
}
