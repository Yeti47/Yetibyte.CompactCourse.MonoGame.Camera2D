using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Yetibyte.CompactCourse.MonoGame.Camera2D
{
    public class CameraExampleGame : Game
    {
        #region Constants

        private const string TEST_MAP_ASSET_NAME = "testmap.json";
        private const string PLAYER_SPRITE_ASSET_NAME = "F_01";
        private const string CONTENT_ROOT_DIRECTORY = "Content";
        private const string WINDOW_TITLE = "Compact Course: Camera2D";
        private const string DEFAULT_FONT_ASSET_NAME = "DefaultFont";

        #endregion

        #region Fields

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _defaultFont;

        private TileMap _tileMap;
        private Player _player;
        private PlayerController _playerController;

        #endregion

        #region Constructors

        public CameraExampleGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = CONTENT_ROOT_DIRECTORY;
            IsMouseVisible = true;
        }

        #endregion

        #region Methods

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 16 * 30;
            _graphics.PreferredBackBufferHeight = 16 * 30;

            _graphics.ApplyChanges();

            Window.Title = WINDOW_TITLE;

            Window.ClientSizeChanged += (o, e) =>
             {
                 _graphics.PreferredBackBufferWidth = 16 * 30;
                 _graphics.PreferredBackBufferHeight = 16 * 30;

                 _graphics.ApplyChanges();
             };

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _defaultFont = Content.Load<SpriteFont>(DEFAULT_FONT_ASSET_NAME);

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

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _tileMap.Draw(_spriteBatch);
            _player.Draw(_spriteBatch);

            _spriteBatch.End();

            DrawInstructions();

            base.Draw(gameTime);
        }

        private void DrawInstructions()
        {
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            _spriteBatch.DrawString(_defaultFont, "Use WASD to move.", new Vector2(4, 4), Color.White);

            _spriteBatch.End();
        }

        #endregion

    }
}
