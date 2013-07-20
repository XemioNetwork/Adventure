using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds
{
    public class Camera
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        public Camera()
        {
            this.Mode = CameraMode.Free;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Camera"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        public Camera(Entity target)
        {
            this.Mode = CameraMode.Focused;
            this.Target = target;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the position.
        /// </summary>
        public Vector2 Position { get; set; }
        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        public CameraMode Mode { get; set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public Entity Target { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            if (this.Target != null && this.Mode == CameraMode.Focused)
            {
                this.Position += (this.Target.Position - this.Position) * 0.05f;
            }
        }
        #endregion
    }
}
