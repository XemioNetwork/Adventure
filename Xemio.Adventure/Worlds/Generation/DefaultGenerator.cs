using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.Adventure.Worlds.Generation
{
    public class DefaultGenerator : IMapGenerator
    {
        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id
        {
            get { return "Default"; }
        }
        /// <summary>
        /// Generates the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="seed">The seed.</param>
        public void Generate(Map map, string seed)
        {
        }
        #endregion
    }
}
