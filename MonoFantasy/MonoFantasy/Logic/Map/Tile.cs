using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFantasy.Content.ITexture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    class Tile
    {
        public BlockData blockData;
        public Layer _layer;
        private static Vector2 size;
        private Vector2 drawPoint;
        public Collision collision;
        // Tile position X relative to chunk
        public int tilePosX;
        // Tile position Y relative to chunk
        public int tilePosY;

        public Rectangle drawRect;

        private ITexture texture;

        public Tile(Layer layer, BlockData blockData, int chunkPosX, int chunkPosY, int tilePosX, int tilePosY)
        {
            this.tilePosX = tilePosX;
            this.tilePosY = tilePosY;
            _layer = layer;
            size = new Vector2(ConfigInfo.TILE_SIZE, ConfigInfo.TILE_SIZE);
            drawPoint = new Vector2(chunkPosX + tilePosX * ConfigInfo.TILE_SIZE, chunkPosY + tilePosY * ConfigInfo.TILE_SIZE);
            drawRect = new Rectangle((int)drawPoint.X, (int)drawPoint.Y, (int)size.X, (int)size.Y);
            this.blockData = blockData;
            if (blockData.isStatic)
            {
                texture = new StaticTexture2(this, null, drawRect, blockData);
            } else
            {
                texture = new AnimatedTexture2(this, null, drawRect, blockData);
            }
            // static: a texture image/map to reference, a rectangle for where the image is drawn in the world
            // animated: same as static, but include number of frames, image area vector, and frame change period (per 30 frames)

            collision = _layer._chunk._collisionLayer[tilePosX, tilePosY];
            
        }

        public void LoadContent()
        {
            texture.setTexture(_layer._chunk._map.tileAtlas);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            texture.Draw(gameTime, spriteBatch);
        }

        public void Update()
        {
            texture.Update();
        }
    }
}
