using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Tetris
{
    public class Tetris : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont roboto, robotoBold;

        public Tetris()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            roboto = Content.Load<SpriteFont>("Roboto");

            // continue when background sprite or format has been decided on
            // the 1.3f adds room for score text
            // _graphics.PreferredBackBufferWidth = background.Width * 1.3f;
            // _graphics.PreferredBackBufferHeight = background.Height;

            _graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // the next code is from the monogame docs, testing to see if font works
            _spriteBatch.Begin();
            // Places text in center of the screen
            Vector2 position = new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            _spriteBatch.DrawString(roboto, "MonoGame Font Test", position, Color.White, 0, position, 1.0f, SpriteEffects.None, 0.5f);
            _spriteBatch.End();

            base.Draw(gameTime);

            // end of project: decide if we want the window to adapt to screen size or keep a set format
        }
    }
}