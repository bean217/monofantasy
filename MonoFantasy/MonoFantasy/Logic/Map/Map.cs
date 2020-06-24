using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    class Map
    {
        private Chunk[,] _chunks;

        public Map()
        {

        }

        public void LoadContent()
        {
            foreach (var chunk in _chunks)
                chunk.LoadContent();
        }
    }
}
