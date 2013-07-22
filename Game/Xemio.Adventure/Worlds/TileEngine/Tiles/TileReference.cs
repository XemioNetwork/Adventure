using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds.TileEngine.Tiles
{
    public class TileReference
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TileReference"/> class.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="animation">The animation.</param>
        /// <param name="properties">The properties.</param>
        public TileReference(Tile tile, Animation animation, ObjectStorage properties)
        {
            this.Tile = tile;
            this.Animation = animation.CreateInstance();
            this.Properties = properties;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the tile.
        /// </summary>
        public Tile Tile { get; private set; }
        /// <summary>
        /// Gets the animation.
        /// </summary>
        public AnimationInstance Animation { get; private set; }
        /// <summary>
        /// Gets the properties.
        /// </summary>
        public ObjectStorage Properties { get; private set; }
        #endregion
    }
}
