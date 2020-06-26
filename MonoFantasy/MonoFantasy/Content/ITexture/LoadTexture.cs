using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoFantasy.Content.ITexture
{
    public static class LoadTexture
    {
        public static Texture2D Load(GraphicsDevice graphics, string dir)
        {
            FileStream fileStream = new FileStream(dir, FileMode.Open);
            Texture2D texture = Texture2D.FromStream(graphics, fileStream);
            fileStream.Dispose();
            return texture;
        }
    }
}
