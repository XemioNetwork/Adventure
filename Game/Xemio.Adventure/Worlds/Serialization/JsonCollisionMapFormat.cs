using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Math.Collision;

namespace Xemio.Adventure.Worlds.Serialization
{
    public class JsonCollisionMapFormat : IFormat<CollisionMap>
    {
        #region Implementation of ILinkable<string>
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="input">The input.</param>
        public CollisionMap Parse(string fileName, string input)
        {
            JObject root = JObject.Parse(input);
            ObjectStorage storage = JsonHelper.CreateObjectStorage(root);

            CollisionMap collisionMap = new CollisionMap(
                storage.Retrieve<int>("Width"),
                storage.Retrieve<int>("Height"),
                storage.Retrieve<int>("CellSize"));
            
            JArray array = storage.Retrieve<JArray>("CollisionMap");
            for (int x = 0; x < collisionMap.Width; x++)
            {
                for (int y = 0; y < collisionMap.Height; y++)
                {
                    bool value = array[x * collisionMap.Height + y].Value<bool>();
                    collisionMap.Cells[x, y] = value;
                }
            }

            return collisionMap;
        }
        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id
        {
            get { return ".json"; }
        }
        #endregion
    }
}
