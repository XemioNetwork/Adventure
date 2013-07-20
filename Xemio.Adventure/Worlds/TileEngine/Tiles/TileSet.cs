using System.Collections.Generic;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds.TileEngine.Tiles
{
    public class TileSet
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TileSet"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="firstTileIndex">First index of the tile.</param>
        public TileSet(string name, int firstTileIndex)
        {
            this.Name = name;
            this.Tiles = new List<TileReference>();
            this.FirstTileIndex = firstTileIndex;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the tiles.
        /// </summary>
        public List<TileReference> Tiles { get; private set; }
        /// <summary>
        /// Gets the first index of the tile.
        /// </summary>
        public int FirstTileIndex { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether the tileset contains the specified index.
        /// </summary>
        /// <param name="tileIndex">Index of the tile.</param>
        public bool HasIndex(int tileIndex)
        {
            int listIndex = tileIndex - this.FirstTileIndex;
            return listIndex >= 0 && listIndex < this.Tiles.Count;
        }
        /// <summary>
        /// Gets the tile at the specified index.
        /// </summary>
        /// <param name="tileIndex">Index of the tile.</param>
        public TileReference GetTile(int tileIndex)
        {
            return this.Tiles[tileIndex - this.FirstTileIndex];
        }
        #endregion
    }
}
