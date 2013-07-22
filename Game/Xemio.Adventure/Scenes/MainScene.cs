using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds;
using Xemio.Adventure.Worlds.Entities;
using Xemio.Adventure.Worlds.Serialization;
using Xemio.Adventure.Worlds.TileEngine.Tiles;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Game.Timing;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering.GDIPlus;

namespace Xemio.Adventure.Scenes
{
    public class MainScene : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainScene"/> class.
        /// </summary>
        /// <param name="world">The world.</param>
        public MainScene(World world)
        {
            var config = this.Content.Load<EntityConfiguration, FormatReader<EntityConfiguration>>(
                "./Resources/Entities/clotharmor.json");

            this._world = world;
            this._world.ChangeMap("world");
        }
        #endregion

        #region Fields
        private readonly World _world;
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._world.Tick(elapsed);
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            this._world.Render();
        }
        #endregion
    }
}
