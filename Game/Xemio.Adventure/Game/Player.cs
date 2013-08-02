using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Xemio.Adventure.Game
{
    public class Player
    {
        #region Constructors
        public Player()
        {

        }
        #endregion

        #region Fields

        #endregion

        #region Properties

        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Experience { get; set; }
        public int EntityId { get; set; }

        #endregion

        #region Methods

        #endregion
    }
}
