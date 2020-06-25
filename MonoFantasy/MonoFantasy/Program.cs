using System;

namespace MonoFantasy
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Logic.Map.CollisionReader.read(@"D:\MonoGame\MonoFantasy\monofantasy\MonoFantasy\MonoFantasy\saves\save1\world\map\chunk0x0\collision.txt", 41, 23);
            using (var game = new MainGame())
                game.Run();
        }
    }
#endif
}
