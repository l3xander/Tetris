using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

public class Tetris : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private InputHelper inputHelper;
    private Song music;
    private Texture2D sblock, titleScreen, endScreen, helpMenu;
    private Block[] allBlocks;
    SoundEffect placeSound, gameOver;
    SpriteFont inconsolata, bungeeShade;
    enum Gamestates { welcome, play, lost };
    Gamestates currentState;
    Block nextBlock, currentBlock, holdingBlock;
    double currentSpeed, timer;
    Grid grid;
    Scoreboard scoreboard;
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
        sblock = Content.Load<Texture2D>("sprite");
        graphics.PreferredBackBufferWidth = sblock.Width * Grid.gridWidth * 2;
        graphics.PreferredBackBufferHeight = sblock.Height * Grid.gridHeight;
        graphics.ApplyChanges();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        inputHelper = new InputHelper();
        scoreboard = new Scoreboard();
        currentSpeed = 2;
        nextBlock = randomBlock(currentSpeed);
        currentBlock = randomBlock(currentSpeed);
        grid = new Grid();

        titleScreen = Content.Load<Texture2D>("TetrisTitleScreen");
        helpMenu = Content.Load<Texture2D>("helpMenuC");
        endScreen = Content.Load<Texture2D>("endScreen");

        // source of soundeffects: mixkit.co 
        placeSound = Content.Load<SoundEffect>("wood");
        gameOver = Content.Load<SoundEffect>("gameOver");

        // source of fonts: fonts.google.com
        inconsolata = Content.Load<SpriteFont>("Inconsolata");
        bungeeShade = Content.Load<SpriteFont>("BungeeShade");
        music = Content.Load<Song>("tetrismusic");

        // source of music: bass cover by youtuber Davie504
        MediaPlayer.IsRepeating = true;
        //MediaPlayer.Play(music);

    }

    protected override void Update(GameTime gameTime)
    {
        inputHelper.Update(gameTime);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || inputHelper.KeyPressed(Keys.Escape))
            Exit();

        // opens/closes the help menu 
        if (inputHelper.KeyPressed(Keys.H) && !paused) paused = true;
        else if (inputHelper.KeyPressed(Keys.H) && paused) paused = false;

        if (currentState == Gamestates.lost)
        {
            grid.Reset();
            scoreboard.Reset();
        }

        //handels welcome state
        if (currentState == Gamestates.welcome || currentState == Gamestates.lost)
        {
            //MediaPlayer.Pause();
            if (inputHelper.KeyPressed(Keys.Enter))
            {
                scoreboard.score = 0;
                currentState = Gamestates.play;
            }
        }

        //executes everything when game is in play mode
        else if (currentState == Gamestates.play)
        {
            currentSpeed = scoreboard.GetSpeed();
            MediaPlayer.Resume();
            if (!currentBlock.finished(grid) && !paused)
            {
                currentBlock.Move(gameTime, inputHelper, graphics, scoreboard, grid);

                // allows for a block to be 'held' for extra strategy
                if (inputHelper.KeyPressed(Keys.C))
                {
                    if (holdingBlock == null)
                    {
                        Block tempBlock;
                        tempBlock = currentBlock;
                        // checks if block doesn't go out of bounds
                        if (nextBlock.size > currentBlock.size && currentBlock.pos.X > Grid.gridWidth * currentBlock.singleSize - nextBlock.singleSize * nextBlock.size)
                        {
                            for (int y = 0; y > currentBlock.size; y++)
                            {
                                for (int x = 0; x > currentBlock.size; x++)
                                {
                                    currentBlock.array[x, y] = currentBlock.array[x - (nextBlock.size - currentBlock.size), y];
                                }
                            }
                            currentBlock.pos.X -= currentBlock.singleSize * (nextBlock.size - currentBlock.size);
                        }

                        // swaps blocks

                        holdingBlock = currentBlock;
                        currentBlock = nextBlock;
                        nextBlock.pos = tempBlock.pos;
                        nextBlock = randomBlock(currentSpeed);
                    }
                    else
                    {
                        Block tempBlock;
                        tempBlock = currentBlock;
                        // checks if block doesn't go out of bounds
                        if (holdingBlock.size > currentBlock.size && currentBlock.pos.X > Grid.gridWidth * currentBlock.singleSize - holdingBlock.singleSize * holdingBlock.size)
                        {
                            for (int y = 0; y > currentBlock.size; y++)
                            {
                                for (int x = 0; x > currentBlock.size; x++)
                                {
                                    currentBlock.array[x, y] = currentBlock.array[x - (holdingBlock.size - currentBlock.size), y];
                                }
                            }
                            currentBlock.pos.X -= currentBlock.singleSize * (holdingBlock.size - currentBlock.size);
                        }
                        // swaps blocks
                        currentBlock = holdingBlock;
                        currentBlock.pos = tempBlock.pos;
                        holdingBlock = tempBlock;
                    }
                }
            }
            else if (!paused)
            {


                //timer added so the block change isn't so abrupt
                timer += gameTime.ElapsedGameTime.TotalSeconds;

                if (timer > 0.3)
                {
                    grid.Update(currentBlock, scoreboard, gameTime);
                    placeSound.Play();
                    currentBlock = nextBlock;
                    nextBlock = randomBlock(currentSpeed);
                    timer = 0;
                }

                if (grid.IsLost())
                {
                    MediaPlayer.Pause();
                    gameOver.Play();
                    currentState = Gamestates.lost;
                }
            }
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(188, 137, 106));
        spriteBatch.Begin();

        if (currentState == Gamestates.welcome) spriteBatch.Draw(titleScreen, Vector2.Zero, Color.White);
        else if (currentState == Gamestates.lost)
        {
            spriteBatch.Draw(endScreen, Vector2.Zero, Color.White);
            scoreboard.DrawEndScore(spriteBatch, inconsolata);
        }
        else
        {
            grid.Draw(spriteBatch, currentBlock);
            currentBlock.Draw(spriteBatch);
            scoreboard.Draw(spriteBatch, nextBlock, holdingBlock, inconsolata, bungeeShade, graphics, gameTime);
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
