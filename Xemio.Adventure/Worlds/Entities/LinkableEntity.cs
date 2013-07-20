using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common.Link;
using Xemio.GameLibrary.Entities;

namespace Xemio.Adventure.Worlds.Entities
{
    public abstract class LinkableEntity : Entity, ILinkable<string>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkableEntity"/> class.
        /// </summary>
        protected LinkableEntity()
        {
            this.Renderer = new LinkableEntityRenderer(this);
        }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the id.
        /// </summary>
        public abstract string Id { get; }
        #endregion
    }
}
