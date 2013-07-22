using Xemio.Adventure.Worlds.Entities.Events.Movement;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Math;

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
        }
        #endregion

        #region Fields
        private Vector2 _direction;
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
                    this._direction = e.Direction.ToVector2();

                if (stopMovement != null)
                    this._direction = Vector2.Zero;
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Entity.Position += this._direction * 2;
        }
        #endregion
    }
}
