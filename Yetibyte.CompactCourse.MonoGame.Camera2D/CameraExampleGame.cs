using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{

    public class CameraExampleGame : Game
    {
        private const string TEST_MAP_ASSET_NAME = "testmap.json";
        private const string PLAYER_SPRITE_ASSET_NAME = "F_01";
        private const string CONTENT_ROOT_DIRECTORY = "Content";

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private TileMap _tileMap;
        private Player _player;
        private PlayerController _playerController;

        public CameraExampleGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = CONTENT_ROOT_DIRECTORY;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 16 * 30;
            _graphics.PreferredBackBufferHeight = 16 * 30;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            string testMapPath = Path.Combine(Content.RootDirectory, TEST_MAP_ASSET_NAME);

            string testMapJson = File.ReadAllText(testMapPath);

            TileMapData testTileMapData = TileMapData.FromJson(testMapJson);

            _tileMap = TileMap.FromData(testTileMapData, Content, Vector2.Zero);

            Texture2D playerSprite = Content.Load<Texture2D>(PLAYER_SPRITE_ASSET_NAME);

            _player = new Player(playerSprite, _tileMap)
            {
                Position = _tileMap.SnapToTilePosition(new Vector2(80, 80))
            };

            _playerController = new PlayerController(_player);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _playerController.Update(gameTime);
            _player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _tileMap.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
