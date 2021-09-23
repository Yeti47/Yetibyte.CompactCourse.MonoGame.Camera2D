using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    /// <summary>
    /// Represents the player character.
    /// </summary>
    public class Player
    {
        private const float EPSILON = 0.01f;

        private const int SPRITE_WIDTH = 16;
        private const int SPRITE_HEIGHT = 17;

        private const float ANIMATION_FRAME_DURATION = 1 / 8f;

        private const float TILES_PER_SECOND = 6f;

        private readonly SpriteAnimation _walkUpAnimation;
        private readonly SpriteAnimation _walkDownAnimation;
        private readonly SpriteAnimation _walkLeftAnimation;
        private readonly SpriteAnimation _walkRightAnimation;

        private readonly TileMap _tileMap;

        private float _walkTimer = 0;
        private Vector2 _position;
        private bool _wasWalking = false;
        private Vector2 _walkStartPositon;
        private Vector2 _walkDestination;

        /// <summary>
        /// The current world position of this <see cref="Player"/>.
        /// </summary>
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                WalkDestination = Position;
            }
        }

        /// <summary>
        /// The world position that this <see cref="Player"/> is currently walking towards. 
        /// Equals <see cref="Position"/> if the Player is not currently moving.
        /// </summary>
        public Vector2 WalkDestination { 
            get => _walkDestination; 
            private set
            {
                _walkStartPositon = _position;
                _walkDestination = value;
            }
        }

        /// <summary>
        /// An enumeration of all walk animations used by this <see cref="Player"/>.
        /// </summary>
        private IEnumerable<SpriteAnimation> AllAnimations => new[] { _walkUpAnimation, _walkDownAnimation, _walkLeftAnimation, _walkRightAnimation };

        /// <summary>
        /// Whether or not the Player is currently walking.
        /// </summary>
        public bool IsWalking => Vector2.Distance(Position, WalkDestination) > EPSILON;

        /// <summary>
        /// The direction this <see cref="Player"/> is currently facing.
        /// </summary>
        public FaceDirection FaceDirection { get; private set; } = FaceDirection.Down;

        public Player(Texture2D texture, TileMap tileMap)
        {
            _tileMap = tileMap;

            _walkDownAnimation = new SpriteAnimation
            {
                ShouldLoop = true
            };
            _walkDownAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 0, SPRITE_HEIGHT * 0, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 0);
            _walkDownAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 0, SPRITE_HEIGHT * 1, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 1);
            _walkDownAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 0, SPRITE_HEIGHT * 0, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 2);
            _walkDownAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 0, SPRITE_HEIGHT * 2, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 3);
            _walkDownAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 0, SPRITE_HEIGHT * 2, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 4);

            _walkRightAnimation = new SpriteAnimation
            {
                ShouldLoop = true
            };
            _walkRightAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 1, SPRITE_HEIGHT * 0, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 0);
            _walkRightAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 1, SPRITE_HEIGHT * 1, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 1);
            _walkRightAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 1, SPRITE_HEIGHT * 0, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 2);
            _walkRightAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 1, SPRITE_HEIGHT * 2, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 3);
            _walkRightAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 1, SPRITE_HEIGHT * 2, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 4);

            _walkUpAnimation = new SpriteAnimation
            {
                ShouldLoop = true
            };
            _walkUpAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 2, SPRITE_HEIGHT * 0, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 0);
            _walkUpAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 2, SPRITE_HEIGHT * 1, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 1);
            _walkUpAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 2, SPRITE_HEIGHT * 0, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 2);
            _walkUpAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 2, SPRITE_HEIGHT * 2, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 3);
            _walkUpAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 2, SPRITE_HEIGHT * 2, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 4);

            _walkLeftAnimation = new SpriteAnimation
            {
                ShouldLoop = true
            };
            _walkLeftAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 3, SPRITE_HEIGHT * 0, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 0);
            _walkLeftAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 3, SPRITE_HEIGHT * 1, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 1);
            _walkLeftAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 3, SPRITE_HEIGHT * 0, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 2);
            _walkLeftAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 3, SPRITE_HEIGHT * 2, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 3);
            _walkLeftAnimation.AddFrame(new Sprite(texture, SPRITE_WIDTH * 3, SPRITE_HEIGHT * 2, SPRITE_WIDTH, SPRITE_HEIGHT), ANIMATION_FRAME_DURATION * 4);

        }

        public void Update(GameTime gameTime)
        {
            if (IsWalking)
            {
                _wasWalking = true;

                _walkTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                _position = Vector2.Lerp(_walkStartPositon, WalkDestination, _walkTimer / (1f / TILES_PER_SECOND));

                foreach (var animation in AllAnimations)
                    animation.Update(gameTime);

            }
            else
            {
                _wasWalking = false;
                _walkTimer = 0;
                WalkDestination = Position;

                StopAnimations();
            }

        }

        /// <summary>
        /// Stops all animations of this <see cref="Player"/>.
        /// </summary>
        private void StopAnimations()
        {
            foreach (var animation in AllAnimations)
            {
                animation.Stop();
            }
        }

        /// <summary>
        /// Makes the Player start moving towards the specified destination.
        /// Movement is not initiated and false is returned if the destination is occupied by a non-walkable tile or if the
        /// Player is already moving.
        /// </summary>
        /// <param name="destination">The destination to move towards.</param>
        /// <returns>True if the player can move; false otherwise.</returns>
        private bool Move(Vector2 destination)
        {
            if (IsWalking)
                return false;

            Point destinationTilePos = _tileMap.WorldPositionToTilePosition(destination);

            if (_tileMap.IsTileBlocked(destinationTilePos.X, destinationTilePos.Y))
                return false;

            WalkDestination = destination;

            if (!_wasWalking)
            {
                foreach (var animation in AllAnimations)
                {
                    animation.Stop();
                    animation.Play();
                }
            }

            _walkTimer = 0;

            return true;
        }

        /// <summary>
        /// Makes the player start moving up.
        /// Movement is not initiated and false is returned if the destination is occupied by a non-walkable tile or if the
        /// </summary>
        /// <returns>True if the player can move; false otherwise.</returns>
        public bool MoveUp()
        {
            if (IsWalking)
                return false;

            FaceDirection = FaceDirection.Up;

            Vector2 destination = _position - Vector2.UnitY * _tileMap.TileSize;
            destination = _tileMap.SnapToTilePosition(destination);

            return Move(destination);
        }

        /// <summary>
        /// Makes the player start moving down.
        /// Movement is not initiated and false is returned if the destination is occupied by a non-walkable tile or if the
        /// </summary>
        /// <returns>True if the player can move; false otherwise.</returns>
        public bool MoveDown()
        {
            if (IsWalking)
                return false;

            FaceDirection = FaceDirection.Down;

            Vector2 destination = _position + Vector2.UnitY * _tileMap.TileSize;
            destination = _tileMap.SnapToTilePosition(destination);

            return Move(destination);
        }

        /// <summary>
        /// Makes the player start moving right.
        /// Movement is not initiated and false is returned if the destination is occupied by a non-walkable tile or if the
        /// </summary>
        /// <returns>True if the player can move; false otherwise.</returns>
        public bool MoveRight()
        {
            if (IsWalking)
                return false;

            FaceDirection = FaceDirection.Right;

            Vector2 destination = _position + Vector2.UnitX * _tileMap.TileSize;
            destination = _tileMap.SnapToTilePosition(destination);

            return Move(destination);
        }

        /// <summary>
        /// Makes the player start moving left.
        /// Movement is not initiated and false is returned if the destination is occupied by a non-walkable tile or if the
        /// </summary>
        /// <returns>True if the player can move; false otherwise.</returns>
        public bool MoveLeft()
        {
            if (IsWalking)
                return false;

            FaceDirection = FaceDirection.Left;

            Vector2 destination = _position - Vector2.UnitX * _tileMap.TileSize;
            destination = _tileMap.SnapToTilePosition(destination);

            return Move(destination);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 renderPosition = _position - new Vector2(SPRITE_WIDTH / 2f, SPRITE_HEIGHT / 2f);

            SpriteAnimation animation = _walkDownAnimation;

            switch (FaceDirection)
            {
                case FaceDirection.Down:
                    animation = _walkDownAnimation;
                    break;
                case FaceDirection.Up:
                    animation = _walkUpAnimation;
                    break;
                case FaceDirection.Left:
                    animation = _walkLeftAnimation;
                    break;
                case FaceDirection.Right:
                    animation = _walkRightAnimation;
                    break;
                default:
                    break;
            }

            animation.Draw(spriteBatch, renderPosition);
        }

    }
}
