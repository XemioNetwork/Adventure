using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds;
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
        public MainScene()
        {

        }
        #endregion

        #region Fields
        private World _world;
        #endregion

        #region Properties

        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            WorldParser parser = new WorldParser();
            this._world = parser.Parse(
                File.ReadAllText("Resources/world.json", Encoding.Default));

            this._world.ActiveCamera = new Camera();
        }
        public override void Tick(float elapsed)
        {
            this._world.ActiveCamera.Position += new Vector2(1, 1);
            this._world.Tick(elapsed);

            GameLoop loop = XGL.Components.Get<GameLoop>();
            Debug.WriteLine(loop.FramesPerSecond.ToString() + "fps");
        }
        public override void Render()
        {
            this._world.Renderer.Render();
        }
        #endregion
    }
}
