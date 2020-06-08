using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Controls
{
    class Image : Component
    {
        #region Fields

        private Texture2D Texture;

        #endregion

        #region Properties

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        #endregion

        #region Methods

        public Image(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Rectangle, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            // Stubbed out
        }
        #endregion
    }
}
