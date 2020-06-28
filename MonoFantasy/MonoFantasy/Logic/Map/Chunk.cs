using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFantasy.Content.ITexture;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    class Chunk
    {
        // ref to map
        public Map _map;
        // config file info
        Dictionary<string, string> _config;
        // chunk filename
        public string _chunkDir;
        // chunk X position on the world map
        public static readonly string TILE_TEXTURE_DATA_FILE = "tile_texture_data.txt";
        public int _chunkPosX;
        // chunk Y position on the world map
        public int _chunkPosY;
        // number of tiles in chunk width
        public static readonly int WIDTH = 41;
        // number of tiles in chunk height
        public static readonly int HEIGHT = 23;

        // collection of graphic layers
        public List<Layer> _layers;
        // number of layers in chunk
        public int numLayers;
        // 2D array of collision layer
        public Collision[,] _collisionLayer;
        // Dictionary of all tile textures in chunk
        public Dictionary<string, BlockData> chunkTileData;

        public Chunk(Map map, int chunkPosX, int chunkPosY)
        {
            _chunkPosX = chunkPosX;
            _chunkPosY = chunkPosY;
            _map = map;
            _chunkDir = $"{_map._mapDir}/chunk{_chunkPosX}x{_chunkPosY}";
            _layers = new List<Layer>();
            chunkTileData = MapTextureReader.getChunkTiles($"{_chunkDir}/{TILE_TEXTURE_DATA_FILE}");
            readConfig();
        }

        public void LoadContent()
        {
            foreach (var layer in _layers)
                layer.LoadContent();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            foreach (var layer in _layers)
                layer.Draw(gameTime, spriteBatch);
            //spriteBatch.Draw(tileAtlas, new Rectangle(0, 0, 96, 128), Color.White);
            //spriteBatch.End();
        }

        public void Update()
        {
            foreach (var layer in _layers)
                layer.Update();
        }

        private void readConfig()
        {
            _config = new Dictionary<string, string>();
            StreamReader sr = null;
            try
            {
                sr = new StreamReader($"{_chunkDir}/{Map.CONFIG_FILENAME}");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] read = line.Split('=');
                    _config.Add(read[0], read[1]);
                }
                // Reads collision file in chunk file and returns a 2D array of Collision values
                _collisionLayer = CollisionReader.read($"{_chunkDir}/{_config[ConfigInfo.COLLISION_FILE_REF]}");
                try
                {
                    for (int layerNum = 0; layerNum < int.Parse(_config[ConfigInfo.NUM_LAYERS_REF]); layerNum++)
                    {
                        _layers.Add(new Layer(this, layerNum));
                    }
                    numLayers = int.Parse(_config[ConfigInfo.NUM_LAYERS_REF]);
                } catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    Console.WriteLine(e.StackTrace);
                    throw new Exception(e.Message, e);
                }
            } catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            } finally
            {
                if (sr != null)
                    sr.Close();
            }
        }
    }
}
