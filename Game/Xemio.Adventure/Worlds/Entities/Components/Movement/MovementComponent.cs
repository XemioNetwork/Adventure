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

            this.Speed = 2;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the speed.
        /// </summary>
        public int Speed { get; set; }
        /// <summary>
        /// Gets or sets the direction.
        /// </summary>
        public Vector2 Direction { get; private set; }
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
        /// Moves to the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void Move(Vector2 direction)
        {
            this.MoveDimension(new Vector2(direction.X, 0));
            this.MoveDimension(new Vector2(0, direction.Y));
        }
        /// <summary>
        /// Moves dimensional.
        /// </summary>
        /// <param name="direction">The direction.</param>
        protected virtual void MoveDimension(Vector2 direction)
        {
            var environment = this.Entity.Environment as MapEnvironment;
            var collisionComponent = this.Entity.GetComponent<CollisionComponent>();

            if (direction.LengthSquared == 0.0f)
                return;

            for (int i = 0; i < this.Speed; i++)
            {
                this.Entity.Position += direction;

                int x = (int)this.Entity.Position.X / environment.Grid.CellSize;
                int y = (int)this.Entity.Position.Y / environment.Grid.CellSize;
                
                environment.Grid.Update(this.Entity, collisionComponent.CollisionMap);

                if (environment.Grid.Collides(
                    x, y, this.Entity,
                    collisionComponent.CollisionMap))
                {
                    this.Entity.Position -= direction;
                    break;
                }
            }
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            this.Move(this.Direction);
        }
        #endregion
    }
}
