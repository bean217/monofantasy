using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoFantasy.Logic.Map;
using MonoFantasy.Logic.Player;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Content.ITexture
{
    class AnimatedSprite : ITexture
    {
        private Player player;
        private Texture2D textureAtlas;
        private Rectangle drawRect;
        private Dictionary<Player.MotionState, SpriteData> spriteData;

        private int startX;
        private int startY;
        private int width;
        private int height;
        private int currentFrame;
        private int totalFrames;
        private int frameUpdate;
        private int frameIncrement;

        public AnimatedSprite(Player player, Texture2D textureAtlas, Rectangle drawRect, string spriteData)
        {
            this.spriteData = SpriteData.getSpriteData(spriteData);
            this.textureAtlas = textureAtlas;
            this.drawRect = drawRect;
            this.player = player;
            updateSpriteData(player.currentState);
            currentFrame = 0;
            frameIncrement = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle srcRect;
            if (totalFrames != 1) 
            {
                int y = startY + (int)((float)currentFrame / (float)this.width);
                int x = startX + currentFrame % totalFrames;
                srcRect = new Rectangle(drawRect.Width * x, drawRect.Height * y, drawRect.Width, drawRect.Height);
            } else
            {
                srcRect = new Rectangle(drawRect.Width * startX, drawRect.Height * startY, drawRect.Width, drawRect.Height);
            }
            spriteBatch.Draw(textureAtlas, drawRect, srcRect, Color.White, 0,
                    Vector2.Zero, SpriteEffects.None, player.drawDepth);
        }

        public void setTexture(Texture2D texture)
        {
            // not needed
        }

        public void Update()
        {
            updateSpriteData(player.currentState);

            if (totalFrames != 1) 
            {
                frameIncrement++;
                if (frameIncrement >= frameUpdate)
                {
                    frameIncrement = 0;
                    currentFrame++;
                    if (currentFrame >= totalFrames)
                        currentFrame = 0;
                }
            }
        }

        private void updateSpriteData(Player.MotionState state)
        {
            SpriteData data = spriteData[state];
            startX = data.startX;
            startY = data.startY;
            width = data.width;
            height = data.height;
            totalFrames = data.numFrames;
            frameUpdate = totalFrames != 1 ? spriteData[state].frameUpdate : 1;
            drawRect = player.Rectangle;
        }

        public class SpriteData
        {
            // img starting X pos
            public int startX;
            // img starting Y pos
            public int startY;
            // img frame width
            public int width;
            // img frame height
            public int height;
            // number of frames
            public int numFrames;
            // frames per update
            public int frameUpdate;
            public SpriteData(string data)
            {
                string[] strData = data.Split(' ');
                startX = int.Parse(strData[0]);
                startY = int.Parse(strData[1]);
                width = int.Parse(strData[2]);
                height = int.Parse(strData[3]);
                numFrames = int.Parse(strData[4]);
                frameUpdate = int.Parse(strData[5]);
            }

            public static Dictionary<Player.MotionState, SpriteData> getSpriteData(string location)
            {
                Console.WriteLine("                 " + location);
                Dictionary<Player.MotionState, SpriteData> data = new Dictionary<Player.MotionState, SpriteData>();
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(location);
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line[0].Equals('#'))
                            continue;

                        string[] lineArray = line.Split('=');
                        switch (lineArray[0])
                        {
                            case "MOVE_RIGHT":
                                data.Add(Player.MotionState.MOVE_RIGHT, new SpriteData(lineArray[1]));
                                break;
                            case "MOVE_LEFT":
                                data.Add(Player.MotionState.MOVE_LEFT, new SpriteData(lineArray[1]));
                                break;
                            case "MOVE_UP":
                                data.Add(Player.MotionState.MOVE_UP, new SpriteData(lineArray[1]));
                                break;
                            case "MOVE_DOWN":
                                data.Add(Player.MotionState.MOVE_DOWN, new SpriteData(lineArray[1]));
                                break;
                            case "IDLE_RIGHT":
                                data.Add(Player.MotionState.IDLE_RIGHT, new SpriteData(lineArray[1]));
                                break;
                            case "IDLE_LEFT":
                                data.Add(Player.MotionState.IDLE_LEFT, new SpriteData(lineArray[1]));
                                break;
                            case "IDLE_UP":
                                data.Add(Player.MotionState.IDLE_UP, new SpriteData(lineArray[1]));
                                break;
                            case "IDLE_DOWN":
                                data.Add(Player.MotionState.IDLE_DOWN, new SpriteData(lineArray[1]));
                                break;
                            default:
                                break;
                        }
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    throw e;
                } finally
                {
                    if (sr != null)
                        sr.Close();
                }
                return data;
            }
        }
    }
}
