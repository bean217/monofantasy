using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoFantasy.Controls;
using MonoFantasy.Content.FontLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteFontPlus;
using System.IO;

namespace MonoFantasy.States
{
    class MenuState : State
    {
        private List<Component> Components;

        public MenuState(MainGame game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            #region Fonts

            var buttonFont = FontLoader.LoadFont("Retron2k/Retron2000.ttf", 36, _graphicsDevice);

            var titleFont1 = FontLoader.LoadFont("ModernSans/micross.ttf", 144, _graphicsDevice);
            var titleFont2 = FontLoader.LoadFont("Tangerine/Tangerine.ttf", 144, _graphicsDevice);

            #endregion

            #region Textures

            var backgroundTexture = _content.Load<Texture2D>("Backgrounds/tempMenuBackground");
            var buttonTexture = _content.Load<Texture2D>("Controls/TitleButtonInverted");

            #endregion

            #region Background

            var menuBackground = new Image(backgroundTexture, Vector2.Zero);

            #endregion

            #region Text Boxes

            #region Title
            var monoText = new TextBox(titleFont1, "Mono");
            
            var fantasyText = new TextBox(titleFont2, "fantasy");
            
            float titleWidth = monoText.Size.X + fantasyText.Size.X;

            monoText.Position = new Vector2(_game.Window.ClientBounds.Width / 2 - titleWidth / 2,
                _game.Window.ClientBounds.Height / 6 - monoText.Size.Y / 2);

            fantasyText.Position = new Vector2(_game.Window.ClientBounds.Width / 2 - titleWidth / 2 + monoText.Size.X + 16,
                _game.Window.ClientBounds.Height / 6 - fantasyText.Size.Y / 2);

            #endregion

            #region Author
            var CSHText = new TextBox(buttonFont, "Computer Science House");
            CSHText.Position = new Vector2(5, _game.Window.ClientBounds.Height - (5 + (int)buttonFont.MeasureString(CSHText.Text).Y));
            #endregion

            #region Release

            var ReleaseText = new TextBox(buttonFont, $"Release: {game.Release}");
            ReleaseText.Position = new Vector2(_game.Window.ClientBounds.Width - (5 + (int)buttonFont.MeasureString(ReleaseText.Text).X), 
                _game.Window.ClientBounds.Height - (5 + (int)buttonFont.MeasureString(ReleaseText.Text).Y));

            #endregion

            #endregion

            #region Buttons

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2, (_game.Window.ClientBounds.Height / 3)),
                Text = "New Game"
            };

            newGameButton.Click += NewGameButton_Click;

            var continueGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2, (_game.Window.ClientBounds.Height / 3) + buttonTexture.Height * 1.5f),
                Text = "Continue"
            };

            continueGameButton.Click += continueGameButton_Click;

            var settingsGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2, (_game.Window.ClientBounds.Height / 3) + buttonTexture.Height * 3.0f),
                Text = "Settings"
            };

            settingsGameButton.Click += settingsGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2, (_game.Window.ClientBounds.Height / 3) + buttonTexture.Height * 4.5f),
                Text = "Quit"
            };

            quitGameButton.Click += quitGameButton_Click;
            
            #endregion

            Components = new List<Component>()
            {
                menuBackground,
                newGameButton,
                continueGameButton,
                settingsGameButton,
                quitGameButton,
                monoText,
                fantasyText,
                CSHText,
                ReleaseText
            };
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("New Game");
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
            // Load new State
        }

        private void continueGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Continue");
        }

        private void settingsGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Settings");
        }

        private void quitGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Quit");
            _game.Exit();
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
            // Remove sprites if they are not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in Components)
                component.Update(gameTime);
        }
    }
}
