using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Content;

namespace Xemio.Adventure.Worlds
{
    public class World
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class.
        /// </summary>
        public World()
        {
            this.Maps = new List<Map>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the active map.
        /// </summary>
        public Map ActiveMap { get; private set; }
        /// <summary>
        /// Gets the maps.
        /// </summary>
        public List<Map> Maps { get; private set; }
        #endregion
        
        #region Methods
        /// <summary>
        /// Changes the map.
        /// </summary>
        /// <param name="name">The name.</param>
        public void ChangeMap(string name)
        {
            this.ActiveMap = this.Maps.FirstOrDefault(map => map.Header.Name == name);
        }
        /// <summary>
        /// Handles a game tick.
        /// </summary>
        /// <param name="elapsed">The elapsed.</param>
        public void Tick(float elapsed)
        {
            if (this.ActiveMap != null)
            {
                this.ActiveMap.Tick(elapsed);
            }

            //TODO: handle passive map ticks.
        }
        /// <summary>
        /// Renders this world.
        /// </summary>
        public void Render()
        {
            if (this.ActiveMap != null)
            {
                this.ActiveMap.Renderer.Render();
            }
        }
        #endregion
    }
}
