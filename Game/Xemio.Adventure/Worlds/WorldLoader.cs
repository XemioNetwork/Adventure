using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Serialization;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Content;

namespace Xemio.Adventure.Worlds
{
    public static class WorldLoader
    {
        #region Methods
        /// <summary>
        /// Creates a world out of a specified directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <returns></returns>
        public static World Load(string directory)
        {
            Stopwatch watch = Stopwatch.StartNew();
            World world = new World();

            var content = XGL.Components.Get<ContentManager>();

            foreach (string file in content.FileSystem.GetFiles(directory))
            {
                world.Maps.Add(content.Load<Map, FormatReader<Map>>(file));
            }

            watch.Stop();

            Debug.WriteLine("--------------------");
            Debug.WriteLine("Loaded world in {0}ms", watch.Elapsed.TotalMilliseconds);

            return world;
        }
        #endregion
    }
}
