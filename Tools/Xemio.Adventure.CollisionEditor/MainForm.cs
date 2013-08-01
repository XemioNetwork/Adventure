using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Xemio.Adventure.CollisionEditor
{
    public partial class MainForm : Form
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the "mNew" click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MNewClick(object sender, EventArgs e)
        {
            using (NewForm form = new NewForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    this.collisionControl.New(
                        form.MapWidth, form.MapHeight, form.CellSize);

                    this.vScrollBar.Maximum = form.MapWidth * form.CellSize / 32;
                    this.hScrollBar.Maximum = form.MapHeight * form.CellSize / 32;

                    this.vScrollBar.Enabled = true;
                    this.hScrollBar.Enabled = true;

                    this.collisionControl.Invalidate();
                }
            }
        }
        /// <summary>
        /// Handles the "mOpen" click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MOpenClick(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the "mSave" click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MSaveClick(object sender, EventArgs e)
        {
            using (SaveFileDialog fileDialog = new SaveFileDialog())
            {
                fileDialog.Filter = "JSON Collision Map (*.json)|*.json";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    JValue[] values = this.collisionControl
                        .ToArray()
                        .Select(v => new JValue(v))
                        .ToArray();

                    JObject root = new JObject(
                        new JProperty("Width", this.collisionControl.MapWidth),
                        new JProperty("Height", this.collisionControl.MapHeight),
                        new JProperty("CollisionMap", new JArray(values)));

                    using (FileStream stream = File.Open(fileDialog.FileName, FileMode.CreateNew))
                    using (StreamWriter streamWriter = new StreamWriter(stream))
                    {
                        streamWriter.Write(root.ToString());
                        streamWriter.Flush();
                    }
                }
            }
        }
        /// <summary>
        /// Handles the "mLoadImage" click event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void MLoadImageClick(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                fileDialog.Filter = "PNG Images (*.png)|*.png";
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.collisionControl.Image = new Bitmap(fileDialog.FileName);
                    this.collisionControl.Invalidate();
                }
            }
        }
        private void VScrollBarScroll(object sender, ScrollEventArgs e)
        {
            this.collisionControl.Offset = new Point(-this.hScrollBar.Value * 32, -e.NewValue * 32);
            this.collisionControl.Invalidate();
        }
        private void HScrollBarScroll(object sender, ScrollEventArgs e)
        {
            this.collisionControl.Offset = new Point(-e.NewValue * 32, -this.vScrollBar.Value * 32);
            this.collisionControl.Invalidate();
        }
        #endregion
    }
}
