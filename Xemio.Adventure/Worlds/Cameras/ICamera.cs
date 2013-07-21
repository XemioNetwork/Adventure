using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Cameras
{
    public interface ICamera
    {
        /// <summary>
        /// Gets the position.
        /// </summary>
        Vector2 Position { get; }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        void Tick(float elapsed);
    }
}
