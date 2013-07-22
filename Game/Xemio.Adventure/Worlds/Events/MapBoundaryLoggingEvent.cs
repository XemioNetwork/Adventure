using Xemio.GameLibrary.Events.Logging;

namespace Xemio.Adventure.Worlds.Events
{
    internal class MapBoundaryLoggingEvent : LoggingEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapBoundaryLoggingEvent"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public MapBoundaryLoggingEvent(int x, int y, int z)
            : base(LoggingLevel.Information, string.Format("Field {0}x{1}x{2} is outside the world bounds.", x, y, z))
        {
        }
        #endregion
    }
}
