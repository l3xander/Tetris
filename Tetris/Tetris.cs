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
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        // window size
        graphics.PreferredBackBufferWidth = 696;
        graphics.PreferredBackBufferHeight = 580;
        graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        inputHelper = new InputHelper();
        sblock = Content.Load<Texture2D>("block");
        nextBlock = randomBlock();
        currentBlock = nextBlock;
        //if(block gets put down)
        //{
        //    currentBlock = nextBlock;    
        //    nextBlock = randomBlock();
        //}

        // TODO: use this.Content to load your game content here
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
        spriteBatch.Draw(sblock, new Vector2(12*sblock.Width, 240), Color.Red);
        currentBlock.Draw(spriteBatch);
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

        return allBlocks[r.Next(7)];
    }
}
