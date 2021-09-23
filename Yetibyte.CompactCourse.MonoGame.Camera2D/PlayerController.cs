using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    public class PlayerController
    {
        private readonly Player _player;

        public PlayerController(Player player)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if(keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
            {
                _player.MoveUp();
            }
            else if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
            {
                _player.MoveDown();
            }
            else if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
            {
                _player.MoveLeft();
            }
            else if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
            {
                _player.MoveRight();
            }
        }

    }
}
