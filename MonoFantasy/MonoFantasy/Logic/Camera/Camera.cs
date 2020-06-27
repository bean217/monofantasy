using Microsoft.Xna.Framework;
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
            var position = Matrix.CreateTranslation(
                -target.Position.X - (target.Rectangle.Width / 2),
                -target.Position.Y - (target.Rectangle.Height / 2),
                0);

            var offset = Matrix.CreateTranslation(
                MainGame.SCREEN_WIDTH / 2,
                MainGame.SCREEN_HEIGHT / 2,
                0);

            Transform = position * offset;
        }
    }
}
