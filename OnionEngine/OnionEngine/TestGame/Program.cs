using System;
using OnionEngine;

namespace OnionTestGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (OEGame game = new OEGame())
            {
                OE.SetStage(new TestStage());
                game.Run();

                
                //game.InitGame();
            }
        }
    }
#endif
}

