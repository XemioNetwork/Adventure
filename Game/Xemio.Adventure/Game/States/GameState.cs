using System.Collections.Generic;
using Xemio.Adventure.Game.Items;
using Xemio.Adventure.Worlds;

namespace Xemio.Adventure.Game.States
{
    public class GameState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        public GameState()
        {
            this.Player = new Player();
            this.Inventory = new Inventory();
            this.MapStates = new List<MapState>();
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public Player Player { get; set; }
        /// <summary>
        /// Gets or sets the inventory.
        /// </summary>
        public Inventory Inventory { get; set; }
        /// <summary>
        /// Gets the map states.
        /// </summary>
        public List<MapState> MapStates { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Applies the gamestate to the specified world.
        /// </summary>
        /// <param name="world">The world.</param>
        public void Apply(World world)
        {
        }
        #endregion
    }
}
