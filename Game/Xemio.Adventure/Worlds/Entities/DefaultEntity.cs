using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.Adventure.Worlds.Entities.Components;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Entities
{
    public class DefaultEntity : LinkableEntity
    {
        #region Overrides of LinkableEntity
        /// <summary>
        /// Gets the id.
        /// </summary>
        public override string Id
        {
            get { return "Default"; }
        }
        #endregion
    }
}
