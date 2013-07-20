using System.Collections.Generic;
using System.Linq;
using Xemio.Adventure.Worlds.TileEngine;
using Xemio.Adventure.Worlds.TileEngine.Components;
using Xemio.Adventure.Worlds.TileEngine.Events;
using Xemio.Adventure.Worlds.TileEngine.Tiles;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.Adventure.Worlds
{
    public class World
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        /// <param name="tileSets">The tile sets.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="tileWidth">Width of the tile.</param>
        /// <param name="tileHeight">Height of the tile.</param>
        /// <param name="layers">The layers.</param>
        public World(IEnumerable<TileSet> tileSets, int width, int height, int tileWidth, int tileHeight, int layers)
        {
            this._eventManager = XGL.Components.Get<EventManager>();

            this.Width = width;
            this.Height = height;
            this.LayerCount = layers;

            this.TileWidth = tileWidth;
            this.TileHeight = tileHeight;

            this.TileSets = tileSets.ToList();

            this.Fields = new Field[width, height, layers];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < layers; z++)
                    {
                        this.Fields[x, y, z] = new Field(this, x, y, z);
                    }
                }
            }

            this.Environment = new EntityEnvironment();
            this.Renderer = new WorldRenderer(this);
        }
        #endregion

        #region Fields
        private readonly EventManager _eventManager;
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
        /// Gets the layer count.
        /// </summary>
        public int LayerCount { get; private set; }
        /// <summary>
        /// Gets the width of the tile.
        /// </summary>
        public int TileWidth { get; private set; }
        /// <summary>
        /// Gets the height of the tile.
        /// </summary>
        public int TileHeight { get; private set; }
        /// <summary>
        /// Gets the tile sets.
        /// </summary>
        public List<TileSet> TileSets { get; private set; }
        /// <summary>
        /// Gets the fields.
        /// </summary>
        public Field[,,] Fields { get; private set; }
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        /// <summary>
        /// Gets or sets the active camera.
        /// </summary>
        public Camera ActiveCamera { get; set; }
        /// <summary>
        /// Gets the renderer.
        /// </summary>
        public WorldRenderer Renderer { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Gets a corresponding tileset for the specified tile index.
        /// </summary>
        /// <param name="tileIndex">Index of the tile.</param>
        public TileSet GetTileSet(int tileIndex)
        {
            return this.TileSets.FirstOrDefault(t => t.HasIndex(tileIndex));
        }
        /// <summary>
        /// Gets the field at the specified position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public Field GetField(int x, int y, int z)
        {
            if (x < 0 || x >= this.Width ||
                y < 0 || y >= this.Height ||
                z < 0 || z >= this.LayerCount)
            {
                this._eventManager.Publish(new FieldOutOfRangeLoggingEvent(x, y, z));
                return new Field(this, x, y, z);
            }

            return this.Fields[x, y, z];
        }
        /// <summary>
        /// Sets the field at the specified position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        /// <param name="tileIndex">Index of the tile.</param>
        public void SetField(int x, int y, int z, int tileIndex)
        {
            if (x < 0 || x >= this.Width ||
                y < 0 || y >= this.Height ||
                z < 0 || z >= this.LayerCount)
            {
                this._eventManager.Publish(new FieldOutOfRangeLoggingEvent(x, y, z));
                return;
            }

            TileSet tileSet = this.GetTileSet(tileIndex);
            if (tileSet == null)
            {
                this._eventManager.Publish(new TileSetNotFoundLoggingEvent(tileIndex));
                return;
            }

            this.Fields[x, y, z].Reference = tileSet.GetTile(tileIndex);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            this.Environment.Tick(elapsed);

            foreach (TileSet tileSet in this.TileSets)
            {
                foreach (TileReference reference in tileSet.Tiles)
                {
                    reference.Animation.Tick(elapsed);
                }
            }

            foreach (Field field in this.Fields)
            {
                if (field.Reference == null) continue;

                foreach (ITileComponent tileComponent in field.Reference.Tile.Components)
                {
                    tileComponent.Tick(field, elapsed);
                }
            }
        }
        #endregion
    }
}
