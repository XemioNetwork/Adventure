using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.Adventure.Worlds.TileEngine.Events
{
    internal class TilesetNotFoundLoggingEvent : LoggingEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TilesetNotFoundLoggingEvent"/> class.
        /// </summary>
        /// <param name="tileIndex">Index of the tile.</param>
        public TilesetNotFoundLoggingEvent(int tileIndex)
            : base(LoggingLevel.Information, string.Format("A tileset for the specified index {0} doesn't exist.", tileIndex))
        {
        }
        #endregion
    }
}
