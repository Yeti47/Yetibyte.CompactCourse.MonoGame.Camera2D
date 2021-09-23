using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    /// <summary>
    /// Describes a tile in a <see cref="TileMap"/>.
    /// </summary>
    public class Tile
    {
        #region Properties

        /// <summary>
        /// The index of the tile within the tileset texture.
        /// </summary>
        public Point Index { get; set; }

        /// <summary>
        /// The tilemap this tile belongs to.
        /// </summary>
        public TileMap TileMap { get; private set; }

        /// <summary>
        /// The tile position within the <see cref="TileMap"/>.
        /// </summary>
        public Point Position { get; }

        /// <summary>
        /// True if this tile is impassable.
        /// </summary>
        public bool IsBlocked { get; set; }

        public Rectangle TextureBounds => new Rectangle(Index.X * TileMap.TileSize, Index.Y * TileMap.TileSize, TileMap.TileSize, TileMap.TileSize);

        #endregion

        #region Constructors

        public Tile(Point index, TileMap tileMap, Point position)
        {
            Index = index;
            TileMap = tileMap ?? throw new ArgumentNullException(nameof(tileMap));
            Position = position;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders this tile to the screen using the given sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw with.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 renderPosition = new Vector2(
                Position.X * TileMap.TileSize + TileMap.Position.X,
                Position.Y * TileMap.TileSize + TileMap.Position.Y
            );

            spriteBatch?.Draw(TileMap.Texture, renderPosition, TextureBounds, Color.White);

        }

        #endregion

    }
}
