using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Generation;
using Xemio.GameLibrary.Common;

namespace Xemio.Adventure.Worlds
{
    public class MapHeader
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapHeader"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="properties">The properties.</param>
        public MapHeader(string name, ObjectStorage properties)
        {
            this.Name = name;
            this.Properties = properties;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the properties.
        /// </summary>
        public ObjectStorage Properties { get; private set; }
        #endregion
    }
}
