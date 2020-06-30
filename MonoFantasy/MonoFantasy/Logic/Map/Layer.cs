using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Map
{
    class Layer
    {
        public Chunk _chunk;
        public int _layerNum;
        public string layerDir;
        public static readonly string TILE_FILE = "tiles.txt";

        public float layerDepth;

        public Tile[,] _tiles;
        
        public Layer(Chunk chunk, int layerNum)
        {

            _tiles = new Tile[Chunk.WIDTH, Chunk.HEIGHT];
            _chunk = chunk;
            _layerNum = layerNum;

            if (_layerNum == 0)
            {
                layerDepth = 1.0f;
            } else
            {
                layerDepth = (float)(((_chunk.numLayers - 1) - _layerNum) * 0.001f);
            }

            layerDir = $"{_chunk._chunkDir}/layer{_layerNum}";
            BlockData[,] layerBlockData = LayerTileReader.getLayerData($"{layerDir}/{TILE_FILE}", _chunk._map.chunkTileData);
            _tiles = new Tile[ConfigInfo.CHUNK_WIDTH, ConfigInfo.CHUNK_HEIGHT];
            for (int j = 0; j < ConfigInfo.CHUNK_HEIGHT; j++)
            {
                for (int i = 0; i < ConfigInfo.CHUNK_WIDTH; i++)
                {
                    _tiles[i, j] = new Tile(this, layerBlockData[i, j], _chunk._chunkPosX * ConfigInfo.CHUNK_WIDTH * ConfigInfo.TILE_SIZE,
                        _chunk._chunkPosY * ConfigInfo.CHUNK_HEIGHT * ConfigInfo.TILE_SIZE, i, j);
                }
            }
        }

        public void LoadContent()
        {
            // Loading tiles will requies reading from a chunk text file with
            // texture mappings. These texture mappings will be used to relate
            // to a 32x32px texture in the texture map image file.
            for (int j = 0; j < ConfigInfo.CHUNK_HEIGHT; j++)
            {
                for (int i = 0; i < ConfigInfo.CHUNK_WIDTH; i++)
                {
                    _tiles[i, j].LoadContent();
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            
            for (int j = 0; j < ConfigInfo.CHUNK_HEIGHT; j++)
            {
                for (int i = 0; i < ConfigInfo.CHUNK_WIDTH; i++)
                {
                    _tiles[i, j].Draw(gameTime, spriteBatch);
                }
            }
        }

        public void Update()
        {
            for (int j = 0; j < ConfigInfo.CHUNK_HEIGHT; j++)
            {
                for (int i = 0; i < ConfigInfo.CHUNK_WIDTH; i++)
                {
                    _tiles[i, j].Update();
                }
            }
        }

    }
}
