using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json.Linq;
using Xemio.Adventure.Worlds.TileEngine.Tiles;
using Xemio.GameLibrary;
using Xemio.GameLibrary.Common;
using Xemio.GameLibrary.Math;
using Xemio.GameLibrary.Plugins.Implementations;
using Xemio.GameLibrary.Rendering;
using Xemio.GameLibrary.Rendering.Sprites;

namespace Xemio.Adventure.Worlds
{
    public class WorldParser : IParser<string, World>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="WorldParser"/> class.
        /// </summary>
        public WorldParser()
        {
            this._factory = XGL.Components.Get<ITextureFactory>();
            this._implementations = XGL.Components.Get<ImplementationManager>();
        }
        #endregion

        #region Fields
        private readonly ITextureFactory _factory;
        private readonly ImplementationManager _implementations;
        #endregion

        #region Implementation of IParser<in string,out Map>
        /// <summary>
        /// Parses the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        public World Parse(string input)
        {
            JObject jsonWorld = JObject.Parse(input);

            int width = jsonWorld["width"].Value<int>();
            int height = jsonWorld["height"].Value<int>();

            int tileWidth = jsonWorld["tilewidth"].Value<int>();
            int tileHeight = jsonWorld["tileheight"].Value<int>();

            int layers = jsonWorld["layers"].Count(t => t["type"].Value<string>() == "tilelayer");

            JArray jsonTilesets = jsonWorld["tilesets"].Value<JArray>();
            JArray jsonLayers = jsonWorld["layers"].Value<JArray>();

            List<TileSet> tileSets = new List<TileSet>();
            List<TileSet> entityTileSets = new List<TileSet>();

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

                JObject jsonTileProperties = jsonTileset["tileproperties"].Value<JObject>();
                for (int i = 0; i < tileSheet.Textures.Length; i++)
                {
                    string tileId = "Default";
                    string namedIndex = i.ToString();

                    JToken currentValue;
                    if (jsonTileProperties.TryGetValue(namedIndex, out currentValue))
                    {
                        tileId = currentValue["TileId"].Value<string>();
                    }

                    //TODO: animation data as tile properties inside tiled.
                    TileReference reference = new TileReference(
                        this._implementations.Get<string, Tile>(tileId),
                        new Animation("Main", tileSheet.Textures[i]));

                    tileSet.Tiles.Add(reference);
                }

                tileSets.Add(tileSet);
            }

            World world = new World(tileSets, width, height, tileWidth, tileHeight, layers);
            int layerIndex = 0;

            foreach (JObject jsonLayer in jsonLayers)
            {
                switch (jsonLayer["type"].Value<string>())
                {
                    case "objectgroup":
                        {
                            //TODO: implement
                        }
                        break;
                    case "tilelayer":
                        {
                            JArray jsonData = jsonLayer["data"].Value<JArray>();

                            int x = 0;
                            int y = 0;

                            foreach (JValue jsonValue in jsonData)
                            {
                                world.SetField(x, y, layerIndex, jsonValue.Value<int>());

                                if (++x >= world.Width)
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

            return world;
        }
        #endregion
    }
}
