using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public class Tetris : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private InputHelper inputHelper;
    private Texture2D sblock;
    private Block[] allBlocks;
    SpriteFont roboto, robotoBold, silkscreen;

    Block nextBlock, currentBlock;

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
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        inputHelper = new InputHelper();
        sblock = Content.Load<Texture2D>("block");
        nextBlock = randomBlock();
        currentBlock = nextBlock;

        roboto = Content.Load<SpriteFont>("Roboto");
        robotoBold = Content.Load<SpriteFont>("RobotoBold");
        silkscreen = Content.Load<SpriteFont>("Silkscreen");

        //if(block gets put down)
        //{
        //    currentBlock = nextBlock;    
        //    nextBlock = randomBlock();
        //}

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
        
        if (inputHelper.KeyPressed(Keys.Right))
        {
            currentBlock.rotateRight();
        }
        currentBlock.Move(gameTime, inputHelper, graphics);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();
        // grid.Draw("block");
        currentBlock.Draw(spriteBatch);
        // Scoreboard.Draw(roboto, robotoBold, silkscreen);
        spriteBatch.End();
        base.Draw(gameTime);
    }

    private Block randomBlock()
    {
        Random r = new Random();
        allBlocks = new Block[7];
        allBlocks[0] = new BlockL(sblock);
        allBlocks[1] = new BlockR(sblock);
        allBlocks[2] = new BlockS(sblock);
        allBlocks[3] = new BlockT(sblock);
        allBlocks[4] = new Block2(sblock);
        allBlocks[5] = new BlockI(sblock);
        allBlocks[6] = new BlockO(sblock);

        return allBlocks[r.Next(6)];
    }
}
