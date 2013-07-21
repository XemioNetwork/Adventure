using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.Adventure.Worlds.Serialization.Formats
{
    public interface IMapFormat : IParser<string, Map>, ILinkable<string>
    {
    }
}
