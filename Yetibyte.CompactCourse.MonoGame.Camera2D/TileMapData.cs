using System;
using System.Linq;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    [Serializable]
    public class TileMapData
    {
        public string TileSetName { get; set; }

        public int TileSize { get; set; }

        public TileData[][] Tiles { get; set; }

        public int Width => Tiles?.Max(y => y.Length) ?? 0;

        public int Height => Tiles?.Length ?? 0;

        public static TileMapData FromJson(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<TileMapData>(json);
        }

    }
}
