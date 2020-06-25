using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    public class BlockData
    {
        public bool isStatic;
        public int startY;
        public int startX;
        public int height;
        public int width;
        public int numFrames;
        public Vector2 imgAreaSize;
        // number of frames before texture update
        public int frameUpdate;
        public BlockData(string[] blockData)
        {
            try
            {
                if (blockData[1].Equals("0"))
                {
                    isStatic = true;
                }
                else
                {
                    isStatic = false;
                }
                startY = int.Parse(blockData[3]);
                startX = int.Parse(blockData[2]);
                height = ConfigInfo.TILE_SIZE;
                width = ConfigInfo.TILE_SIZE;
                if (!isStatic)
                {
                    numFrames = int.Parse(blockData[4]);
                    imgAreaSize = new Vector2(int.Parse(blockData[5]), int.Parse(blockData[6]));
                    frameUpdate = int.Parse(blockData[7]);
                }
                else
                {
                    numFrames = 1;
                    imgAreaSize = Vector2.One;
                    frameUpdate = 1;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public enum Block
        {
            GREEN_GRASS = 0,
            DARK_GREEN_GRASS = 1,
            MAROON_CARPET = 2,
            LIGHT_MAROON_CARPET = 3,
            GOLD = 4,
            BLACK = 5, 
            DARK_STONE = 6,
            STONE = 7,
            PURPLE_CARPET = 8,
            HOT_PINK_CARPET = 9
        }

        public override string ToString()
        {
            return $"Tile{startX}x{startY}[isStatic: {isStatic}, height: {height}, width: {width}, numFrames: {numFrames}, frameUpdate: {frameUpdate}, Area.X: {imgAreaSize.X}, Area.Y: {imgAreaSize.Y}]";
        }
    }
}
