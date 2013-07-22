using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.Adventure.Worlds
{
    public class MapEnvironment : EntityEnvironment
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapEnvironment"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        public MapEnvironment(Map map)
        {
            this.Map = map;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the map.
        /// </summary>
        public Map Map { get; private set; }
        #endregion
    }
}
