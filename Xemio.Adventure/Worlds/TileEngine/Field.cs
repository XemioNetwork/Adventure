using Xemio.Adventure.Worlds.TileEngine.Tiles;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds.TileEngine
{
    /// <summary>
    /// Represents a field on the map.
    /// </summary>
    public class Field
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Field"/> class.
        /// </summary>
        /// <param name="world">The map.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="layer">The layer.</param>
        public Field(Map world, int x, int y, int layer)
        {
            this.World = world;
            this.X = x;
            this.Y = y;
            this.Layer = layer;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the X.
        /// </summary>
        public int X { get; private set; }
        /// <summary>
        /// Gets the Y.
        /// </summary>
        public int Y { get; private set; }
        /// <summary>
        /// Gets the layer.
        /// </summary>
        public int Layer { get; private set; }
        /// <summary>
        /// Gets the world.
        /// </summary>
        public Map World { get; private set; }
        /// <summary>
        /// Gets or sets the reference.
        /// </summary>
        public TileReference Reference { get; internal set; }
        #endregion
    }
}
