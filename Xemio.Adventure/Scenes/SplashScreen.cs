using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Properties;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

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
            this.RenderManager.Clear(new Color(221, 221, 221));
            this.RenderManager.Render(this._texture,
                new Vector2(
                    200 - this._texture.Width * 0.5f,
                    150 - this._texture.Height * 0.5f));

            this.Geometry.FillRectangle(
                this.Geometry.Factory.CreateSolid(new Color(0.0f, 0.0f, 0.0f, this._alpha)),
                new Rectangle(0, 0, 400, 300));
        }
        #endregion
    }
}
