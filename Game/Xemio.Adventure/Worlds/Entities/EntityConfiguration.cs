using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Math.Collision;
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
        /// <param name="collisionMap">The bounding box.</param>
        /// <param name="collidable">if set to <c>true</c> [collidable].</param>
        /// <param name="animations">The animations.</param>
        public EntityConfiguration(CollisionMap collisionMap, bool collidable, IEnumerable<Animation> animations)
        {
            this.CollisionMap = collisionMap;
            this.Collidable = collidable;
            this.Animations = animations.ToList();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the collision map.
        /// </summary>
        public CollisionMap CollisionMap { get; private set; }
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
