using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.Adventure.Worlds.Serialization
{
    public class MapReader : ContentReader<Map>
    {
        #region Overrides of ContentReader<World>
        /// <summary>
        /// Reads the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public override Map Read(BinaryReader reader)
        {
            StreamReader streamReader = new StreamReader(reader.BaseStream, Encoding.Default);
            string input = streamReader.ReadToEnd();

            FileStream fileStream = reader.BaseStream as FileStream;
            string id = ".json";

            if (fileStream != null)
            {
                id = Path.GetExtension(fileStream.Name);
            }

            var implementations = XGL.Components.Get<ImplementationManager>();
            IMapFormat format = implementations.Get<string, IMapFormat>(id);

            Stopwatch watch = Stopwatch.StartNew();
            Map map = format.Parse(input);

            watch.Stop();
            Debug.WriteLine("Loaded map '{0}' ({1}ms)", map.Header.Name, watch.Elapsed.TotalMilliseconds);

            return map;
        }
        #endregion
    }
}
