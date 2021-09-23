using System;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CameraExampleGame())
                game.Run();
        }
    }
}
