using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.Adventure.Worlds.Serialization
{
    public interface IFormat<out T> : ILinkable<string>
    {
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="input">The input.</param>
        T Parse(string fileName, string input);
    }
}
