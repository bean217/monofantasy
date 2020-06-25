using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    public static class LayerTileReader
    {
        public static BlockData[,] getLayerData(string layerTilesFilename, Dictionary<string, BlockData> chunkTileData)
        {
            BlockData[,] layerData = new BlockData[ConfigInfo.CHUNK_WIDTH, ConfigInfo.CHUNK_HEIGHT];
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(layerTilesFilename);

                int[,] layerTileNums = new int[ConfigInfo.CHUNK_WIDTH, ConfigInfo.CHUNK_HEIGHT];

                string line;
                int x = 0;
                int y = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] strLine = line.Split(' ');
                    foreach (string num in strLine)
                    {
                        layerTileNums[x, y] = int.Parse(num);
                        x = (x + 1) % ConfigInfo.CHUNK_WIDTH;
                    }
                    y = (y + 1) % ConfigInfo.CHUNK_HEIGHT;
                }

                for (int j = 0; j < ConfigInfo.CHUNK_HEIGHT; j++)
                {
                    for (int i = 0; i < ConfigInfo.CHUNK_WIDTH; i++)
                    {
                        layerData[i, j] = chunkTileData[((BlockData.Block)layerTileNums[i, j]).ToString()];
                    }
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            } finally
            {
                if (sr != null)
                    sr.Close();
            }
            return layerData;
        }
    }
}
