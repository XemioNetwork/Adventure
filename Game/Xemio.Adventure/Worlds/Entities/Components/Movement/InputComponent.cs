using System.Diagnostics;
using Xemio.Adventure.Worlds.Entities.Events.Movement;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Entities.Components.Movement
{
    public class InputComponent : EntityComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="InputComponent"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public InputComponent(Entity entity) : base(entity)
        {
            this._lastDirection = Direction.None;
        }
        #endregion

        #region Fields
        private Direction _lastDirection;
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            //TODO: input bindings
            var keyboard = XGL.Components.Get<KeyListener>();
            var direction = Direction.None;

            if (keyboard.IsKeyDown(Keys.Left))
                direction = direction | Direction.Left;

            if (keyboard.IsKeyDown(Keys.Right))
                direction = direction | Direction.Right;

            if (keyboard.IsKeyDown(Keys.Up))
                direction = direction | Direction.Up;

            if (keyboard.IsKeyDown(Keys.Down))
                direction = direction | Direction.Down;
            
            var eventManager = XGL.Components.Get<EventManager>();

            if (direction != Direction.None &&
                direction != this._lastDirection)
            {
                eventManager.Publish(new StartMovementEvent(this.Entity, this.Entity.Position, direction));
            }
            if (direction == Direction.None &&
                direction != this._lastDirection)
            {
                eventManager.Publish(new StopMovementEvent(this.Entity, this.Entity.Position, direction));
            }

            this._lastDirection = direction;
        }
        #endregion
    }
}
