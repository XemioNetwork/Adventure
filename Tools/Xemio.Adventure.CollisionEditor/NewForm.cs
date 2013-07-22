using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Xemio.Adventure.CollisionEditor
{
    public partial class NewForm : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NewForm"/> class.
        /// </summary>
        public NewForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets the width of the map.
        /// </summary>
        public int MapWidth
        {
            get { return (int)this.nbxWidth.Value; }
        }
        /// <summary>
        /// Gets the height of the map.
        /// </summary>
        public int MapHeight
        {
            get { return (int) this.nbxHeight.Value; }
        }
        /// <summary>
        /// Gets the size of the cell.
        /// </summary>
        public int CellSize
        {
            get { return (int) this.nbxCellSize.Value; }
        }
        #endregion
    }
}
