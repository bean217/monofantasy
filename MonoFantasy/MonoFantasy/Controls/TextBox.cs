using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Controls
{
    class TextBox : Component
    {
        #region Fields

        public SpriteFont Font;

        #endregion

        #region Properties
        
        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Vector2 Size
        {
            get
            {
                return new Vector2((int)Font.MeasureString(Text).X, (int)Font.MeasureString(Text).Y);
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Font.MeasureString(Text).X, (int)Font.MeasureString(Text).Y);
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public TextBox(SpriteFont font, string text)
        {
            Text = text;
            Font = font;
            PenColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!string.IsNullOrEmpty(Text)) {
                var x = Rectangle.X;
                var y = Rectangle.Y;
                spriteBatch.DrawString(Font, Text, new Vector2(x, y), PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Textbox does not need updating
        }

        #endregion
    }
}
