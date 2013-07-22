using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Entities.Components;
using Xemio.Adventure.Worlds.Entities.Components.Movement;
using Xemio.Adventure.Worlds.Entities.Events.Movement;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Common.Randomization;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Entities.Characters
{
    public class TestEntity : LinkableEntity
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HeroEntity"/> class.
        /// </summary>
        public TestEntity()
        {
            this.Components.Add(new MovementComponent(this) {Speed = 10});
        }
        #endregion

        #region Fields
        private IRandom _random;
        private Vector2 _direction;
        private float _elapsed;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes the specified environment.
        /// </summary>
        /// <param name="environment">The environment.</param>
        public override void Initialize(EntityEnvironment environment)
        {
            this.GetComponent<AnimationComponent>().ChangeAnimation("IdleDown");
        }
        public override void Tick(float elapsed)
        {
            if (this._random == null)
            {
                this._random = new RandomProxy(this.EntityId);
            }

            this._elapsed += elapsed;
            if (this._elapsed >= 100)
            {
                this._elapsed = 0;
                this._direction = new Vector2(
                    this._random.NextFloat() * 2 - 1,
                    this._random.NextFloat() * 2 - 1);
            }

            this.GetComponent<MovementComponent>()
                .Move(this._direction);

            base.Tick(elapsed);
        }
        #endregion

        #region Overrides of LinkableEntity
        /// <summary>
        /// Gets the id.
        /// </summary>
        public override string Id
        {
            get { return "Test"; }
        }
        #endregion
    }
}
