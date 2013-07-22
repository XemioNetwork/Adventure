using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Entities.Components;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;

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
            this.Color = Color.White;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public Color Color { get; set; }
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public Vector2 Offset { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            var animationComponent = this.Entity.GetComponent<AnimationComponent>();
            if (animationComponent != null && animationComponent.Current != null)
            {
                this.RenderManager.Render(
                    animationComponent.Current.Frame,
                    this.Entity.Position + this.Offset,
                    this.Color);
            }
        }
        #endregion
    }
}
