using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using SpriteFontPlus;

namespace MonoFantasy.Content.FontLoader
{
    public static class FontLoader
    {
        public static string RootDirectory = "../../../../Content/Fonts/";
        public static SpriteFont LoadFont(string fileLocation, int fontSize, GraphicsDevice graphicsDevice)
        {
            return TtfFontBaker.Bake(File.ReadAllBytes(RootDirectory + fileLocation),
                    fontSize, 1024, 1024, new CharacterRange[] { CharacterRange.BasicLatin }).CreateSpriteFont(graphicsDevice);
        }
    }
}
