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
            //Logic.Map.CollisionLayer.test(); THIS MUST BE REMOVED LATER
            using (var game = new MainGame())
                game.Run();
        }
    }
#endif
}
