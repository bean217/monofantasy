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
    class AnimatedTexture2 : ITexture
    {
        public Texture2D texture;
        public int startY;
        public int startX;
        public int height;
        public int width;
        private int currentFrame;
        private int totalFrames;
        private int frameUpdate;
        private int frameIncrement;

        private Rectangle drawRect;
        public AnimatedTexture2(Texture2D texture, Rectangle drawRect, BlockData blockData)
        {
            this.texture = texture;
            this.drawRect = drawRect;
            startY = blockData.startY;
            startX = blockData.startX;
            height = blockData.height;
            width = blockData.width;
            currentFrame = 0;
            frameIncrement = 0;
            totalFrames = blockData.numFrames;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            int width = texture.Width / this.width;
            int height = texture.Height / this.height;
            int y = (int)((float)currentFrame / (float)width);
            int x = currentFrame % width;
            Rectangle srcRect = new Rectangle(width * x, height * y, width, height);
            
            spriteBatch.Draw(texture, drawRect, srcRect, Color.White);
        }

        public void setTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Update()
        {
            frameIncrement++;
            if (frameIncrement == frameUpdate)
            {
                frameIncrement = 0;
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }
        }
    }
}
