using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Events.Logging;

namespace Xemio.Adventure.Worlds.Events
{
    public class AnimationNotFoundLoggingEvent : LoggingEvent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationNotFoundLoggingEvent"/> class.
        /// </summary>
        /// <param name="animationName">Name of the animation.</param>
        public AnimationNotFoundLoggingEvent(string animationName)
            : base(LoggingLevel.Information, string.Format("Animation not found: {0}", animationName))
        {
        }
        #endregion
    }
}
