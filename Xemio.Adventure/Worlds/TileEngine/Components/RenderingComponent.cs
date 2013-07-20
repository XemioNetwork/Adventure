using Xemio.GameLibrary;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

namespace Xemio.Adventure.Worlds.TileEngine.Components
{
    public class RenderingComponent : ITileComponent
    {
        #region Implementation of ITileComponent
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(Field field, float elapsed)
        {
        }
        /// <summary>
        /// Renders the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        public void Render(Field field)
        {
            IRenderManager renderManager = XGL.Components.Get<IRenderManager>();
            
            renderManager.Render(
                field.Reference.Animation.Frame,
                new Vector2(
                    field.X * field.World.TileWidth,
                    field.Y * field.World.TileHeight));
        }
        #endregion
    }
}
