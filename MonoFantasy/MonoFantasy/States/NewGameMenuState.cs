﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFantasy.Content.FontLoader;
using MonoFantasy.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.States
{
    class NewGameMenuState : State
    {

        private List<Component> Components;

        public NewGameMenuState(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, State lastState) : base(game, graphicsDevice, content, lastState)
        {
            #region Fonts

            var buttonFont = FontLoader.LoadFont("Retron2k/Retron2000.ttf", 36, _graphicsDevice);
            var headerFont = FontLoader.LoadFont("Tangerine/Tangerine.ttf", 144, _graphicsDevice);

            #endregion

            #region Textures

            var backgroundTexture = _content.Load<Texture2D>("Backgrounds/tempMenuBackground");
            var buttonTexture = _content.Load<Texture2D>("Controls/TitleButtonInverted");
            var backButtonTexture = _content.Load<Texture2D>("Controls/backButton");

            #endregion

            #region Background

            var menuBackground = new Image(backgroundTexture, Vector2.Zero);

            #endregion

            #region Text Boxes
            var newGameText = new TextBox(headerFont, "New Game");
            newGameText.Position = new Vector2(_game.Window.ClientBounds.Width / 2 - newGameText.Size.X / 2,
                _game.Window.ClientBounds.Height / 6 - newGameText.Size.Y / 2);
            #endregion

            #region Buttons

            var backButton = new Button(backButtonTexture, null)
            {
                Position = new Vector2(25, 25),
                Text = ""
            };

            backButton.Click += backButton_Click;

            var saveOneButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2,
                _game.Window.ClientBounds.Height / 3),
                Text = "New Game1"
            };

            var saveTwoButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2,
                _game.Window.ClientBounds.Height / 3 + buttonTexture.Height * 2.0f),
                Text = "New Game2"
            };

            var saveThreeButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2,
                _game.Window.ClientBounds.Height / 3 + buttonTexture.Height * 4.0f),
                Text = "New Game3"
            };

            #endregion

            Components = new List<Component>()
            {
                menuBackground,
                backButton,
                newGameText,
                saveOneButton,
                saveTwoButton,
                saveThreeButton
            };
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Back");
            _game.ChangeState(_lastState);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var component in Components)
                component.Draw(gameTime, spriteBatch);
            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                backButton_Click(null, null);

            foreach (var component in Components)
                component.Update(gameTime);
        }
    }
}