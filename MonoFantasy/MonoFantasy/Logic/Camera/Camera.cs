using Microsoft.Xna.Framework;
using MonoFantasy.Logic.Map;
using MonoFantasy.Logic.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Logic.Camera
{
    class Camera
    {
        public Matrix Transform { get; private set; }

        public void Follow(Sprite target)
        {
            //int X = -target.Position.X - (target.Rectangle.Width / 2);
            //int Y = ;

            //Console.WriteLine($"     PLAYER[{target.Position.X / 32}, {target.Position.Y / 32}]");

            float X = -(target.Position.X + (target.Rectangle.Width / 2));
            float Y = -(target.Position.Y + (target.Rectangle.Height / 2));

            float minWidth = MainGame.SCREEN_WIDTH / 2;
            float minHeight = MainGame.SCREEN_HEIGHT / 2;
            float maxWidth = ConfigInfo.MAP_WIDTH * ConfigInfo.CHUNK_WIDTH * ConfigInfo.TILE_SIZE - minWidth;
            float maxHeight = ConfigInfo.MAP_HEIGHT * ConfigInfo.CHUNK_HEIGHT * ConfigInfo.TILE_SIZE - minHeight;

            // Checks camera X bounds
            if (-X < minWidth)
                X = -minWidth;
            else if (-X > maxWidth)
                X = -maxWidth;

            // Checks camera Y bounds
            if (-Y < minHeight)
                Y = -minHeight;
            else if (-Y > maxHeight)
                Y = -maxHeight;


            var position = Matrix.CreateTranslation(
                X,
                Y,
                0);

            var offset = Matrix.CreateTranslation(
                MainGame.SCREEN_WIDTH / 2,
                MainGame.SCREEN_HEIGHT / 2,
                0);

            Transform = position * offset;
            Vector2 pos = new Vector2(Transform.Translation.X, Transform.Translation.Y);
            //Console.WriteLine($"          Vector[{pos.X / 32}, {pos.Y / 32}]");
        }
    }
}
