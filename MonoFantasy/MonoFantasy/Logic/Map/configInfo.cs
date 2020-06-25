using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    struct ConfigInfo
    {
        public static readonly int TILE_SIZE = 32;
        public static readonly int CHUNK_WIDTH = 41;
        public static readonly int CHUNK_HEIGHT = 23;
        public static readonly string TEXTURE_FILE_REF = "textureFile";
        public static readonly string NUM_LAYERS_REF = "numLayers";
        public static readonly string COLLISION_FILE_REF = "collisionFile";
    }
}
