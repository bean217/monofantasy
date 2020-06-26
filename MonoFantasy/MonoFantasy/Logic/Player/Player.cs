using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoFantasy.Logic.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Player
{
    abstract class Player
    {
        public Map.Map map;
        public int gameNum;
        public Data initPlayerData;
        public Vector2 Position;

        public Player(Map.Map map)
        {
            this.map = map;
            gameNum = map._gameState._gameNum;
            initPlayerData = InitReader.Read(gameNum);
            Position = new Vector2(initPlayerData.spawn.X, initPlayerData.spawn.Y);
        }

        public void LoadContent()
        {

        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Position += new Vector2(0, 1);
            } 
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Position += new Vector2(0, -1);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position += new Vector2(1, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Position += new Vector2(-1, 0);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

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
                    string dir = $"saves/save{saveNum}";
                    if (!Directory.Exists(dir))
                        throw new DirectoryNotFoundException($"Directory not found: {Directory.GetCurrentDirectory()}/{dir}");
                    sr = new StreamReader(dir);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        // '#' denotes a comment
                        if (line[0].Equals('#'))
                            continue;
                        string[] strLine = line.Split('=');
                        initData[strLine[0].ToLower()] = strLine[1];
                    }

                }
                catch (DirectoryNotFoundException dnfe)
                {
                    throw dnfe;
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
