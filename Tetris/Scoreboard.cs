
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal class Scoreboard
{
    public int score;
    int previousScore, highScore, level;
    bool highScoreVisible;
    Vector2 posTitle, posScore, posHighScore, posLevel, posNextBlockText, posNextBlock, posHoldingText, posHoldingBlock, posEndScore, posEndHighScore, posAccessHelp;
    Color textColor = new Color(85, 55, 55);

    // speed for block movement:
    public float speed { get; private set; }
    public Scoreboard()
    {
    }

    // calculates level
    public int GetLevel()
    {
        int lvl1Bound = 6000;
        int lvl2Bound = 15000;

        if (score == 0) level = 0;
        else if (score > 0 && score <= lvl1Bound)         level = 1;
        else if (score > lvl1Bound && score <= lvl2Bound) level = 2;
        else if (score > lvl2Bound)                       level = 3;

        return level;
    }

    // generates speed based on level
    public double GetSpeed()
    {
        double speed = 2;

        switch (level)
        {
            case 1:
                speed = 1.5;
                break;
            case 2: 
                speed = 1; 
                break;
            case 3: 
                speed = 0.5; 
                break;
        }

        return speed;
    }

    // generates score based on level
    public void ScoreUp(double scoreAdd)
    {
        double scoreMod = 1;

        switch (level)
        {
            case 2:
                scoreMod = 1.3;
                break;
            case 3:
                scoreMod = 1.7;
                break;
        }

        score += (int)Math.Round(scoreAdd * scoreMod);
    }

    // makes it possible for the title to fade into colors
    public Color TitleColor(GameTime gameTime, Block nextBlock)
    {
        Color titleColor = new Color(0, 0, 0);
        double pR, pG, pB;

        // handles the looping
        // the % are added so it keeps looping
        int duration = 10 * 1000;
        double fade = (gameTime.TotalGameTime.Seconds * 1000 + gameTime.TotalGameTime.Milliseconds) % duration;
        double colorFade = fade / duration * nextBlock.colorArray.Length % 1.0;
        
        // determines the color
        // the % makes sure it loops through the array
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
    public void Positions(int pscreenWidth, int pscreenHeight, SpriteFont inconsolata, Block nextBlock)
    {
        int screenWidth = pscreenWidth;
        int screenHeight = pscreenHeight;

        posTitle = new Vector2(screenWidth / 4 * 3, screenHeight / 8);
        posScore = new Vector2(screenWidth / 4 * 3, screenHeight / 4);
        posHighScore = new Vector2(screenWidth / 4 * 3, screenHeight / 4 + inconsolata.MeasureString("Text").Y);
        posLevel = new Vector2(screenWidth / 4 * 3, screenHeight / 4 + 2 * inconsolata.MeasureString("Text").Y);
        
        posNextBlockText = new Vector2(screenWidth / 20 * 11, screenHeight / 2 * 1);
        posNextBlock = new Vector2(posNextBlockText.X, posNextBlockText.Y + inconsolata.MeasureString("Text").Y); ;
        
        // makes sure holdingBlock's position is relative to nextBlock's position
        posHoldingText = posNextBlock;
        posHoldingText.Y += 3 * nextBlock.singleSize;
        posHoldingBlock = posHoldingText;
        posHoldingBlock.Y += inconsolata.MeasureString("Text").Y;

        posAccessHelp = new Vector2(screenWidth / 4 * 3, posHoldingBlock.Y+5*nextBlock.singleSize);

        posEndScore = new Vector2(screenWidth / 2, screenHeight / 5 * 4);
        posEndHighScore = posEndScore;
        posEndHighScore.Y += inconsolata.MeasureString("Text").Y;
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
        if (posTitle == Vector2.Zero) Positions(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, inconsolata, nextBlock);

        spriteBatch.DrawString(bungeeShade, "Tetris", 
        /* position, color      */  posTitle, titleColor,
        /* rotation, origin     */  0, bungeeShade.MeasureString("Tetris") / 2, // so it will center in the middle
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

        spriteBatch.DrawString(inconsolata, "Press H to access the help menu",
                                    posAccessHelp, textColor,
                                    0, inconsolata.MeasureString("Press H to access the help menu") / 2,
                                    0.5f, SpriteEffects.None, 0);
    }

    public void DrawEndScore(SpriteBatch spriteBatch, SpriteFont inconsolata)
    {
        Color endColor = new Color(205, 135, 60);

        spriteBatch.DrawString(inconsolata, "Your score: " + previousScore.ToString(),
                                    posEndScore, endColor,
                                    0, inconsolata.MeasureString("Your score: " + previousScore.ToString()) / 2, // so it will center in the middle
                                    1, SpriteEffects.None, 0);
        if(highScore == previousScore) spriteBatch.DrawString(inconsolata, "That is also your best score!",
                                    posEndHighScore, endColor,
                                    0, inconsolata.MeasureString("That is also your best score!") / 2,
                                    1, SpriteEffects.None, 0);
                               else spriteBatch.DrawString(inconsolata, "Your best score: " + highScore.ToString(),
                                    posEndHighScore, endColor,
                                    0, inconsolata.MeasureString("Your best score: " + highScore.ToString()) / 2,
                                    1, SpriteEffects.None, 0); ;
    }
    public void Reset()
    {
        previousScore = score;

        if (previousScore > highScore)
        {
            highScore = previousScore;
            highScoreVisible = true;
        }
    }
    
}

