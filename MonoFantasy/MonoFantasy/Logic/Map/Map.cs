using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public readonly static int WIDTH = 1;
        // Number of chunks (height)
        public readonly static int HEIGHT = 1;
        public readonly static string CONFIG_FILENAME = "config.txt";
        public GameState _gameState;
        private Chunk[,] _chunks;

        public Map(GameState gameState)
        {
            _chunks = new Chunk[WIDTH, HEIGHT];
            _gameState = gameState;
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
