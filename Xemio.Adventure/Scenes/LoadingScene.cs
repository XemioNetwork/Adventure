using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Properties;
using Xemio.Adventure.Worlds;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.Adventure.Scenes
{
    public class LoadingScene : Scene
    {
        #region Fields
        private ITexture _loadingTexture;

        private float _alpha;
        private float _screenAlpha;

        private bool _loaded;
        #endregion
        
        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._screenAlpha = 1.0f;

            this._loadingTexture = this.TextureFactory.CreateTexture(
                "loading", Resources.ResourceManager);
        }
        /// <summary>
        /// Ticks the specified elapsed.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            if (this._alpha >= 1.0f && !this._loaded)
            {
                World world = WorldLoader.Load("./Maps/");
                world.ChangeMap("test");

                this.SceneManager.Add(new MainScene(world));
                this.BringToFront();

                this._loaded = true;
            }

            this._alpha = MathHelper.Min(1.0f, this._alpha + 0.05f);
            if (this._loaded)
            {
                this._screenAlpha = MathHelper.Max(0.0f, this._screenAlpha - 0.01f);
            }

            if (this._screenAlpha <= 0.0f)
            {
                this.SceneManager.Remove(this);
            }
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            DisplayMode displayMode = this.GraphicsDevice.DisplayMode;

            this.Geometry.FillRectangle(
                this.Geometry.Factory.CreateSolid(new Color(0, 0, 0, this._screenAlpha)),
                displayMode.Bounds);

            this.RenderManager.Render(
                this._loadingTexture,
                new Vector2(
                    displayMode.Width - this._loadingTexture.Width - 10,
                    displayMode.Height - this._loadingTexture.Height - 10),
                new Color(1.0f, 1.0f, 1.0f, this._alpha * this._screenAlpha));
        }
        #endregion
    }
}
