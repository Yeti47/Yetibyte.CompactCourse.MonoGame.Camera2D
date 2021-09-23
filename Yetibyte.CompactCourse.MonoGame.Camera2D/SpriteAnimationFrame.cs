using System;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    /// <summary>
    /// Describes a frame in a <see cref="SpriteAnimation"/>.
    /// </summary>
    public class SpriteAnimationFrame
    {
        #region Fields

        private Sprite _sprite;

        #endregion

        #region Properties

        public Sprite Sprite
        {
            get => _sprite;
            set => _sprite = value ?? throw new ArgumentNullException("value", "The sprite cannot be null.");
        }

        public float TimeStamp { get; }

        #endregion

        #region Constructors

        public SpriteAnimationFrame(Sprite sprite, float timeStamp)
        {
            Sprite = sprite;
            TimeStamp = timeStamp;
        }

        #endregion

    }
}
