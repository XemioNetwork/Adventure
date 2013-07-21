using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Common.Link;

namespace Xemio.Adventure.Worlds.Serialization
{
    public interface IMapFormat : IParser<string, Map>, ILinkable<string>
    {
    }
}
