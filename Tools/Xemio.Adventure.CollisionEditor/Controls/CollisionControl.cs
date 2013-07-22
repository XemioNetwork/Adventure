using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Xemio.Adventure.CollisionEditor.Controls
{
    public class CollisionControl : Panel
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CollisionControl"/> class.
        /// </summary>
        public CollisionControl()
        {
            this.ResizeRedraw = true;
            this.DoubleBuffered = true;
        }
        #endregion

        #region Fields
        private bool[,] _collisionMap;
        private int _cellSize;
        private bool _currentMappingValue;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public Point Offset { get; set; }
        /// <summary>
        /// Gets the width of the map.
        /// </summary>
        public int MapWidth { get; private set; }
        /// <summary>
        /// Gets the height of the map.
        /// </summary>
        public int MapHeight { get; private set; }
        /// <summary>
        /// Gets the size of the cell.
        /// </summary>
        public int CellSize { get; private set; }
        #endregion

        #region Methods
        /// <summary>
        /// Returns an array representing the collision map.
        /// </summary>
        public bool[] ToArray()
        {
            List<bool> values = new List<bool>();

            for (int x = 0; x < this._collisionMap.GetLength(0); x++)
            {
                for (int y = 0; y < this._collisionMap.GetLength(1); y++)
                {
                    values.Add(this._collisionMap[x, y]);
                }
            }

            return values.ToArray();
        }
        /// <summary>
        /// Creates a new collision map.
        /// </summary>
        /// <param name="mapWidth">Width of the map.</param>
        /// <param name="mapHeight">Height of the map.</param>
        /// <param name="cellSize">Size of the cell.</param>
        public void New(int mapWidth, int mapHeight, int cellSize)
        {
            this.MapWidth = mapWidth;
            this.MapHeight = mapHeight;
            this.CellSize = cellSize;

            this._collisionMap = new bool[mapWidth, mapHeight];
            this._cellSize = cellSize;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this._collisionMap != null && e.Button == MouseButtons.Left)
            {
                int x = (e.X - this.Offset.X) / this._cellSize;
                int y = (e.Y - this.Offset.Y) / this._cellSize;

                if (x >= 0 && x < this._collisionMap.GetLength(0) &&
                    y >= 0 && y < this._collisionMap.GetLength(1))
                {
                    this._currentMappingValue = !this._collisionMap[x, y];

                    this._collisionMap[x, y] = this._currentMappingValue;
                    this.Invalidate();
                }
            }
        }
        /// <summary>
        /// Raises the mouse move event.
        /// </summary>
        /// <param name="e">An instance containing event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (this._collisionMap != null && e.Button == MouseButtons.Left)
            {
                int x = (e.X - this.Offset.X) / this._cellSize;
                int y = (e.Y - this.Offset.Y) / this._cellSize;

                if (x >= 0 && x < this._collisionMap.GetLength(0) &&
                    y >= 0 && y < this._collisionMap.GetLength(1))
                {
                    this._collisionMap[x, y] = this._currentMappingValue;
                    this.Invalidate();
                }
            }

            base.OnMouseMove(e);
        }
        /// <summary>
        /// Raises the paint event.
        /// </summary>
        /// <param name="e">An instance containing event data,</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.FromArgb(220, 220, 220));

            if (this.Image != null)
            {
                e.Graphics.DrawImage(this.Image, this.Offset.X, this.Offset.Y);
            }

            if (this._collisionMap != null)
            {
                for (int x = 0; x < this._collisionMap.GetLength(0); x++)
                {
                    for (int y = 0; y < this._collisionMap.GetLength(1); y++)
                    {
                        if (!this._collisionMap[x, y])
                            continue;

                        Color color = Color.FromArgb(200, Color.Red);

                        e.Graphics.FillRectangle(
                            new SolidBrush(color),
                            x * this._cellSize + this.Offset.X,
                            y * this._cellSize + this.Offset.Y,
                            this._cellSize,
                            this._cellSize);
                    }
                }

                e.Graphics.DrawRectangle(Pens.Red,
                                         new Rectangle(this.Offset.X, this.Offset.Y,
                                                       this._collisionMap.GetLength(0) * this._cellSize,
                                                       this._collisionMap.GetLength(1) * this._cellSize));

            }
            e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(40, Color.Black)),
                0, 0, this.Width - 1, this.Height - 1);

            e.Graphics.DrawRectangle(
                new Pen(Color.FromArgb(80, Color.White)),
                1, 1, this.Width - 3, this.Height - 3);

            base.OnPaint(e);
        }
        #endregion
    }
}
