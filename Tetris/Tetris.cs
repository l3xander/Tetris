using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

    public class Tetris : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        SpriteFont roboto, robotoBold;

         [STAThread]

         static void Main()
         {
            Tetris game = new Tetris();
            game.Run();
         }

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
            robotoBold = Content.Load<SpriteFont>("RobotoBold");


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

            base.Draw(gameTime);

            // end of project: decide if we want the window to adapt to screen size or keep a set format
        }
    }
