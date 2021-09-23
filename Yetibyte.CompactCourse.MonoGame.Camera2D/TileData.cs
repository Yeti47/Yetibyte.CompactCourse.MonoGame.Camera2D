using System;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    [Serializable]
    public class TileData
    {
        public int X { get; set; }
        public int Y { get; set; }

        public bool IsBlocked { get; set; }
    }
}
