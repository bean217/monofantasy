using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Content.ITexture
{
    interface ITexture
    {
        void Update();
        void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        void setTexture(Texture2D texture);
    }
}
