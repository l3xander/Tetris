﻿
using System;
using Microsoft.Xna.Framework.Graphics;

internal class Scoreboard
{
    int level, score, scoreAdd;
    float scoreMod;

    // speed modifier for block movement:
    public float speedMod { get; private set; }

    public enum gameState {start, ongoing, end};

    public Scoreboard(int points)
    {
        scoreMod = 1;
        scoreAdd = points;
        // to-do: score goes up based on block movement
    }

    public int LevelUp()
    {
        // to-do: which values are we using for score to level?
        if (score == 0)
        {
            level = 1;
        }
        else if (score > 0 && score <= 100)
        {
            level = 2;
            scoreMod = 1.2f;
            //speedMod =
        }
        else if (score > 100 && score <= 250)
        {
            level = 3;
            scoreMod = 1.4f;
            //speedMod =
        }
        else if (score > 250 && score <= 400)
        {
            level = 4;
            scoreMod = 1.6f;
            //speedMod =
        }
        else if (score > 400)
        {
            level = 5;
            scoreMod = 2;
            //speedMod =
        }

        return level;
    }

    public int Score()
    {
        // generates new score based on level
        score += (int)Math.Round(scoreAdd * scoreMod);

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

