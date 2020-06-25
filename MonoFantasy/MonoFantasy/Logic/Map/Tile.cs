using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    class Tile
    {
        private Layer _layer;
        private static Vector2 size;
        private int _tilePosX;
        private int _tilePosY;
        private Vector2 drawPoint;

        private Texture2D _chunkTexture;

        private Rectangle rectangle;
        private Texture2D texture;

        public Tile(Layer layer, TextureMap texture, int tilePosX, int tilePosY)
        {
            _layer = layer;
            _tilePosX = tilePosX;
            _tilePosY = tilePosY;
            size = new Vector2(32, 32);
            drawPoint = new Vector2(layer._chunk._chunkPosX + tilePosX * 32, layer._chunk._chunkPosY + tilePosY * 32);
            rectangle = new Rectangle((int)drawPoint.X, (int)drawPoint.Y, (int)size.X, (int)size.Y);

        }

        public void LoadContent()
        {
            _chunkTexture = _layer._chunk.tileAtlas;
            texture = 
        }

        private void setTexture()
        {
            
        }
    }
}
