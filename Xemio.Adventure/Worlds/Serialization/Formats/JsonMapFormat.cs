using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;
using Xemio.Adventure.Worlds.Entities;
using Xemio.Adventure.Worlds.Entities.Components;
using Xemio.Adventure.Worlds.TileEngine.Tiles;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds.Serialization.Formats
{
    public class JsonMapFormat : IMapFormat
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonMapFormat"/> class.
        /// </summary>
        public JsonMapFormat()
        {
            this._factory = XGL.Components.Get<ITextureFactory>();
            this._implementations = XGL.Components.Get<ImplementationManager>();
        }
        #endregion

        #region Fields
        private readonly ITextureFactory _factory;
        private readonly ImplementationManager _implementations;
        #endregion

        #region Implementation of IParser<string, Map>
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public Map Parse(string input)
        {
            JObject jsonMap = JObject.Parse(input);

            JToken jsonMapName = jsonMap["properties"]["Name"];
            if (jsonMapName == null)
            {
                throw new InvalidOperationException(
                    "Your map has to contain the property 'Name' inside the map properties, to be uniquely identifiable.");
            }

            string name = jsonMapName.Value<string>();

            int width = jsonMap["width"].Value<int>();
            int height = jsonMap["height"].Value<int>();

            int tileWidth = jsonMap["tilewidth"].Value<int>();
            int tileHeight = jsonMap["tileheight"].Value<int>();

            int layers = jsonMap["layers"].Count(t => t["type"].Value<string>() == "tilelayer");

            JArray jsonTilesets = jsonMap["tilesets"].Value<JArray>();
            JArray jsonLayers = jsonMap["layers"].Value<JArray>();

            List<TileSet> tileSets = new List<TileSet>();
            List<TileSet> entityTileSets = new List<TileSet>();
            Dictionary<int, string> entityIds = new Dictionary<int, string>();

            foreach (JObject jsonTileset in jsonTilesets)
            {
                string fileName = jsonTileset["image"].Value<string>();
                ITexture texture = this._factory.CreateTexture(fileName);
 
                SpriteSheet tileSheet = new SpriteSheet(
                    texture,
                    jsonTileset["tilewidth"].Value<int>(),
                    jsonTileset["tileheight"].Value<int>());
                
                TileSet tileSet = new TileSet(
                    jsonTileset["name"].Value<string>(),
                    jsonTileset["firstgid"].Value<int>());

                if (jsonTileset["properties"]["IsEntity"] != null)
                {
                    entityTileSets.Add(tileSet);
                }

                JToken jsonTilePropertyToken = jsonTileset["tileproperties"];
                for (int i = 0; i < tileSheet.Textures.Length; i++)
                {
                    string tileId = "Default";

                    string namedIndex = i.ToString();
                    JToken currentValue = default(JToken);

                    if (jsonTilePropertyToken != null &&
                        jsonTilePropertyToken
                            .Value<JObject>()
                            .TryGetValue(namedIndex, out currentValue))
                    {
                        if (currentValue["TileId"] != null)
                        {
                            tileId = currentValue["TileId"].Value<string>();
                        }
                        if (currentValue["EntityId"] != null)
                        {
                            entityIds.Add(
                                tileSet.FirstTileIndex + i,
                                currentValue["EntityId"].Value<string>());
                        }
                    }

                    //TODO: animation data as tile properties inside tiled.
                    TileReference reference = new TileReference(
                        this._implementations.Get<string, Tile>(tileId),
                        new Animation("Main", tileSheet.Textures[i]));
                    
                    tileSet.Tiles.Add(reference);
                }

                tileSets.Add(tileSet);
            }

            Map map = new Map(name, tileSets, width, height, tileWidth, tileHeight, layers);
            int layerIndex = 0;

            foreach (JObject jsonLayer in jsonLayers)
            {
                switch (jsonLayer["type"].Value<string>())
                {
                    case "objectgroup":
                        {
                            JArray jsonObjects = jsonLayer["objects"].Value<JArray>();
                            foreach (JObject jsonValue in jsonObjects)
                            {
                                int tileId = jsonValue["gid"].Value<int>();
                                Vector2 position = new Vector2(
                                    jsonValue["x"].Value<int>(),
                                    jsonValue["y"].Value<int>());

                                //TODO: properties for EntityDataContainer

                                TileReference reference = map.GetTile(tileId);

                                Entity entity = this._implementations
                                    .GetNew<string, LinkableEntity>(entityIds[tileId]);

                                entity.Position = position;

                                var animationComponent = new AnimationComponent(
                                    entity, reference.Animation.Animation);

                                entity.Components.Add(animationComponent);
                                map.Environment.Add(entity);
                            }
                        }
                        break;
                    case "tilelayer":
                        {
                            JArray jsonData = jsonLayer["data"].Value<JArray>();

                            int x = 0;
                            int y = 0;

                            foreach (JValue jsonValue in jsonData)
                            {
                                map.SetField(x, y, layerIndex, jsonValue.Value<int>());

                                if (++x >= map.Width)
                                {
                                    x = 0;
                                    y++;
                                }
                            }

                            layerIndex++;
                        }
                        break;
                }
            }

            foreach (TileSet tileSet in entityTileSets)
            {
                map.TileSets.Remove(tileSet);
            }

            return map;
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
