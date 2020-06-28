using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFantasy.Content.FontLoader;
using MonoFantasy.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonoFantasy.States
{
    class NewGameMenuState : State
    {
        // List of state UI Components
        private List<Component> Components;
        private Dictionary<int, Button> startButtons;

        public NewGameMenuState(MainGame game, GraphicsDevice graphicsDevice, ContentManager content, State lastState) : base(game, graphicsDevice, content, lastState)
        {
            //LoadContent();
        }

        // Loads UI fonts, textures, images, text boxed and buttons
        public override void LoadContent()
        {
            #region Fonts

            var buttonFont = FontLoader.LoadFont("Retron2k/Retron2000.ttf", 36, _graphicsDevice);
            var headerFont = FontLoader.LoadFont("Tangerine/Tangerine.ttf", 144, _graphicsDevice);

            #endregion

            #region Textures

            var backgroundTexture = _content.Load<Texture2D>("Backgrounds/tempMenuBackground");
            var buttonTexture = _content.Load<Texture2D>("Controls/TitleButton");
            var hoverButtonTexture = _content.Load<Texture2D>("Controls/TitleButtonInverted");
            var backButtonTexture = _content.Load<Texture2D>("Controls/backButton");
            var hoverBackButtonTexture = _content.Load<Texture2D>("Controls/backButtonInverted");

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

            var backButton = new Button(backButtonTexture, null, hoverBackButtonTexture)
            {
                Position = new Vector2(25, 25),
                Text = ""
            };

            backButton.Click += backButton_Click;

            var saveOneButton = new Button(buttonTexture, buttonFont, hoverButtonTexture, Color.White)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2,
                _game.Window.ClientBounds.Height / 3),
                Text = System.IO.Directory.Exists("saves/save1/game") ? "Ongoing Game" : "New Game"
            };

            saveOneButton.Click += SaveOneButton_Click;

            var saveTwoButton = new Button(buttonTexture, buttonFont, hoverButtonTexture, Color.White)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2,
                _game.Window.ClientBounds.Height / 3 + buttonTexture.Height * 2.0f),
                Text = System.IO.Directory.Exists("saves/save2/game") ? "Ongoing Game" : "New Game"
            };

            saveTwoButton.Click += SaveTwoButton_Click;

            var saveThreeButton = new Button(buttonTexture, buttonFont, hoverButtonTexture, Color.White)
            {
                Position = new Vector2(_game.Window.ClientBounds.Width / 2 - buttonTexture.Width / 2,
                _game.Window.ClientBounds.Height / 3 + buttonTexture.Height * 4.0f),
                Text = System.IO.Directory.Exists("saves/save3/game") ? "Ongoing Game" : "New Game"
            };

            saveThreeButton.Click += SaveThreeButton_Click;

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

            startButtons = new Dictionary<int, Button>();
            startButtons.Add(1, saveOneButton);
            startButtons.Add(2, saveTwoButton);
            startButtons.Add(3, saveThreeButton);
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Back");
            _game.ChangeState(_lastState);
        }

        private void SaveOneButton_Click(object sender, EventArgs e)
        {
            int saveNum = 1;
            startNewGame(saveNum);
            /*
            Button button = startButtons[1];
            // If the button has already been pressed, do nothing
            if (!button.Pressed)
            {
                string dir = "saves/save1/game";

                GameState gameState;
                button.Pressed = true;
                button.Text = "Loading...";
                Thread thread = new Thread(() => {
                    if (System.IO.Directory.Exists(dir))
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(dir,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                            Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently);
                    }
                    gameState = new GameState(_game, _graphicsDevice, _content, this, 1);
                    _game.ChangeState(gameState);
                    button.Text = "Ongoing Game";
                    button.Pressed = false;
                });
                thread.Start();
            }
            */
        }

        private void SaveTwoButton_Click(object sender, EventArgs e)
        {
            int saveNum = 2;
            startNewGame(saveNum);
        }

        private void SaveThreeButton_Click(object sender, EventArgs e)
        {
            int saveNum = 3;
            startNewGame(saveNum);
        }

        private void startNewGame(int saveNum)
        {
            Button button = startButtons[saveNum];
            // If the button has already been pressed, do nothing
            if (!button.Pressed)
            {
                string dir = $"saves/save{saveNum}/game";

                GameState gameState;
                button.Pressed = true;
                button.Text = "Loading...";
                Thread thread = new Thread(() => {
                    if (System.IO.Directory.Exists(dir))
                    {
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(dir,
                            Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs,
                            Microsoft.VisualBasic.FileIO.RecycleOption.DeletePermanently);
                    }
                    gameState = new GameState(_game, _graphicsDevice, _content, this, saveNum);
                    _game.ChangeState(gameState);
                    button.Text = "Ongoing Game";
                    button.Pressed = false;
                });
                thread.Start();
            }
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
