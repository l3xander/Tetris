
using Microsoft.Xna.Framework.Graphics;

internal class Scoreboard
{
    int level, score;
    
    public Scoreboard()
    {
        // voor level: switch function?
    }

    public int LevelUp()
    {
        return level;
    }

    public int Score()
    {
        return score;
    }

    public void Draw(SpriteFont roboto, SpriteFont robotoBold, SpriteFont Silkscreen)
    {
       // code for drawing text
       // _spriteBatch.DrawString(robotoBold, "MonoGame Font Test", position, Color.White, 0, new Vector2(0, 0), 1.0f, SpriteEffects.None, 0.5f);
    }

    // todo: levels extra speed

    public void Reset()
    {
        level = 1;
        score = 0;
    }
    
}

