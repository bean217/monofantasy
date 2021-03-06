﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFantasy.States;
using System;
using System.IO;

namespace MonoFantasy
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static readonly int SCREEN_WIDTH = 1280;
        public static readonly int SCREEN_HEIGHT = 720;

        public string Release;

        public State _currentState;

        private State _nextState; 

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public MainGame()
        {
            Directory.SetCurrentDirectory(@"../../../..");
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Release = "Alpha 1.0";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
            _currentState.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            if (_nextState != null)
            {
                _currentState.UnloadContent();
                _nextState.LoadContent();
                _currentState = _nextState;
                _nextState = null;
            }

            // TODO: Add your update logic here
            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // TODO: Add your drawing code here
            _currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
