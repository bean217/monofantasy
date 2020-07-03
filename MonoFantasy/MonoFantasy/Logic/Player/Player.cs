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
        public float drawDepth;
        public Data initPlayerData;
        // position of the current chunk
        private Vector2 currentChunk;
        
        public GraphicsDevice graphics;
        public string playerDir;

        public readonly float speed = 5f;

        public ITexture itexture; //NEW

        public MotionState currentState;
        public MotionState futureState;

        public Player(Map.Map map) : base()
        {
            drawDepth = ConfigInfo.PLAYER_DRAW_DEPTH;
            currentState = MotionState.IDLE_DOWN;
            futureState = currentState;
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
            //texture = LoadTexture.Load(graphics, $"{playerDir}/sprites/sprite.png");
            itexture = new AnimatedSprite(this, LoadTexture.Load(graphics, $"{playerDir}/sprites/spriteAtlas.png"), Rectangle, $"{playerDir}/sprites/spriteData.txt"); // NEW
            map.LoadContent();
        }

        public override void Update()
        {

            PlayerChunkUpdate();
            
            //Update Map
            map.Update(currentChunk);

            PositionUpdate();
            itexture.Update(); // NEW

            //Console.WriteLine($"     CURRENT CHUNK: Vector[{currentChunk.X}, {currentChunk.Y}]");
            //Console.WriteLine($"          POSITION: Vector[{getPosition().X}, {getPosition().Y}]");
        }

        //This method will be fixed later to implement player sprite animation
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Draw Map
            map.Draw(gameTime, spriteBatch, currentChunk);

            //Draw Player
            itexture.Draw(gameTime, spriteBatch); // NEW
            //spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, Rectangle.Width, Rectangle.Height), 
            //    null, Color.White, 0, Vector2.Zero, SpriteEffects.None, drawDepth);
            
        }

        private void PlayerChunkUpdate()
        {
            currentChunk = new Vector2((int)(Position.X / (ConfigInfo.CHUNK_WIDTH * ConfigInfo.TILE_SIZE)),
                (int)(Position.Y / (ConfigInfo.CHUNK_HEIGHT * ConfigInfo.TILE_SIZE)));
        }
        private void PositionUpdate()
        {
            //Update Player Movement
            //var velocity = new Vector2();
            //Velocity = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.W)) 
            {
                Velocity.Y += -speed;
                Direction.Y--;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Velocity.Y += speed;
                Direction.Y++;
            }
                
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Velocity.X += -speed;
                Direction.X--;
            }
                
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Velocity.X += speed;
                Direction.X++;
            }
                

            
            checkTileCollisions();

            //CHANGE MOTION STATE HERE
            updateMotionState(); // NEW

            Vector2 newPosition = Position;
            newPosition += Velocity;

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

            Velocity = Vector2.Zero;
            Direction = Vector2.Zero;
        }

        public Vector2 getPosition()
        {
            return new Vector2((int)(Position.X / ConfigInfo.TILE_SIZE), (int)(Position.Y / ConfigInfo.TILE_SIZE));
        }

        private void checkTileCollisions()
        {
            Vector2 potentialPos = Position + Velocity;
            // get list of tiles around player based on player rectangle size
            int evalTilesWidth = 3;
            int evalTilesHeight = 3;
            if (Rectangle.Width > ConfigInfo.TILE_SIZE)
                evalTilesWidth = 3 + (int)(Rectangle.Width / ConfigInfo.TILE_SIZE);
            if (Rectangle.Height > ConfigInfo.TILE_SIZE)
                evalTilesHeight = 3 + (int)(Rectangle.Height / ConfigInfo.TILE_SIZE);

            Vector2[,] evalTileVectors = new Vector2[evalTilesWidth, evalTilesHeight];

            evalTileVectors[1, 1] = potentialPos;

            for (int y = 0; y < evalTilesHeight; y++)
            {
                for (int x = 0; x < evalTilesWidth; x++)
                {
                    if (x == 1 && y == 1)
                        continue;

                    int X = (int)potentialPos.X + (x - 1) * ConfigInfo.TILE_SIZE;
                    int Y = (int)potentialPos.Y + (y - 1) * ConfigInfo.TILE_SIZE;

                    // check if tile position is in bounds
                    if (!(X < 0 || Y < 0 || X > (ConfigInfo.MAP_WIDTH * ConfigInfo.CHUNK_WIDTH * ConfigInfo.TILE_SIZE - Rectangle.Width) ||
                        Y > (ConfigInfo.MAP_HEIGHT * ConfigInfo.CHUNK_HEIGHT * ConfigInfo.TILE_SIZE - Rectangle.Height)))
                        evalTileVectors[x, y] = new Vector2(X, Y);
                    else
                        evalTileVectors[x, y] = Vector2.Negate(Vector2.One);
                }
            }

            // get chunk object by tile's map position
            Chunk[,] tileChunks = new Chunk[evalTilesWidth, evalTilesHeight];
            for (int y = 0; y < evalTilesHeight; y++)
            {
                for (int x = 0; x < evalTilesWidth; x++)
                {
                    var vector = evalTileVectors[x, y];
                    if (vector.Equals(Vector2.Negate(Vector2.One)))
                        tileChunks[x, y] = null;
                    else 
                        tileChunks[x, y] = map._chunks[(int)(vector.X / (ConfigInfo.CHUNK_WIDTH * ConfigInfo.TILE_SIZE)), 
                            (int)(vector.Y / (ConfigInfo.CHUNK_HEIGHT * ConfigInfo.TILE_SIZE))];
                }
            }

            // get tile object by tile's relative chunk position
            Tile[,] tiles = new Tile[evalTilesWidth, evalTilesHeight];
            for (int y = 0; y < evalTilesHeight; y++)
            {
                for (int x = 0; x < evalTilesWidth; x++)
                {
                    var chunk = tileChunks[x, y];
                    if (chunk == null)
                    {
                        tiles[x, y] = null;
                        continue;
                    }
                        
                    var bgLayer = chunk._layers[0];

                    var vector = evalTileVectors[x, y];
                    Tile tile = bgLayer._tiles[(int)((vector.X % (ConfigInfo.CHUNK_WIDTH * ConfigInfo.TILE_SIZE)) / ConfigInfo.TILE_SIZE),
                        (int)((vector.Y % (ConfigInfo.CHUNK_HEIGHT * ConfigInfo.TILE_SIZE)) / ConfigInfo.TILE_SIZE)];
                    tiles[x, y] = tile;
                }
            }

            for (int y = 0; y < evalTilesHeight; y++)
            {
                for (int x = 0; x < evalTilesWidth; x++)
                {
                    Tile tile = tiles[x, y];
                    if (tile == null)
                        continue;

                    if ((Velocity.X < 0 && IsTouchingLeft(tile)) ||
                        (Velocity.X > 0 && IsTouchingRight(tile)))
                    {
                        // Find X offset from player and tile
                        int offset = 0;
                        if (Velocity.X < 0)
                            offset = (tile.drawRect.Right - Rectangle.Left);
                        else if (Velocity.X > 0)
                            offset = (tile.drawRect.Left - Rectangle.Right);
                        Velocity.X = offset;
                    }
                        

                    if ((Velocity.Y > 0 && IsTouchingTop(tile)) ||
                        (Velocity.Y < 0 && IsTouchingBottom(tile)))
                    {
                        // Find Y offset from player and tile
                        int offset = 0;
                        if (Velocity.Y < 0)
                            offset = (tile.drawRect.Bottom - Rectangle.Top);
                        else if (Velocity.Y > 0)
                            offset = (tile.drawRect.Top - Rectangle.Bottom);
                        Velocity.Y = offset;
                    }
                        
                }
            }
        }

        private void updateMotionState()
        {

            if (Direction.X > 0 && Direction.Y == 0)
                futureState = MotionState.MOVE_RIGHT;
            else if (Direction.X < 0 && Direction.Y == 0)
                futureState = MotionState.MOVE_LEFT;
            if (Direction.Y > 0)
                futureState = MotionState.MOVE_DOWN;
            else if (Direction.Y < 0)
                futureState = MotionState.MOVE_UP;
            if (Direction.X == 0 && Direction.Y == 0)
            {
                switch (currentState)
                {
                    case MotionState.MOVE_RIGHT:
                        futureState = MotionState.IDLE_RIGHT;
                        break;
                    case MotionState.MOVE_LEFT:
                        futureState = MotionState.IDLE_LEFT;
                        break;
                    case MotionState.MOVE_UP:
                        futureState = MotionState.IDLE_UP;
                        break;
                    case MotionState.MOVE_DOWN:
                        futureState = MotionState.IDLE_DOWN;
                        break;
                    case MotionState.IDLE_RIGHT:
                        futureState = MotionState.IDLE_RIGHT;
                        break;
                    case MotionState.IDLE_LEFT:
                        futureState = MotionState.IDLE_LEFT;
                        break;
                    case MotionState.IDLE_UP:
                        futureState = MotionState.IDLE_UP;
                        break;
                    case MotionState.IDLE_DOWN:
                        futureState = MotionState.IDLE_DOWN;
                        break;
                    default:
                        break;
                }
            }

            if (currentState != futureState)
            {
                //Console.WriteLine($"      CURRENT: {currentState}");
                //Console.WriteLine($"       FUTURE: {futureState}");
                currentState = futureState;
            }
                
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

        public enum MotionState
        {
            MOVE_RIGHT,
            MOVE_LEFT,
            MOVE_UP,
            MOVE_DOWN,
            IDLE_RIGHT,
            IDLE_LEFT,
            IDLE_UP,
            IDLE_DOWN
        }
    }
}
