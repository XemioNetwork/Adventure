using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Xemio.Adventure.Worlds.Entities;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds.Serialization.Entities
{
    public class JsonConfigurationFormat : IFormat<EntityConfiguration>
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
            JObject root = JObject.Parse(input);
            ObjectStorage storage = JsonHelper.CreateObjectStorage(root);
            ITextureFactory factory = XGL.Components.Get<ITextureFactory>();

            string imageFile = storage.Retrieve<string>("Image");
            ITexture texture = factory.CreateTexture(imageFile);

            int frameWidth = storage.Retrieve<int>("FrameWidth");
            int frameHeight = storage.Retrieve<int>("FrameHeight");
            
            Vector2 offset = new Vector2(
                storage.Retrieve<int>("OffsetX"),
                storage.Retrieve<int>("OffsetY"));

            bool collidable = storage.Retrieve<bool>("Collidable");

            SpriteSheet sheet = new SpriteSheet(texture, frameWidth, frameHeight);
            int frameTime = storage.Retrieve<int>("FrameTime");

            Rectangle bounds = storage.Retrieve<Rectangle>("BoundingBox");
            BoundingBox boundingBox = new BoundingBox(
                new Vector2(bounds.X, bounds.Y),
                new Vector2(bounds.X + bounds.Width, bounds.Y + bounds.Height));

            JObject jsonAnimations = storage.Retrieve<JObject>("Animations");

            ObjectStorage animationStorage = JsonHelper.CreateObjectStorage(jsonAnimations);
            IList<string> keys = JsonHelper.GetKeys(jsonAnimations);

            IEnumerable<Animation> animations = from key in keys
                                                select new Animation(key, sheet, frameTime, true);

            int rowIndex = 0;
            int columns = sheet.Columns;

            var list = animations as List<Animation> ?? animations.ToList();
            foreach (Animation animation in list)
            {
                int index = rowIndex * columns;
                int length = animationStorage.Retrieve<int>(animation.Name);

                List<int> indices = new List<int>();
                for (int i = 0; i < length; i++)
                {
                    indices.Add(index + i);
                }

                animation.Indices = indices.ToArray();
                rowIndex++;
            }

            return new EntityConfiguration(offset, boundingBox, collidable, list);
        }
        #endregion

        #region Implementation of ILinkable<string>
        /// <summary>
        /// Gets the id.
        /// </summary>
        public string Id
        {
            get { return ".json"; }
        }
        #endregion
    }
}
