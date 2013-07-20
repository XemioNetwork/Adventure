using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds.Entities.Components
{
    public class AnimationComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AnimationComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="animation">The animation.</param>
        public AnimationComponent(Entity entity, Animation animation) : base(entity)
        {
            this.Animation = animation.CreateInstance();
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the animation.
        /// </summary>
        public AnimationInstance Animation { get; private set; }
        #endregion
    }
}
