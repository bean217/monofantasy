﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy
{
    class Button : Component
    {
        #region Fields

        private MouseState CurrentMouse;

        private SpriteFont Font;

        private bool IsHovering;

        private MouseState PreviousMouse;

        private Texture2D Texture;

        #endregion

        #region Properties

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColor { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            Texture = texture;
            Font = font;
            PenColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // If not hovering, hue is white
            var color = Color.White;

            // If hovering, hue is gray
            if (IsHovering)
                color = Color.Gray;

            // Draws Button Texture
            spriteBatch.Draw(Texture, Rectangle, color);

            // Draws Text in center of button if text is not an empty string
            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (Font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (Font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(Font, Text, new Vector2(x, y), PenColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            // Updates mouse state
            PreviousMouse = CurrentMouse;
            CurrentMouse = Mouse.GetState();

            // Creates a 1x1px collision box (rectangle) for mouse
            var mouseRectangle = new Rectangle(CurrentMouse.X, CurrentMouse.Y, 1, 1);

            // If mouseRectangle is not on the button, then not hovering
            IsHovering = false;

            // If mouseRectangle is on the button, hovering is true
            if (mouseRectangle.Intersects(Rectangle))
            {
                IsHovering = true;
                // If the user has clicked and released on the button, then invoke an action
                if (CurrentMouse.LeftButton == ButtonState.Released && PreviousMouse.LeftButton == ButtonState.Pressed)
                {
                    // If EventHandler Click (field) is not null, invokes an event
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
