using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Cameras
{
    public class FreeCamera : ICamera
    {
        #region Implementation of ICamera
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
        }
        #endregion
    }
}
