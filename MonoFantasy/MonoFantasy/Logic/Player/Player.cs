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
        private Vector2 currentChunk;
        
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
            PlayerChunkUpdate();
        }

        public override void LoadContent()
        {
            texture = LoadTexture.Load(graphics, $"{playerDir}/sprites/sprite.png");
            
            map.LoadContent();
        }

        public override void Update()
        {

            PlayerChunkUpdate();
            
            //Update Map
            map.Update(currentChunk);

            PositionUpdate();

            //Console.WriteLine($"     CURRENT CHUNK: Vector[{currentChunk.X}, {currentChunk.Y}]");
            //Console.WriteLine($"          POSITION: Vector[{getPosition().X}, {getPosition().Y}]");
        }

        //This method will be fixed later to implement player sprite animation
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw Map
            map.Draw(gameTime, spriteBatch, currentChunk);

            //Draw Player
            spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height), 
                null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.5f);
            
        }

        private void PlayerChunkUpdate()
        {
            currentChunk = new Vector2((int)(Position.X / (ConfigInfo.CHUNK_WIDTH * ConfigInfo.TILE_SIZE)),
                (int)(Position.Y / (ConfigInfo.CHUNK_HEIGHT * ConfigInfo.TILE_SIZE)));
        }
        private void PositionUpdate()
        {
            //Update Player Movement
            var velocity = new Vector2();
            var speed = 10f;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
                velocity.Y += -speed;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                velocity.Y += speed;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                velocity.X += -speed;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                velocity.X += speed;

            Vector2 newPosition = Position;
            newPosition += velocity;

            float minPosX = 0;
            float minPosY = 0;
            float maxPosX = ConfigInfo.MAP_WIDTH * ConfigInfo.CHUNK_WIDTH * ConfigInfo.TILE_SIZE - Rectangle.Width;
            float maxPosY = ConfigInfo.MAP_HEIGHT * ConfigInfo.CHUNK_HEIGHT * ConfigInfo.TILE_SIZE - Rectangle.Height;
            
            if (newPosition.X < minPosX)
                newPosition.X = minPosX;
            else if (newPosition.X > maxPosX)
                newPosition.X = maxPosX;

            if (newPosition.Y < minPosY)
                newPosition.Y = minPosY;
            else if (newPosition.Y > maxPosY)
                newPosition.Y = maxPosY;
            
            Position = newPosition;
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
