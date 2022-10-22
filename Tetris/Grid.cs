using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;

internal class Grid
{
    const int gridWidth = 12;
    const int gridHeight = 20;
    public bool[,] grid { get; private set; }

    public Grid()
    {
        grid = new bool[gridWidth, gridHeight];

    }
    public void Update()
    {
        // todo: game states
    }
    public void RemoveLine()
    {
        bool rowIsFull;

        // checks all rows
        for (int y = 0; y < gridHeight; y++)
        {
            rowIsFull = true;

            // if one is false, this loop stops
            for (int x = 0; x < gridWidth; x++)
            {
                if (grid[x, y] == false)
                {
                    rowIsFull = false;
                    break;
                }
            }

            if (rowIsFull)
            {
                // copies all values of each row to the row below
                for (int yDrop = y - 1; yDrop >= 0; yDrop--)
                {
                    for (int xCopy = 0; xCopy < gridWidth; xCopy++)
                    {
                        grid[xCopy, yDrop + 1] = grid[xCopy, yDrop];

                    }
                }
                // clears the top row (to false)
                for (int xCopy = 0; xCopy < gridWidth; xCopy++)
                {
                    grid[xCopy, 0] = false;
                }

                //ScoreUp(100);
            }
        }
        // todo: communicate this to scoreboard
    }
    public void Draw(SpriteBatch spriteBatch, Texture2D block)
    {
        Vector2 position = new Vector2();

        // draws the sprite on the grid
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                position.X = x * block.Width;
                position.Y = y * block.Height;
                spriteBatch.Draw(block, position, Color.White);
            }
        }
    }
    public void Reset()
    {
        // sets all elements of the grid to false
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                grid[x,y] = false;
            }
        }
    }
    public void IsLost()
    {
        for (int x = 0; x <= gridWidth; x++)
        {
            if (grid[x, gridHeight])
            {
                // gamestate = lost
                // zeg iets tegen het scoreboard
            }
        }
    }
}

