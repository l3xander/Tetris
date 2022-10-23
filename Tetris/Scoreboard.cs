
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal class Scoreboard
{
    int score, highScore, level;
    bool highScoreVisible;
    Color titleColor;
    //Vector2 posTitle, posScoreNum, posScoreText, posHighScoreNum, posHighScoreText, posBlockText, posBlock;
    public bool scoreCheck { get; private set; }

    // speed for block movement:
    public float speed { get; private set; }
    public Scoreboard()
    {

    }

    public void LevelCheck()
    {
        if (score == 0) level = 0;
        else if (score > 0 && score <= 1000) level = 1;
        else if (score > 1000 && score <= 2000) level = 2;
        else if (score > 3000) level = 3;

        scoreCheck = false;
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

    // todo, check: is there a better way to get the fonts to scoreboard?
    public void Update()
    {
        LevelCheck();
    }

    public void Draw(SpriteBatch spriteBatch, Block nextBlock, SpriteFont inconsolata, SpriteFont bungeeShade, GraphicsDeviceManager graphics)
    {
        spriteBatch.DrawString(bungeeShade, "Tetris", 
        /* position             */  new Vector2(graphics.PreferredBackBufferWidth / 4 * 3, graphics.PreferredBackBufferHeight / 8),
        /* color, rotation      */  Color.Red, 0, 
        /* origin               */  bungeeShade.MeasureString("Tetris")/2, // sets origin to the middle of the text, so it will center in the middle
        /* scale, effect, depth */  1, SpriteEffects.None, 0);

        spriteBatch.DrawString(inconsolata, "Score: "+score.ToString(), 
                                    new Vector2(graphics.PreferredBackBufferWidth / 4 * 3, graphics.PreferredBackBufferHeight / 4), 
                                    Color.Red, 0, 
                                    inconsolata.MeasureString("Score: "+score.ToString())/2, 
                                    1, SpriteEffects.None, 0);

        if(highScoreVisible) spriteBatch.DrawString(inconsolata, "High score: " + highScore.ToString(), 
                                    new Vector2(graphics.PreferredBackBufferWidth / 4 * 3, graphics.PreferredBackBufferHeight / 4 + inconsolata.MeasureString("Text").Y),
                                    Color.Red, 0, 
                                    inconsolata.MeasureString("High score: " + highScore.ToString()) / 2, 
                                    1, SpriteEffects.None, 0);
        spriteBatch.DrawString(inconsolata, "Next block:",
                                    new Vector2(graphics.PreferredBackBufferWidth / 20 * 11, graphics.PreferredBackBufferHeight / 5 * 2),
                                    new Color(85, 55, 55), 0,
                                    Vector2.Zero,
                                    0.7f, SpriteEffects.None, 0);

        // draw nextBlock
        /*
        this is the better way to draw it but it crashes:
        nextBlock.pos.X = graphics.PreferredBackBufferWidth / 20 * 11;
        nextBlock.pos.Y = graphics.PreferredBackBufferHeight / 5 * 2 + inconsolata.MeasureString("Text").Y;
        nextBlock.Draw(spriteBatch);
        */
        
        for (int i = 0; i < nextBlock.size; i++)
        {
            for (int j = 0; j < nextBlock.size; j++)
            {
                if (nextBlock.array[i, j]) spriteBatch.Draw(nextBlock.spBlock, new Vector2(graphics.PreferredBackBufferWidth / 20 * 11 + nextBlock.singleSize * i, graphics.PreferredBackBufferHeight / 5 * 2 + inconsolata.MeasureString("Text").Y + nextBlock.singleSize*j), nextBlock.color);
            }
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

