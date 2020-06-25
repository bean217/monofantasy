using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    class Tile
    {
        private Chunk _chunk;

        public Tile(Chunk chunk)
        {
            _chunk = chunk;
        }
    }
}
