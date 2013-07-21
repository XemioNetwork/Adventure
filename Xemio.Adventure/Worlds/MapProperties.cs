using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.Adventure.Worlds
{
    public class MapProperties
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapProperties"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="tileWidth">Width of the tile.</param>
        /// <param name="tileHeight">Height of the tile.</param>
        /// <param name="layers">The layers.</param>
        public MapProperties(int width, int height, int tileWidth, int tileHeight, int layers)
        {
            this.Width = width;
            this.Height = height;

            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;

            this.LayerCount = layers;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; private set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Gets the width of a tile.
        /// </summary>
        public int TileWidth { get; private set; }
        /// <summary>
        /// Gets the height of a tile.
        /// </summary>
        public int TileHeight { get; private set; }
        /// <summary>
        /// Gets the layer count.
        /// </summary>
        public int LayerCount { get; private set; }
        #endregion
    }
}
