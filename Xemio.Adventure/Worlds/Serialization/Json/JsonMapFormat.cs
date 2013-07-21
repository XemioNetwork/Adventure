using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Xemio.Adventure.Worlds.Entities;
using Xemio.Adventure.Worlds.Entities.Components;
using Xemio.Adventure.Worlds.Generation;
using Xemio.Adventure.Worlds.TileEngine.Tiles;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Entities;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds.Serialization.Json
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
            ObjectStorage mapProperties = JsonHelper.CreateObjectStorage(jsonMap["properties"]);

            if (!mapProperties.Contains("Name"))
            {
                throw new InvalidOperationException(
                    "Your map has to contain the property 'Name' inside the map properties, to be uniquely identifiable.");
            }

            string name = mapProperties.Retrieve<string>("Name");

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

                ObjectStorage tileSetProperties = JsonHelper
                    .CreateObjectStorage(jsonTileset["properties"]);
                
                SpriteSheet tileSheet = new SpriteSheet(
                    texture,
                    jsonTileset["tilewidth"].Value<int>(),
                    jsonTileset["tileheight"].Value<int>());
                
                TileSet tileSet = new TileSet(
                    jsonTileset["name"].Value<string>(),
                    jsonTileset["firstgid"].Value<int>(),
                    tileSetProperties);

                if (tileSetProperties.Contains("IsEntity"))
                {
                    entityTileSets.Add(tileSet);
                }

                JToken jsonTileProperties = jsonTileset["tileproperties"];
                for (int i = 0; i < tileSheet.Textures.Length; i++)
                {
                    string tileId = "Default";

                    string namedIndex = i.ToString();
                    ObjectStorage tileProperties = new ObjectStorage();
                    
                    if (jsonTileProperties != null && 
                        jsonTileProperties[namedIndex] != null)
                    {
                        tileProperties = JsonHelper.CreateObjectStorage(jsonTileProperties[namedIndex]);

                        if (tileProperties.Contains("EntityId"))
                        {
                            entityIds.Add(
                                tileSet.StartIndex + i,
                                tileProperties.Retrieve<string>("EntityId"));
                        }

                        if (tileProperties.Contains("TileId"))
                        {
                            tileId = tileProperties.Retrieve<string>("TileId");
                        }
                    }

                    Tile tile = this._implementations.Get<string, Tile>(tileId);
                    Animation animation = new Animation("Main", tileSheet.Textures[i]);
                    
                    //TODO: animation data as tile properties inside tiled.
                    tileSet.Tiles.Add(new TileReference(tile, animation, tileProperties));
                }

                tileSets.Add(tileSet);
            }

            MapHeader header = new MapHeader(name, mapProperties);

            MapProperties properties = new MapProperties(width, height, tileWidth, tileHeight, layers);
            Map map = new Map(header, tileSets, properties);
            
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
                                ObjectStorage entityProperties = JsonHelper
                                    .CreateObjectStorage(jsonValue["properties"]);

                                int tileId = jsonValue["gid"].Value<int>();
                                Vector2 position = new Vector2(
                                    jsonValue["x"].Value<int>(),
                                    jsonValue["y"].Value<int>());

                                //TODO: properties for EntityDataContainer

                                TileSet tileSet = map.GetTileSet(tileId);
                                TileReference reference = tileSet.GetTile(tileId);

                                Entity entity = this._implementations
                                    .GetNew<string, LinkableEntity>(entityIds[tileId]);

                                entity.Position = position;

                                var animationComponent = new AnimationComponent(
                                    entity, reference.Animation.Animation);

                                var propertyComponent = new PropertyComponent(
                                    entity, entityProperties);

                                entity.Components.Add(animationComponent);
                                entity.Components.Add(propertyComponent);

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
            
            if (mapProperties.Contains("GeneratorId"))
            {
                string generatorId = mapProperties.Retrieve<string>("GeneratorId");
                IMapGenerator generator = this._implementations.Get<string, IMapGenerator>(generatorId);

                if (generator != null)
                {
                    generator.Generate(map, "");
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
