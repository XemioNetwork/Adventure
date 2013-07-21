using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.TileEngine;
using Xemio.Adventure.Worlds.TileEngine.Components;
using Xemio.Adventure.Worlds.TileEngine.Tiles;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.Adventure.Worlds
{
    public class MapRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapRenderer"/> class.
        /// </summary>
        /// <param name="world">The world.</param>
        public MapRenderer(Map world)
        {
            this.World = world;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the world.
        /// </summary>
        public Map World { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Renders the specified world.
        /// </summary>
        public void Render()
        {
            GraphicsDevice graphicsDevice = XGL.Components.Get<GraphicsDevice>();
            DisplayMode displayMode = graphicsDevice.DisplayMode;

            int tilesX = displayMode.Width / this.World.TileWidth + 2;
            int tilesY = displayMode.Height / this.World.TileHeight + 2;

            Vector2 cameraPosition = Vector2.Zero;
            if (this.World.ActiveCamera != null)
            {
                cameraPosition = this.World.ActiveCamera.Position;
            }

            int offsetX = (int)cameraPosition.X / (int)this.World.TileWidth;
            int offsetY = (int)cameraPosition.Y / (int)this.World.TileHeight;

            graphicsDevice.RenderManager.Translate(-cameraPosition);
            for (int x = 0; x < tilesX; x++)
            {
                for (int y = 0; y < tilesY; y++)
                {
                    for (int z = 0; z < this.World.LayerCount; z++)
                    {
                        Field field = this.World.GetField(offsetX + x, offsetY + y, z);

                        if (field.Reference == null)
                            continue;

                        foreach (ITileComponent tileComponent in field.Reference.Tile.Components)
                        {
                            tileComponent.Render(field);
                        }
                    }
                }
            }

            this.World.Environment.Render();
            graphicsDevice.RenderManager.Translate(Vector2.Zero);
        }
        #endregion
    }
}
