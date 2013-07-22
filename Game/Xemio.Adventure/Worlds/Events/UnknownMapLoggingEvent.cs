using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.Adventure.Worlds.Events
{
    internal class UnknownMapLoggingEvent : LoggingEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownMapLoggingEvent"/> class.
        /// </summary>
        /// <param name="mapName">Name of the map.</param>
        public UnknownMapLoggingEvent(string mapName)
            : base(LoggingLevel.Information, string.Format("Map '{0}' does not exist.", mapName))
        {
        }
        #endregion
    }
}
