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
    class Player
    {
        public Map.Map map;
        public int gameNum;
        public Data initPlayerData;
        public Vector2 Position;
        public GraphicsDevice graphics;
        public string playerDir;

        private Texture2D texture;

        public Player(Map.Map map)
        {
            this.map = map;
            gameNum = map._gameState._gameNum;
            initPlayerData = InitReader.Read(gameNum);
            Position = new Vector2(initPlayerData.spawn.X, initPlayerData.spawn.Y);
            graphics = map._gameState._graphicsDevice;
            playerDir = $"{map._gameState._gameDir}/player";
        }

        public void LoadContent()
        {
            texture = LoadTexture.Load(graphics, $"{playerDir}/sprites/sprite.png");
        }

        public void Update()
        {
            int speed = 4;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position += new Vector2(0, -1) * speed;
            } 
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position += new Vector2(0, 1) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position += new Vector2(1, 0) * speed;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position += new Vector2(-1, 0) * speed;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, texture.Width * 2, texture.Height * 2), Color.White);
        }


        public Vector2 getPosition()
        {
            return new Vector2(Position.X / 32, Position.Y / 32);
        }




        public class Data
        {
            public static readonly string SPAWN_VECTOR_KEY= "spawn";
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
