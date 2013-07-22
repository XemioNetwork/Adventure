using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Content;
using Xemio.GameLibrary.Content.Texts;
using Xemio.GameLibrary.Plugins.Implementations;

namespace Xemio.Adventure.Worlds.Serialization
{
    public class FormatReader<T> : TextReader<T>
    {
        #region Overrides of TextReader<T>
        /// <summary>
        /// Reads the specified input.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="input">The input.</param>
        protected override T Read(Stream stream, string input)
        {
            FileStream fileStream = stream as FileStream;

            if (fileStream == null)
            {
                throw new InvalidOperationException(
                    "You cannot load formatted resources without using a file as source.");
            }

            string id = Path.GetExtension(fileStream.Name);
            var implementations = XGL.Components.Get<ImplementationManager>();

            IFormat<T> format = implementations.Get<string, IFormat<T>>(id);
            return format.Parse(fileStream.Name, input);
        }
        #endregion
    }
}
