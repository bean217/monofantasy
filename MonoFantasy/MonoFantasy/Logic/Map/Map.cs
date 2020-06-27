using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFantasy.Content.ITexture;
using MonoFantasy.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    class Map
    {
        // Number of chunks (width)
        private int WIDTH;
        // Number of chunks (height)
        private int HEIGHT;
        public readonly static string CONFIG_FILENAME = "config.txt";
        public GameState _gameState;
        private Chunk[,] _chunks;
        public string _mapDir;

        public static readonly string TEXTURE_MAPPINGS_FILE = "texture_mappings.txt";
        public Dictionary<string, BlockData> chunkTileData;

        public Texture2D tileAtlas;

        public Map(GameState gameState)
        {
            WIDTH = ConfigInfo.MAP_WIDTH;
            HEIGHT = ConfigInfo.MAP_HEIGHT;
            _chunks = new Chunk[WIDTH, HEIGHT];
            _gameState = gameState;
            _mapDir = $"{_gameState._saveDir}/game/world";
            chunkTileData = MapTextureReader.getChunkTiles($"{_mapDir}/{TEXTURE_MAPPINGS_FILE}");
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    _chunks[x, y] = new Chunk(this, x, y);
                }
            }
        }

        public void LoadContent()
        {
            tileAtlas = LoadTexture.Load(_gameState._graphicsDevice, $"{_mapDir}/texture_map.png");
            foreach (var chunk in _chunks)
                chunk.LoadContent();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    _chunks[x, y].Draw(gameTime, spriteBatch);
                }
            }
        }

        public void Update()
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    _chunks[x, y].Update();
                }
            }
        }
    }
}
