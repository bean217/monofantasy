using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    class Layer
    {
        private Chunk _chunk;
        private int _layerNum;

        private Tile[,] _tiles;

        public Layer(Chunk chunk, int layerNum)
        {
            _tiles = new Tile[Chunk.WIDTH, Chunk.HEIGHT];
            _chunk = chunk;
            _layerNum = layerNum;
        }

        public void LoadContent()
        {
            // Loading tiles will requies reading from a chunk text file with
            // texture mappings. These texture mappings will be used to relate
            // to a 32x32px texture in the texture map image file.
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
        }

    }
}
