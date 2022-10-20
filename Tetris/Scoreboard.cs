
using System;
using Microsoft.Xna.Framework.Graphics;

internal class Scoreboard
{
    int level, score;
    float scoreMod;
    public bool scoreCheck { get; private set; }

    // speed for block movement:
    public float speed { get; private set; }
    public Scoreboard()
    {
        scoreMod = 1;
        // to-do: score goes up based on block movement
    }

    public void LevelCheck()
    {
        // to-do: which values are we using for score to level?
        scoreCheck = false;
        if (score == 0)
        {
            level = 1;
        }
        else if (score > 0 && score <= 100)
        {
            level = 2;
            scoreMod = 1.2f;
            // speed =
        }
        else if (score > 100 && score <= 250)
        {
            level = 3;
            scoreMod = 1.4f;
            //speed =
        }
        else if (score > 250 && score <= 400)
        {
            level = 4;
            scoreMod = 1.6f;
            //speed =
        }
        else if (score > 400)
        {
            level = 5;
            scoreMod = 2;
            //speed =
        }
    }

    public int ScoreUp(int scoreAdd)
    {
        // generates new score based on level
        score += (int)Math.Round(scoreAdd * scoreMod);
        scoreCheck = true;
        LevelCheck();

        return score;
    }


    // todo, check: is there a better way to get the fonts to scoreboard?
    public void Draw(SpriteBatch spriteBatch, SpriteFont roboto, SpriteFont robotoBold, SpriteFont Silkscreen)
    {
        // code for drawing text:
        // spriteBatch.DrawString(robotoBold, "MonoGame Font Test", position, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
        // score: spriteBatch.DrawString(roboto, Score.ToString(), position, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
    
        // to-do: display score and sprite
    }

    public void Reset()
    {
        level = 1;
        score = 0;
    }
    
}

