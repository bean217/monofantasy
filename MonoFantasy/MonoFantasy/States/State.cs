using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.States
{
    public abstract class State
    {
        #region Fields

        protected ContentManager _content;
        
        public GraphicsDevice _graphicsDevice;

        protected MainGame _game;

        protected State _lastState;

        #endregion

        #region Methods

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, State lastState)
        {
            _game = game;
            _graphicsDevice = graphicsDevice;
            _content = content;
            _lastState = lastState;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void LoadContent();

        public void UnloadContent()
        {
            _content.Unload();
        }

        #endregion
    }
}
