using System.Collections.Generic;
using System.Linq;
using Xemio.Adventure.Worlds.Cameras;
using Xemio.Adventure.Worlds.Events;
using Xemio.Adventure.Worlds.TileEngine;
using Xemio.Adventure.Worlds.TileEngine.Components;
using Xemio.Adventure.Worlds.TileEngine.Tiles;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Events.Logging;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.Adventure.Worlds
{
    public class Map
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="header">The header.</param>
        /// <param name="tileSets">The tile sets.</param>
        /// <param name="properties">The properties.</param>
        public Map(MapHeader header, IEnumerable<TileSet> tileSets, MapProperties properties)
        {
            this.Header = header;

            this.Width = properties.Width;
            this.Height = properties.Height;
            this.LayerCount = properties.LayerCount;

            this.TileWidth = properties.TileWidth;
            this.TileHeight = properties.TileHeight;

            this.TileSets = tileSets.ToList();

            this.Fields = new Field[this.Width, this.Height, this.LayerCount];
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    for (int z = 0; z < this.LayerCount; z++)
                    {
                        this.Fields[x, y, z] = new Field(this, x, y, z);
                    }
                }
            }

            this.Environment = new EntityEnvironment();
            this.Renderer = new MapRenderer(this);

            this._eventManager = XGL.Components.Get<EventManager>();
        }
        #endregion

        #region Fields
        private readonly EventManager _eventManager;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the header.
        /// </summary>
        public MapHeader Header { get; private set; }
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
        public ICamera ActiveCamera { get; set; }
        /// <summary>
        /// Gets the renderer.
        /// </summary>
        public MapRenderer Renderer { get; private set; }
        /// <summary>
        /// Gets the maximum tile index.
        /// </summary>
        public int MaxTileIndex
        {
            get { return this.TileSets.Sum(tileset => tileset.Tiles.Count); }
        }
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
                this._eventManager.Publish(new MapBoundaryLoggingEvent(x, y, z));
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
                this._eventManager.Publish(new MapBoundaryLoggingEvent(x, y, z));
                return;
            }

            TileSet tileSet = this.GetTileSet(tileIndex);
            if (tileSet == null)
            {
                this._eventManager.Publish(new TilesetNotFoundLoggingEvent(tileIndex));
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
