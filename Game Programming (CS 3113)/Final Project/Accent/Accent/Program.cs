using System;

namespace Accent
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ActGame game = new ActGame())
            {
                game.Run();
            }
        }
    }
#endif
}

