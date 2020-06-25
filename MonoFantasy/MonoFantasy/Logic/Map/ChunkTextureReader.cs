using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    public static class ChunkTextureReader
    {
        public static Dictionary<string, BlockData> getChunkTiles(string tileInfoFilename)
        {
            Dictionary<string, BlockData> chunkTiles = new Dictionary<string, BlockData>();

            StreamReader sr = null;
            try
            {
                sr = new StreamReader(tileInfoFilename);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // '#' represents a comment
                    if (line[0] == '#')
                        continue;
                    string[] lineArray = line.Split(' ');
                    BlockData blockData = new BlockData(lineArray);
                    chunkTiles.Add(lineArray[0], blockData);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            } finally
            {
                if (sr != null)
                    sr.Close();
            }
            return chunkTiles;
        }
    }
}
