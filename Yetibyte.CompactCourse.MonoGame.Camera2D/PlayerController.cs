using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    /// <summary>
    /// Encapsulates logic for controlling a <see cref="Player"/> via keyboard input.
    /// </summary>
    public class PlayerController
    {
        #region Fields

        private readonly Player _player;

        #endregion

        #region Constructors

        public PlayerController(Player player)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

        #endregion

        #region Methods

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.W))
            {
                _player.MoveUp();
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                _player.MoveDown();
            }
            else if (keyboardState.IsKeyDown(Keys.A))
            {
                _player.MoveLeft();
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                _player.MoveRight();
            }
        }

        #endregion

    }
}
