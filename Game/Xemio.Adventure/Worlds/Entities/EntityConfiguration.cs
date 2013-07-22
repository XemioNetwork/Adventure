using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds.Entities
{
    public class EntityConfiguration
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityConfiguration"/> class.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="boundingBox">The bounding box.</param>
        /// <param name="collidable">if set to <c>true</c> [collidable].</param>
        /// <param name="animations">The animations.</param>
        public EntityConfiguration(Vector2 offset, BoundingBox boundingBox, bool collidable, IEnumerable<Animation> animations)
        {
            this.Offset = offset;
            this.BoundingBox = boundingBox;
            this.Collidable = collidable;
            this.Animations = animations.ToList();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the offset.
        /// </summary>
        public Vector2 Offset { get; private set; }
        /// <summary>
        /// Gets the bounding box.
        /// </summary>
        public BoundingBox BoundingBox { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="EntityConfiguration"/> is collidable.
        /// </summary>
        public bool Collidable { get; private set; }
        /// <summary>
        /// Gets the animations.
        /// </summary>
        public List<Animation> Animations { get; private set; } 
        #endregion
    }
}
