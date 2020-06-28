using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFantasy.Logic.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Player
{
    class Sprite
    {
        protected Texture2D texture;

        public Vector2 Position { get; set; }
        public Vector2 Velocity;

        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height); }
        }

        public virtual void LoadContent()
        {
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }

        public Sprite()
        {
            Velocity = Vector2.Zero;
        }

        public virtual void Update()
        {

        }

        #region Collision
        protected bool IsTouchingRight(Tile tile)
        {
            Rectangle Rectangle = tile.drawRect;
            bool isCollision = tile.collision == Collision.COLLIDE;
            return isCollision && 
                this.Rectangle.Right + this.Velocity.X > Rectangle.Left &&
                this.Rectangle.Left < Rectangle.Left &&
                this.Rectangle.Bottom > Rectangle.Top &&
                this.Rectangle.Top < Rectangle.Bottom;
        }

        protected bool IsTouchingLeft(Tile tile)
        {
            Rectangle Rectangle = tile.drawRect;
            bool isCollision = tile.collision == Collision.COLLIDE;
            return isCollision &&
                this.Rectangle.Left + this.Velocity.X < Rectangle.Right &&
                this.Rectangle.Right > Rectangle.Right &&
                this.Rectangle.Bottom > Rectangle.Top &&
                this.Rectangle.Top < Rectangle.Bottom;
        }

        protected bool IsTouchingTop(Tile tile)
        {
            Rectangle Rectangle = tile.drawRect;
            bool isCollision = tile.collision == Collision.COLLIDE;
            return isCollision &&
                this.Rectangle.Bottom + this.Velocity.Y > Rectangle.Top &&
                this.Rectangle.Top < Rectangle.Top &&
                this.Rectangle.Right > Rectangle.Left &&
                this.Rectangle.Left < Rectangle.Right;
        }

        protected bool IsTouchingBottom(Tile tile)
        {
            Rectangle Rectangle = tile.drawRect;
            bool isCollision = tile.collision == Collision.COLLIDE;
            return isCollision &&
                this.Rectangle.Top + this.Velocity.Y < Rectangle.Bottom &&
                this.Rectangle.Bottom > Rectangle.Bottom &&
                this.Rectangle.Right > Rectangle.Left &&
                this.Rectangle.Left < Rectangle.Right;
        }
        #endregion
    }
}
