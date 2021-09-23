using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{

    public class TileMap
    {

        private readonly Tile[,] _tiles;
        private readonly Point _clearTileIndex;

        public Texture2D Texture { get; }
        public Vector2 Position { get; set; }
        public int TileSize { get; }

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
                for(int x = 0; x < data.Width; x++)
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

        public bool IsTileBlocked(int posX, int posY)
        {
            if (posX >= _tiles.GetLength(0) || posX < 0)
                throw new ArgumentOutOfRangeException(nameof(posX));

            if (posY >= _tiles.GetLength(1) || posY < 0)
                throw new ArgumentOutOfRangeException(nameof(posY));

            return _tiles[posX, posY].IsBlocked;
        }

    }
}
