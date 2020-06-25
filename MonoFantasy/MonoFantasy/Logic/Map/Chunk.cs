using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Map _map;
        // config file info
        Dictionary<string, string> _config;
        // chunk filename
        private string _chunkDir;
        // chunk X position on the world map
        public int _chunkPosX;
        // chunk Y position on the world map
        public int _chunkPosY;
        // number of tiles in chunk width
        public static readonly int WIDTH = 41;
        // number of tiles in chunk height
        public static readonly int HEIGHT = 23;

        // Atlas of tile textures
        public Texture2D tileAtlas;
        // collection of graphic layers
        private List<Layer> _layers;
        // 2D array of collision layer
        private Collision[,] _collisionLayer;

        public Chunk(Map map, int chunkPosX, int chunkPosY)
        {
            _chunkPosX = chunkPosX;
            _chunkPosY = chunkPosY;
            _map = map;
            _chunkDir = $"{_map._gameState._saveDir}/world/chunk{_chunkPosX}x{_chunkPosY}";
            readConfig();
        }

        public void LoadContent()
        {
            FileStream fileStream = new FileStream($"{_chunkDir}/texture_map.png", FileMode.Open);
            tileAtlas = Texture2D.FromStream(_map._gameState._graphicsDevice, fileStream);
            fileStream.Dispose();
            foreach (var layer in _layers)
                layer.LoadContent();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var layer in _layers)
                layer.Draw(gameTime, spriteBatch);
            spriteBatch.End();
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
                _collisionLayer = CollisionReader.read($"{_chunkDir}/{_config[ConfigInfo.COLLISION_FILE_REF]}", _chunkPosX, _chunkPosY);
                try
                {
                    for (int layerNum = 0; layerNum < int.Parse(_config[ConfigInfo.NUM_LAYERS_REF]); layerNum++)
                    {
                        _layers.Add(new Layer(this, layerNum));
                    }
                } catch (Exception e)
                {
                    Console.WriteLine($"ERROR: {e.Message}");
                    Console.WriteLine(e.StackTrace);
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
