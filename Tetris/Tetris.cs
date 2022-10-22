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
    private Song music;
    private Texture2D sblock, titleScreen, endScreen, helpMenu;
    private Block[] allBlocks;
    SpriteFont roboto, robotoBold, silkscreen;
    enum Gamestates {welcome, play, lost};
    Gamestates currentState;
    Block nextBlock, currentBlock;
    double currentSpeed, timer;
    Grid grid;
    public bool paused;

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
        currentState = Gamestates.welcome;
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
        grid = new Grid();

        titleScreen = Content.Load<Texture2D>("TetrisTitleScreen");
        helpMenu = Content.Load<Texture2D>("helpMenu");
        endScreen = Content.Load<Texture2D>("TetrisTitleScreen");

        roboto = Content.Load<SpriteFont>("Roboto");
        robotoBold = Content.Load<SpriteFont>("RobotoBold");
        silkscreen = Content.Load<SpriteFont>("Silkscreen");
        //music = Content.Load<Song>("tetrismusic");

        MediaPlayer.IsRepeating = true;
        MediaPlayer.Play(music);

        // continue this later: making screen size bigger
        // so both the game world and the scoreboard are visible
        // graphics.PreferredBackBufferWidth = grid.Width * 1.3f;

    }

    protected override void Update(GameTime gameTime)
    {   
        inputHelper.Update(gameTime);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || inputHelper.KeyPressed(Keys.Escape))
            Exit();
            
        // opens/closes the help menu 
        if (inputHelper.KeyPressed(Keys.H) && !paused) paused = true;
        else if (inputHelper.KeyPressed(Keys.H) && paused) paused = false;

        //handels welcome state
        if (currentState == Gamestates.welcome || currentState == Gamestates.lost)
        {
            if (inputHelper.KeyPressed(Keys.Enter)) currentState = Gamestates.play;            
        }
        
        //executes everything when game is in play mode
        else if (currentState == Gamestates.play)
        {
      
            if (!currentBlock.finished(grid) && !paused)  
            { 
                currentBlock.Move(gameTime, inputHelper, graphics); 
            }
            else if (!paused)
            {
                //timer added so the block change isn't so abrupt
                timer += gameTime.ElapsedGameTime.TotalSeconds;
                if (timer > 0.3)
                {
                    currentBlock = nextBlock;
                    nextBlock = randomBlock(currentSpeed);
                    timer = 0;
                }
            }
        }

        else 


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin();

        if (currentState == Gamestates.welcome) spriteBatch.Draw(titleScreen, Vector2.Zero, Color.White);
        else if (currentState == Gamestates.lost) spriteBatch.Draw(endScreen, Vector2.Zero, Color.White);
        else
        {
            currentBlock.Draw(spriteBatch);

            // grid.Draw("block");
            // Scoreboard.Draw(roboto, robotoBold, silkscreen);
        }

        if (paused) spriteBatch.Draw(helpMenu, new Vector2(150, 30), Color.White);

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
