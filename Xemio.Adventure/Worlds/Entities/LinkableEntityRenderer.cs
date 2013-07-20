using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Entities.Components;
using Xemio.GameLibrary.Entities;

namespace Xemio.Adventure.Worlds.Entities
{
    public class LinkableEntityRenderer : EntityRenderer
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkableEntityRenderer"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public LinkableEntityRenderer(Entity entity) : base(entity)
        {
        }
        #endregion
        
        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            var animationComponent = this.Entity.GetComponent<AnimationComponent>();
            if (animationComponent != null)
            {
                this.RenderManager.Render(
                    animationComponent.Animation.Frame,
                    this.Entity.Position);
            }
        }
        #endregion
    }
}
