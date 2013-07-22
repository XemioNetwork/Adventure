using Xemio.Adventure.Worlds.TileEngine.Components;

namespace Xemio.Adventure.Worlds.TileEngine.Tiles
{
    public class DefaultTile : Tile
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTile"/> class.
        /// </summary>
        public DefaultTile()
        {
            this.Add(new RenderingComponent());
        }
        #endregion

        #region Overrides of Tile
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public override string Id
        {
            get { return "Default"; }
        }
        #endregion
    }
}
