using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    public class Tile
    {
        public Point Index { get; set; }
        public TileMap TileMap { get; private set; }

        public Point Position { get; }
        public bool IsBlocked { get; set; }

        public Rectangle TextureBounds => new Rectangle(Index.X * TileMap.TileSize, Index.Y * TileMap.TileSize, TileMap.TileSize, TileMap.TileSize);

        public Tile(Point index, TileMap tileMap, Point position)
        {
            Index = index;
            TileMap = tileMap ?? throw new ArgumentNullException(nameof(tileMap));
            Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 renderPosition = new Vector2(
                Position.X * TileMap.TileSize + TileMap.Position.X,
                Position.Y * TileMap.TileSize + TileMap.Position.Y
            );

            spriteBatch?.Draw(TileMap.Texture, renderPosition, TextureBounds, Color.White);

        }

    }
}
