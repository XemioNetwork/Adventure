using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Math;

namespace Xemio.Adventure.Worlds.Entities
{
    public class TestEntity : LinkableEntity
    {
        #region Overrides of LinkableEntity
        /// <summary>
        /// Gets the id.
        /// </summary>
        public override string Id
        {
            get { return "Test"; }
        }
        #endregion

        #region Methods
        public override void Tick(float elapsed)
        {
            this.Position += new Vector2(1, 1);
        }
        #endregion
    }
}
