using System;
using System.Linq;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    /// <summary>
    /// Encapsulates serializable tilemap data.
    /// </summary>
    [Serializable]
    public class TileMapData
    {
        public string TileSetName { get; set; }

        public int TileSize { get; set; }

        public TileData[][] Tiles { get; set; }

        public int Width => Tiles?.Max(y => y.Length) ?? 0;

        public int Height => Tiles?.Length ?? 0;

        /// <summary>
        /// Deserializes the given JSON string into an instance of <see cref="TileMapData"/>.
        /// </summary>
        /// <param name="json">The JSON describing the tile map data.</param>
        /// <returns>The deserialized instance.</returns>
        public static TileMapData FromJson(string json)
        {
            return System.Text.Json.JsonSerializer.Deserialize<TileMapData>(json);
        }

    }
}
