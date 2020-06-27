using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFantasy.Logic.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Content.ITexture
{
    class StaticTexture2 : ITexture
    {
        public Tile tile;

        public Texture2D texture;
        public int startY;
        public int startX;
        public int height;
        public int width;

        private Rectangle drawRect;
        private Rectangle srcRect;

        public StaticTexture2(Tile tile, Texture2D texture, Rectangle drawRect, BlockData blockData)
        {
            this.tile = tile;
            this.texture = texture;
            this.drawRect = drawRect;
            startY = blockData.startY;
            startX = blockData.startX;
            height = blockData.height;
            width = blockData.width;
            srcRect = new Rectangle(startX * ConfigInfo.TILE_SIZE, startY * ConfigInfo.TILE_SIZE, width, height);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
;
            spriteBatch.Draw(texture, drawRect, srcRect, Color.White, 0,
                Vector2.Zero, SpriteEffects.None, tile._layer.layerDepth);
        }

        public void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Update()
        {
            // stubbed out
        }
    }
}
