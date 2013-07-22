namespace Xemio.Adventure.Worlds.TileEngine.Components
{
    public interface ITileComponent
    {
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(Field field, float elapsed);
        /// <summary>
        /// Renders the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        void Render(Field field);
    }
}
