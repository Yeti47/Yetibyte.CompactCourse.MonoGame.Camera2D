using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    /// <summary>
    /// Describes a sprite that can be rendered to the screen.
    /// </summary>
    public class Sprite
    {
        #region Properties

        public Texture2D Texture { get; set; }

        /// <summary>
        /// The x-coordinate within the <see cref="Texture"/> where the sprite's boundaries start.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The <-coordinate within the <see cref="Texture"/> where the sprite's boundaries start.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The width of this sprite in pixels.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// The height of this sprite in pixels.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Tint color with which to render this sprite. White by default.
        /// </summary>
        public Color TintColor { get; set; } = Color.White;

        #endregion

        #region Constructors

        public Sprite(Texture2D texture, int x, int y, int width, int height)
        {
            Texture = texture;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders this sprite to the screen at the given position using the given sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch to draw with.</param>
        /// <param name="position">The world position at which to render the sprite.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {

            spriteBatch.Draw(Texture, position, new Rectangle(X, Y, Width, Height), TintColor);

        }

        #endregion

    }
}
