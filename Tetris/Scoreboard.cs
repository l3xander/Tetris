
internal class Scoreboard
{
    int level, score;
    
    public Scoreboard()
    {

    }

    public int LevelUp()
    {
        return level;
    }

    public int Score()
    {
        return score;
    }

    public void Draw()
    {

    }

    public void Reset()
    {
        level = 0;
        score = 0;
    }
    
}

