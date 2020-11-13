using System;
using System.Windows.Forms;
using Platformer.Editor;

namespace Platformer.Main
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
            using (var game = new Game1())
                game.Run();

            //using (MapEditor editor = new MapEditor())
            //    editor.Run();
        }
    }
#endif
}
