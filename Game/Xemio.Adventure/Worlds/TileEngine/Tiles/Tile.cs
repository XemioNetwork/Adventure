﻿using System.Collections.Generic;
using Xemio.Adventure.Worlds.TileEngine.Components;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.Adventure.Worlds.TileEngine.Tiles
{
    public abstract class Tile : ILinkable<string>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Tile"/> class.
        /// </summary>
        protected Tile()
        {
            this.Components = new List<ITileComponent>();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the components.
        /// </summary>
        public List<ITileComponent> Components { get; private set; } 
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified tile component.
        /// </summary>
        /// <param name="tileComponent">The tile component.</param>
        public void Add(ITileComponent tileComponent)
        {
            this.Components.Add(tileComponent);
        }
        /// <summary>
        /// Removes the specified tile component.
        /// </summary>
        /// <param name="tileComponent">The tile component.</param>
        public void Remove(ITileComponent tileComponent)
        {
            this.Components.Remove(tileComponent);
        }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the identifier for the current instance.
        /// </summary>
        public abstract string Id { get; }
        #endregion
    }
}
