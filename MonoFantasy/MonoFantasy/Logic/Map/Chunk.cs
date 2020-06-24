using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    class Chunk
    {
        private List<Layer> _layers;
        private int[,] _collisionLayer;

        public Chunk(int[,] collisionLayer)
        {
            _collisionLayer = collisionLayer;
        }

        public void LoadContent()
        {
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
    }
}
