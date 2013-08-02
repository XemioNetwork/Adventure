using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Entities;

namespace Xemio.Adventure.Game.States
{
    public class MapState
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MapState"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="environment">The environment.</param>
        public MapState(string name, EntityEnvironment environment)
        {
            this.Name = name;
            this.Environment = environment;
        }
        #endregion
        
        #region Properties
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Gets the environment.
        /// </summary>
        public EntityEnvironment Environment { get; private set; }
        #endregion
    }
}
