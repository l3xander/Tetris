using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;

public class Tetris : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private InputHelper inputHelper;
    private Texture2D sblock, titleScreen;
    private Block[] allBlocks;
    SpriteFont roboto, robotoBold, silkscreen;
    enum Gamestates {welcome, play, lost};
    Gamestates currentState;
    Block nextBlock, currentBlock;
    double currentSpeed;

    [STAThread]

    static void Main()
    {
        Tetris game = new Tetris();
        game.Run();
    }

    public Tetris()
    {        
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = false;
        //sets the current gameplay state to play (will be changed to welcome once we have a welcome screen!!)
        currentState = Gamestates.play;
    }

    protected override void Initialize()
    {
        // window size
        graphics.PreferredBackBufferWidth = 30*24;
        graphics.PreferredBackBufferHeight = 30*20;
        graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        inputHelper = new InputHelper();
        sblock = Content.Load<Texture2D>("sprite");
        currentSpeed = 2;
        nextBlock = randomBlock(currentSpeed);
        currentBlock = nextBlock;

        roboto = Content.Load<SpriteFont>("Roboto");
        robotoBold = Content.Load<SpriteFont>("RobotoBold");
        silkscreen = Content.Load<SpriteFont>("Silkscreen");
        music = Content.Load<Song>("tetrismusic");

        // continue this later: making screen size bigger
        // so both the game world and the scoreboard are visible
        // graphics.PreferredBackBufferWidth = grid.Width * 1.3f;
    }

    protected override void Update(GameTime gameTime)
    {   
        inputHelper.Update(gameTime);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || inputHelper.KeyPressed(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        
        //executes everything when game is in play mode
        if (currentState == Gamestates.play)
        {
            
            if (!currentBlock.finished) currentBlock.Move(gameTime, inputHelper, graphics);
            else
            {
                currentBlock = nextBlock;
                nextBlock = randomBlock(currentSpeed);
            }

        }
        base.Update(gameTime);

    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();

        spriteBatch.Draw(sblock, new Vector2(12*sblock.Width, 240), Color.Red);

        // grid.Draw("block");

        currentBlock.Draw(spriteBatch);
        // Scoreboard.Draw(roboto, robotoBold, silkscreen);
        spriteBatch.End();
        base.Draw(gameTime);
    }

    private Block randomBlock(double pSpeed)
    {
        Random r = new Random();
        allBlocks = new Block[7];
        allBlocks[0] = new BlockL(sblock, pSpeed);
        allBlocks[1] = new BlockR(sblock, pSpeed);
        allBlocks[2] = new BlockS(sblock, pSpeed);
        allBlocks[3] = new BlockT(sblock, pSpeed);
        allBlocks[4] = new Block2(sblock, pSpeed);
        allBlocks[5] = new BlockI(sblock, pSpeed);
        allBlocks[6] = new BlockO(sblock, pSpeed);

        return allBlocks[r.Next(7)];
    }
}
