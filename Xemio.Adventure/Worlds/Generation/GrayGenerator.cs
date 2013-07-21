using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Randomization;

namespace Xemio.Adventure.Worlds.Generation
{
    public class GrayGenerator : IMapGenerator
    {
        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id
        {
            get { return "GrayGenerator"; }
        }
        /// <summary>
        /// Generates the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="seed">The seed.</param>
        public void Generate(Map map, string seed)
        {
            IRandom random = new RandomProxy(seed);
            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    for (int z = 0; z < map.LayerCount; z++)
                    {
                        map.SetField(x, y, z, 4);
                    }
                }
            }
        }
        #endregion
    }
}
