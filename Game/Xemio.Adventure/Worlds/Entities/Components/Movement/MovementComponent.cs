using System;
using Xemio.Adventure.Worlds.Entities.Events.Movement;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Math.Collision.Entities;

namespace Xemio.Adventure.Worlds.Entities.Components.Movement
{
    public class MovementComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MovementComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public MovementComponent(Entity entity) : base(entity)
        {
            var eventManager = XGL.Components.Get<EventManager>();
            eventManager.Subscribe<IMovementEvent>(this.HandleMovement);

            this.Speed = 1.0f;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public float Speed { get; set; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public Vector2 Direction { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles the movement.
        /// </summary>
        /// <param name="e">The e.</param>
        protected virtual void HandleMovement(IMovementEvent e)
        {
            if (e.Entity == this.Entity)
            {
                var startMovement = e as StartMovementEvent;
                var stopMovement = e as StopMovementEvent;

                if (startMovement != null)
                    this.Direction = e.Direction.ToVector2();

                if (stopMovement != null)
                    this.Direction = Vector2.Zero;
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Entity.Position += this.Direction * this.Speed;
        }
        #endregion
    }
}
