using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFantasy.Content.ITexture;
using MonoFantasy.Logic.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Player
{
    class Player : Sprite
    {
        public Map.Map map;
        public int gameNum;
        public Data initPlayerData;
        
        public GraphicsDevice graphics;
        public string playerDir;

        public Player(Map.Map map) : base()
        {
            this.map = map;
            gameNum = map._gameState._gameNum;
            initPlayerData = InitReader.Read(gameNum);
            Position = new Vector2(initPlayerData.spawn.X, initPlayerData.spawn.Y);
            graphics = map._gameState._graphicsDevice;
            playerDir = $"{map._gameState._gameDir}/player";
        }

        public override void LoadContent()
        {
            texture = LoadTexture.Load(graphics, $"{playerDir}/sprites/sprite.png");
            
            map.LoadContent();
        }

        public override void Update()
        {
            //Update Map
            map.Update();

            //Update Player Movement
            var velocity = new Vector2();
            var speed = 3f;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                velocity.Y = -speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                velocity.Y = speed;

            if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X = -speed;
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X = speed;

            Position += velocity;
        }

        //This method will be fixed later to implement player sprite animation
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw Map
            map.Draw(gameTime, spriteBatch);

            //Draw Player
            int playerWidth = texture.Width * 2;
            int playerHeight = texture.Height * 2;
            spriteBatch.Draw(texture, new Rectangle((int)Position.X - (playerWidth / 2), (int)Position.Y - (playerWidth / 2), playerWidth, playerHeight), 
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            
        }


        public Vector2 getPosition()
        {
            return new Vector2(Position.X / 32, Position.Y / 32);
        }




        public class Data
        {
            public static readonly string SPAWN_VECTOR_KEY= "spawn";
            
            // The starting spawn location of the player based on the game tile system
            public Vector2 spawn;

            public Data(Dictionary<string, string> initData)
            {
                string[] spawnVectorData = initData[SPAWN_VECTOR_KEY].Split(' ');
                spawn = new Vector2(int.Parse(spawnVectorData[0]) * ConfigInfo.TILE_SIZE, 
                    int.Parse(spawnVectorData[1]) * ConfigInfo.TILE_SIZE);
            }
        }

        public static class InitReader
        {
            public static Data Read(int saveNum)
            {
                Dictionary<string, string> initData = new Dictionary<string, string>();
                StreamReader sr = null;
                try
                {
                    string file = $"saves/save{saveNum}/game/player/init.txt";
                    if (!File.Exists(file))
                        throw new FileNotFoundException($"Directory not found: {Directory.GetCurrentDirectory()}/{file}");
                    sr = new StreamReader(file);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // '#' denotes a comment
                        if (line[0].Equals('#'))
                            continue;
                        string[] strLine = line.Split('=');
                        Console.WriteLine("                   PRINTING");
                        Console.WriteLine(line);
                        initData[strLine[0].ToLower()] = strLine[1];
                    }

                }
                catch (FileNotFoundException fnfe)
                {
                    throw fnfe;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                finally
                {
                    if (sr != null)
                        sr.Close();
                }
                return new Data(initData);
            }
        }
    }
}
