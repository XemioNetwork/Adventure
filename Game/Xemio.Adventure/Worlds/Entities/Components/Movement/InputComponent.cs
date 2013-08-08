using System.Diagnostics;
using Xemio.Adventure.Worlds.Entities.Events.Movement;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Events;
using Xemio.GameLibrary.Input;
using Xemio.GameLibrary.Input.Keyboard;
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
        /// <param name="playerIndex">Index of the player.</param>
        public InputComponent(Entity entity, int playerIndex) : base(entity)
        {
            this._lastDirection = Direction.None;
            this.PlayerIndex = playerIndex;
        }
        #endregion

        #region Fields
        private Direction _lastDirection;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the index of the player.
        /// </summary>
        public int PlayerIndex { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public override void Tick(float elapsed)
        {
            //TODO: input bindings
            var inputManager = XGL.Components.Get<InputManager>();
            var playerInput = inputManager.GetPlayerInput(this.PlayerIndex);

            var direction = Direction.None;

            if (playerInput.Keyboard.GetState(Keys.Left).Active)
                direction = direction | Direction.Left;

            if (playerInput.Keyboard.GetState(Keys.Right).Active)
                direction = direction | Direction.Right;

            if (playerInput.Keyboard.GetState(Keys.Up).Active)
                direction = direction | Direction.Up;

            if (playerInput.Keyboard.GetState(Keys.Down).Active)
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
