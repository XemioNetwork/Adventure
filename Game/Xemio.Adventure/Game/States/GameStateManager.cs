using System.Collections.Generic;
using System.Linq;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Components;
using Xemio.GameLibrary.Content;

namespace Xemio.Adventure.Game.States
{
    public class GameStateManager : IComponent
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="GameStateManager"/> class.
        /// </summary>
        public GameStateManager()
        {
            this.RootDirectory = "./Saves/";
            this.Current = new GameState();
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets or sets the root directory.
        /// </summary>
        public string RootDirectory { get; set; }
        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        public GameState Current { get; private set; }
        /// <summary>
        /// Gets the content.
        /// </summary>
        protected ContentManager Content
        {
            get { return XGL.Components.Get<ContentManager>(); }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Creates the save path for the specified player.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        private string CreatePath(string playerName)
        {
            return Path.Combine(this.RootDirectory, playerName) + ".sav";
        }
        /// <summary>
        /// Gets the available players.
        /// </summary>
        public IEnumerable<Player> GetAvailablePlayers()
        {
            return this.Content.FileSystem
                .GetFiles(this.RootDirectory)
                .Select(file => this.Content.Load<GameState>(file).Player);
        }

        /// <summary>
        /// Loads the specified player savegame.
        /// </summary>
        /// <param name="playerName">Name of the player.</param>
        public void Load(string playerName)
        {
            this.Current = this.Content.Load<GameState>(this.CreatePath(playerName));
        }
        /// <summary>
        /// Saves the current gamestate.
        /// </summary>
        public void Save()
        {
            this.Content.Save(this.Current, this.CreatePath(this.Current.Player.Name));
        }
        #endregion
    }
}
