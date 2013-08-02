using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Scenes
{
    public class HUD : Scene
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HUD"/> class.
        /// </summary>
        public HUD()
        {
        }
        #endregion

        #region Fields
        private ITexture _bar;

        private SpriteSheet _experience;
        private SpriteSheet _experienceEmpty;

        private ITexture _heart;
        private ITexture _heartEmpty;
        private ITexture _heartHalf;

        private ITexture _slot;
        #endregion

        #region Methods
        /// <summary>
        /// Loads the content.
        /// </summary>
        public override void LoadContent()
        {
            this._bar = this.Content.Load<ITexture>("Resources/UI/bar.png");

            this._experience = new SpriteSheet(
                this.Content.Load<ITexture>("Resources/UI/experience.png"),
                3, 11);

            this._experienceEmpty = new SpriteSheet(
                this.Content.Load<ITexture>("Resources/UI/experienceEmpty.png"),
                3, 11);

            this._heart = this.Content.Load<ITexture>("Resources/UI/heart.png");
            this._heartEmpty = this.Content.Load<ITexture>("Resources/UI/heartEmpty.png");
            this._heartHalf = this.Content.Load<ITexture>("Resources/UI/heartHalf.png");

            this._slot = this.Content.Load<ITexture>("Resources/UI/slot.png");
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
        }
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            this.RenderBar();
            this.RenderHearts();
            this.RenderExperience();
            this.RenderItemSlots();
        }
        /// <summary>
        /// Renders the background bar.
        /// </summary>
        private void RenderBar()
        {
            DisplayMode displayMode = this.GraphicsDevice.DisplayMode;

            this.RenderManager.Render(this._bar,
                new Rectangle(0, 0, displayMode.Width, this._bar.Height));
        }
        /// <summary>
        /// Renders the hearts bar.
        /// </summary>
        private void RenderHearts()
        {
            //TODO: get hero health
            int maxHealth = 10;
            int health = 6;

            for (int mh = 0; mh < maxHealth / 2; mh++)
            {
                this.RenderManager.Render(
                    this._heartEmpty,
                    new Vector2(10 + mh * (this._heartEmpty.Width + 2), 10));
            }
            for (int h = 0; h < health / 2; h++)
            {
                this.RenderManager.Render(
                    this._heart,
                    new Vector2(10 + h * (this._heartEmpty.Width + 2), 10));
            }

            //Handle half hearts
            if (health % 2 != 0)
            {
                this.RenderManager.Render(
                    this._heartHalf,
                    new Vector2(10 + (health / 2) * (this._heart.Width + 2), 10));
            }
        }
        /// <summary>
        /// Renders the experience bar.
        /// </summary>
        private void RenderExperience()
        {
            //TODO: get hero experience
            float neededExperience = 300;
            float experience = 200;

            float percentage = experience / neededExperience;

            this.RenderSpriteSheet(
                this._experienceEmpty,
                new Rectangle(10, 30, 200, 11));

            if (percentage > 0)
            {
                this.RenderSpriteSheet(
                    this._experience,
                    new Rectangle(10, 30, 200 * percentage, 11));
            }
        }
        /// <summary>
        /// Renders a sprite sheet.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <param name="rectangle">The rectangle.</param>
        private void RenderSpriteSheet(SpriteSheet sheet, Rectangle rectangle)
        {
            float width = MathHelper.Max(rectangle.Width, sheet.FrameWidth * 3);

            this.RenderManager.Render(
                sheet.Textures[0],
                new Rectangle(
                    rectangle.X,
                    rectangle.Y,
                    sheet.FrameWidth,
                    rectangle.Height));

            this.RenderManager.Render(
                sheet.Textures[1],
                new Rectangle(
                    rectangle.X + sheet.FrameWidth,
                    rectangle.Y,
                    width - sheet.FrameWidth * 2,
                    rectangle.Height));

            this.RenderManager.Render(
                sheet.Textures[2],
                new Rectangle(
                    rectangle.X + width - sheet.FrameWidth,
                    rectangle.Y,
                    sheet.FrameWidth, 
                    rectangle.Height));
        }
        /// <summary>
        /// Renders the item slots.
        /// </summary>
        private void RenderItemSlots()
        {
            DisplayMode displayMode = this.GraphicsDevice.DisplayMode;

            this.RenderManager.Render(
                this._slot, new Vector2(displayMode.Width - this._slot.Width - 10, 5));
            this.RenderManager.Render(
                this._slot, new Vector2(displayMode.Width - this._slot.Width * 2 - 15, 5));
        }
        #endregion
    }
}
