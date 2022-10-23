
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal class Scoreboard
{
    int score, highScore, level;
    bool highScoreVisible;
    Vector2 posTitle, posScore, posHighScore, posLevel, posNextBlockText, posNextBlock, posHoldingText, posHoldingBlock;
    public bool scoreCheck { get; private set; }

    // speed for block movement:
    public float speed { get; private set; }
    public Scoreboard()
    {
    }

    public int GetLevel()
    {
        if (score == 0) level = 0;
        else if (score > 0 && score <= 10) level = 1;
        else if (score > 10 && score <= 2000) level = 2;
        else if (score > 3000) level = 3;

        return level;
    }

    public double NextSpeed(int speedAdd)
    {
        double speed = 2;

        switch (level)
        {
            case 1:
                speed = 1.5;
                break;
            case 2: 
                speed = 1.5; 
                break;
            case 3: 
                speed = 1; 
                break;
        }

        return speed;
    }

    public int ScoreUp(double scoreAdd)
    {
        double scoreMod = 1;

        switch (level)
        {
            case 1:
                scoreMod = 1.2;
                break;
            case 2:
                scoreMod = 1.5;
                break;
            case 3:
                scoreMod = 2;
                break;
        }

        // generates new score based on level
        score += (int)Math.Round(scoreAdd * scoreMod);
        scoreCheck = true;

        return score;
    }

    // makes it possible for the title to fade into colors
    public Color TitleColor(GameTime gameTime, Block nextBlock)
    {
        Color titleColor = new Color(0, 0, 0);
        double pR, pG, pB;

        // handles the duration
        // the % are added so it keeps looping, this avoids lots of if statements
        int duration = 10 * 1000;
        double fade = (gameTime.TotalGameTime.Seconds * 1000 + gameTime.TotalGameTime.Milliseconds) % duration;
        double colorFade = fade / duration * nextBlock.colorArray.Length % 1.0;
        
        // determines the color
        // the % makes sure it doesn't go out of the array
        int index = (int)(fade / duration * nextBlock.colorArray.Length);
        int nextIndex = (index + 1) % nextBlock.colorArray.Length;

        Color firstColor = nextBlock.colorArray[index];

        // figures out the RGB difference between the current color and the next
        pR = nextBlock.colorArray[nextIndex].R - nextBlock.colorArray[index].R;
        pG = nextBlock.colorArray[nextIndex].G - nextBlock.colorArray[index].G;
        pB = nextBlock.colorArray[nextIndex].B - nextBlock.colorArray[index].B;

        // changes the color
        titleColor.R = (byte)(firstColor.R + pR * colorFade);
        titleColor.G = (byte)(firstColor.G + pG * colorFade);
        titleColor.B = (byte)(firstColor.B + pB * colorFade);

        return titleColor;
    }

    // initializes positions, relative to screen size
    public void Positions(GraphicsDeviceManager graphics, SpriteFont inconsolata, Block nextBlock)
    {
        posTitle = new Vector2(graphics.PreferredBackBufferWidth / 4 * 3, graphics.PreferredBackBufferHeight / 8);
        posScore = new Vector2(graphics.PreferredBackBufferWidth / 4 * 3, graphics.PreferredBackBufferHeight / 4);
        posHighScore = new Vector2(graphics.PreferredBackBufferWidth / 4 * 3, graphics.PreferredBackBufferHeight / 4 + inconsolata.MeasureString("Text").Y);
        posLevel = new Vector2(graphics.PreferredBackBufferWidth / 4 * 3, graphics.PreferredBackBufferHeight / 4 + 2 * inconsolata.MeasureString("Text").Y);
        
        posNextBlockText = new Vector2(graphics.PreferredBackBufferWidth / 20 * 11, graphics.PreferredBackBufferHeight / 2 * 1);
        posNextBlock = new Vector2(posNextBlockText.X, posNextBlockText.Y + inconsolata.MeasureString("Text").Y); ;
        
        // makes sure holdingBlock's position is relative to nextBlock's position
        posHoldingText = posNextBlock;
        posHoldingText.Y += nextBlock.size * nextBlock.singleSize;
        posHoldingBlock = posHoldingText;
        posHoldingBlock.Y += inconsolata.MeasureString("Text").Y;
    }

    // draws the blocks in the scoreboard
    public void DrawBlocks(Block block, SpriteBatch spriteBatch, Vector2 position)
    {
        if (block != null)
        {
            for (int i = 0; i < block.size; i++)
            {
                for (int j = 0; j < block.size; j++)
                {
                    if (block.array[i, j]) spriteBatch.Draw(block.spBlock, new Vector2(position.X + block.singleSize * i, position.Y + block.singleSize * j), block.color);
                }
            }
        }
    }

    public void Draw(SpriteBatch spriteBatch, Block nextBlock, Block holdingBlock, SpriteFont inconsolata, SpriteFont bungeeShade, GraphicsDeviceManager graphics, GameTime gameTime)
    {
        Color titleColor = TitleColor(gameTime, nextBlock);
        if (posTitle == Vector2.Zero) Positions(graphics, inconsolata, nextBlock);
        Color textColor = new Color(85, 55, 55);

        spriteBatch.DrawString(bungeeShade, "Tetris", 
        /* position, color      */  posTitle, titleColor,
        /* rotation, origin     */  0, bungeeShade.MeasureString("Tetris") / 2, // sets origin to the middle of the text, so it will center in the middle
        /* scale, effect, depth */  1, SpriteEffects.None, 0);

        spriteBatch.DrawString(inconsolata, "Score: "+score.ToString(), 
                                    posScore, textColor, 
                                    0, inconsolata.MeasureString("Score: "+score.ToString())/2, 
                                    1, SpriteEffects.None, 0);

        if(highScoreVisible) spriteBatch.DrawString(inconsolata, "High score: " + highScore.ToString(), 
                                    posHighScore, textColor, 
                                    0, inconsolata.MeasureString("High score: " + highScore.ToString()) / 2, 
                                    1, SpriteEffects.None, 0);

        level = GetLevel();
        spriteBatch.DrawString(inconsolata, "Level: " + level.ToString(),
                                    posLevel, textColor,
                                    0, inconsolata.MeasureString("Level: " + level.ToString()) / 2,
                                    1, SpriteEffects.None, 0);

        spriteBatch.DrawString(inconsolata, "Next block:",
                                    posNextBlockText, textColor,
                                    0, Vector2.Zero,
                                    0.7f, SpriteEffects.None, 0);

        spriteBatch.DrawString(inconsolata, "Holding block:",
                                    posHoldingText, textColor,
                                    0, Vector2.Zero,
                                    0.7f, SpriteEffects.None, 0);

        DrawBlocks(nextBlock, spriteBatch, posNextBlock);
        DrawBlocks(holdingBlock, spriteBatch, posHoldingBlock);
        if(holdingBlock == null)
        {
            spriteBatch.DrawString(inconsolata, "None",
                                    posHoldingBlock, textColor,
                                    0, Vector2.Zero,
                                    0.6f, SpriteEffects.None, 0);
        }
    }

    public void Reset()
    {
        if(score > highScore)
        {
            highScore = score;
            highScoreVisible = true;
        }
        score = 0;
    }
    
}

