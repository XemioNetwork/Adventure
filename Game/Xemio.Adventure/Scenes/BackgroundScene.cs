using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Game.Scenes;
using Xemio.GameLibrary.Rendering;

namespace Xemio.Adventure.Scenes
{
    public class BackgroundScene : Scene
    {
        #region Methods
        /// <summary>
        /// Renders this instance.
        /// </summary>
        public override void Render()
        {
            this.GraphicsDevice.Clear(Color.Black);
        }
        #endregion
    }
}
