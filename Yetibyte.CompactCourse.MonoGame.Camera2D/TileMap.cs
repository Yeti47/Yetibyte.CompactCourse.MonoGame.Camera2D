using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    /// <summary>
    /// Describes a tile-based map.
    /// </summary>
    public class TileMap
    {
        #region Fields

        private readonly Tile[,] _tiles;
        private readonly Point _clearTileIndex;

        #endregion

        #region Properties

        /// <summary>
        /// The texture to use for the tileset.
        /// </summary>
        public Texture2D Texture { get; }

        /// <summary>
        /// The position in world space at which to render the map.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The tile size in pixels.
        /// </summary>
        public int TileSize { get; }

        #endregion

        #region Constructors

        public TileMap(Texture2D texture, Vector2 position, int tileSize, Point mapSize, Point? clearTileIndex = null)
        {
            clearTileIndex ??= Point.Zero;

            Texture = texture;
            Position = position;
            TileSize = tileSize;

            _clearTileIndex = clearTileIndex.Value;

            _tiles = new Tile[mapSize.X, mapSize.Y];

            Clear();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an instance of <see cref="TileMap"/> from the given <see cref="TileMapData"/>.
        /// </summary>
        /// <param name="data">An object encapsulating the tile map's data.</param>
        /// <param name="content">Content manager instance to load content with.</param>
        /// <param name="position">The position of the map in world space.</param>
        /// <returns></returns>
        public static TileMap FromData(TileMapData data, ContentManager content, Vector2 position)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (content is null)
                throw new ArgumentNullException(nameof(content));

            Texture2D texture = content.Load<Texture2D>(data.TileSetName);

            TileMap map = new TileMap(texture, position, data.TileSize, new Point(data.Width, data.Height));

            for(int y = 0; y < data.Height; y++)
            {
                for(int x = 0; x < data.Tiles[y].Length; x++)
                {
                    TileData tileData = data.Tiles[y][x];
                    map.SetTile(new Point(tileData.X, tileData.Y), x, y, tileData.IsBlocked);
                }
            }

            return map;

        }

        public void SetTile(Point tileIndex, int posX, int posY, bool isBlocked)
        {
            if (posX >= _tiles.GetLength(0) || posX < 0)
                throw new ArgumentOutOfRangeException(nameof(posX));

            if (posY >= _tiles.GetLength(1) || posY < 0)
                throw new ArgumentOutOfRangeException(nameof(posY));

            Tile tile = _tiles[posX, posY];
            tile.Index = tileIndex;
            tile.IsBlocked = isBlocked;

        }

        public void Clear()
        {
            for(int y = 0; y < _tiles.GetLength(1); y++)
            {
                for (int x = 0; x < _tiles.GetLength(0); x++)
                {
                    Tile tile = _tiles[x, y];

                    if(tile == null)
                    {
                        tile = new Tile(_clearTileIndex, this, new Point(x, y));
                        _tiles[x, y] = tile;
                    }
                    else
                    {
                        tile.Index = _clearTileIndex;
                    }

                    tile.IsBlocked = false;
                }
            }
        }

        /// <summary>
        /// Renders this <see cref="TileMap"/> to the screen.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                for (int x = 0; x < _tiles.GetLength(0); x++)
                {
                    Tile tile = _tiles[x, y];
                    tile.Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Gets the center of the tile the given world position lies on.
        /// </summary>
        /// <param name="worldPosition">The position in world space to get the tile center for.</param>
        /// <returns>The centered position of the tile.</returns>
        public Vector2 SnapToTilePosition(Vector2 worldPosition)
        {
            Point tilePos = WorldPositionToTilePosition(worldPosition);

            Vector2 snappedPosition = Position + new Vector2(tilePos.X * TileSize, tilePos.Y * TileSize) + new Vector2(TileSize / 2f, TileSize / 2f);

            return snappedPosition;
        }

        public Point WorldPositionToTilePosition(Vector2 worldPosition)
        {
            Vector2 relativeMapPos = worldPosition - Position;

            return new Point((int)Math.Floor(relativeMapPos.X / (float)TileSize), (int)Math.Floor(relativeMapPos.Y / (float)TileSize));
        }

        /// <summary>
        /// Checks if the tile at the specified position is blocked.
        /// </summary>
        /// <param name="posX">The x position of the tile.</param>
        /// <param name="posY">The y position of the tile.</param>
        /// <returns>True if the tile is impassable; false otherwise.</returns>
        public bool IsTileBlocked(int posX, int posY)
        {
            if (posX >= _tiles.GetLength(0) || posX < 0)
                throw new ArgumentOutOfRangeException(nameof(posX));

            if (posY >= _tiles.GetLength(1) || posY < 0)
                throw new ArgumentOutOfRangeException(nameof(posY));

            return _tiles[posX, posY].IsBlocked;
        }

        #endregion

    }
}
