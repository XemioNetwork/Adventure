using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Cameras;
using Xemio.Adventure.Worlds.Entities.Components;
using Xemio.Adventure.Worlds.Entities.Components.Movement;
using Xemio.GameLibrary.Entities;

namespace Xemio.Adventure.Worlds.Entities.Characters
{
    public class HeroEntity : LinkableEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HeroEntity"/> class.
        /// </summary>
        public HeroEntity()
        {
            this.Components.Add(new InputComponent(this));
            this.Components.Add(new MovementComponent(this) {Speed = 3});
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the specified environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public override void Initialize(EntityEnvironment environment)
        {
            var mapEnvironment = this.Environment as MapEnvironment;
            if (mapEnvironment.Map.ActiveCamera == null)
            {
                mapEnvironment.Map.ActiveCamera = new EntityCamera(this);
            }

            this.GetComponent<AnimationComponent>()
                .ChangeAnimation("IdleDown");

            base.Initialize(environment);
        }
        #endregion

        #region Overrides of LinkableEntity
        /// <summary>
        /// Gets the id.
        /// </summary>
        public override string Id
        {
            get { return "Hero"; }
        }
        #endregion
    }
}
