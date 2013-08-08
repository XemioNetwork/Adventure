using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Properties;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Geometry;

namespace Xemio.Adventure.Scenes
{
    public class SplashScreen : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashScreen"/> class.
        /// </summary>
        /// <param name="startScene">The start scene.</param>
        public SplashScreen(Scene startScene)
        {
            this._startScene = startScene;
        }
        #endregion

        #region Fields
        private float _alpha;
        private float _elapsed;

        private ITexture _texture;
        private readonly Scene _startScene;
        #endregion
        
        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._texture = this.TextureFactory.CreateTexture("xemioIntro", Resources.ResourceManager);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this._elapsed += elapsed;
            if (this._elapsed >= 2000.0f)
            {
                this._alpha = MathHelper.Min(this._alpha + 0.025f, 1.0f);
                if (this._alpha >= 1.0f)
                {
                    this.SceneManager.Remove(this);
                    this.SceneManager.Add(this._startScene);
                }
            }
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            DisplayMode displayMode = this.GraphicsDevice.DisplayMode;

            this.GraphicsDevice.Clear(new Color(221, 221, 221));

            this.RenderManager.Render(this._texture,
                                      new Vector2(
                                          displayMode.Center.X - this._texture.Width * 0.5f,
                                          displayMode.Center.Y - this._texture.Height * 0.5f));

            Color color = new Color(0, 0, 0, this._alpha);
            IBrush brush = this.Geometry.Factory.CreateSolid(color);

            this.Geometry.FillRectangle(
                brush,
                new Rectangle(0, 0, displayMode.Width, displayMode.Height));
        }
        #endregion
    }
}
