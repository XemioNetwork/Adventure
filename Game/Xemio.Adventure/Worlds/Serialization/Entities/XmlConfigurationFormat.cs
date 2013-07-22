using System;
using Xemio.Adventure.Worlds.Entities;

namespace Xemio.Adventure.Worlds.Serialization.Entities
{
    public class XmlConfigurationFormat : IFormat<EntityConfiguration>
    {
        #region Implementation of IParser<in string,out EntityConfiguration>
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public EntityConfiguration Parse(string fileName, string input)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id
        {
            get { return ".xml"; }
        }
        #endregion
    }
}
