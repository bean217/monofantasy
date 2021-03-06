﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoFantasy.Logic.Map;
using MonoFantasy.Content.DirectoryCopy;
using MonoFantasy.Logic.Player;
using MonoFantasy.Logic.Camera;

namespace MonoFantasy.States
{
    public class GameState : State
    {
        public string _saveDir;
        public string _gameDir;
        public int _gameNum;
        private Map _map;
        private Texture2D map;

        private Player player;
        private Camera camera;
        
        public GameState(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, State lastState, int gameNum) : base(game, graphicsDevice, content, lastState)
        {
            _saveDir = $"saves/save{gameNum}";
            _gameDir = $"{_saveDir}/game";
            _gameNum = gameNum;
            CopyDir.DeepCopy("game", $"{_saveDir}/game");
            //LoadContent();
        }

        public override void LoadContent()
        {
            camera = new Camera();

            _map = new Map(this);
            //_map.LoadContent();
            
            #region TemporaryLoad
            player = new Player(_map);
            player.LoadContent();
            #endregion
            
            map = _content.Load<Texture2D>("32pxmap");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, transformMatrix: camera.Transform);
            //spriteBatch.Draw(map, new Rectangle(0, 0, map.Width, map.Height), Color.White);
            //_map.Draw(gameTime, spriteBatch);
            
            #region TemporaryDraw
            player.Draw(gameTime, spriteBatch);
            #endregion
            
            spriteBatch.End();

        }
        public override void PostUpdate(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
            //_map.Update();
            camera.Follow(player);
            #region TemporaryUpdate
            player.Update();
            #endregion
        }
    }
}
