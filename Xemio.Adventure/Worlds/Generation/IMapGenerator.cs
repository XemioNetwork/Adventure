using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.Adventure.Worlds.Generation
{
    public interface IMapGenerator : ILinkable<string>
    {
        /// <summary>
        /// Generates the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="seed">The seed.</param>
        void Generate(Map map, string seed);
    }
}
