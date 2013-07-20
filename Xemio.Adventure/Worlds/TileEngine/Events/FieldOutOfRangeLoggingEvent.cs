using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.Adventure.Worlds.TileEngine.Events
{
    internal class FieldOutOfRangeLoggingEvent : LoggingEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldOutOfRangeLoggingEvent"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="z">The z.</param>
        public FieldOutOfRangeLoggingEvent(int x, int y, int z)
            : base(LoggingLevel.Information, string.Format("Field {0}x{1}x{2} is outside the world bounds.", x, y, z))
        {
        }
        #endregion
    }
}
