using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFantasy.Logic.Map;

namespace MonoFantasy.States
{
    public class GameState : State
    {
        private Map _map;
        private Texture2D map;
        
        public GameState(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, State lastState) : base(game, graphicsDevice, content, lastState)
        {
            LoadContent();
        }

        public override void LoadContent()
        {
            map = _content.Load<Texture2D>("32pxmap");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(map, new Rectangle(0, 0, map.Width, map.Height), Color.White);
            spriteBatch.End();
        }
        public override void PostUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }
    }
}
