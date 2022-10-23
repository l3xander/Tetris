using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;

internal class Grid
{
    public const int gridWidth = 12;
    public const int gridHeight = 20;
    public double timer;
    public bool[,] grid { get; private set; }
    Color[,] colors;

    public Grid()
    {
        grid = new bool[gridWidth, gridHeight + 1];
        colors = new Color[gridWidth, gridHeight];

        // the +1 is added to make a non-visible row
        // of which all elements will be set to 'true' in the following code
        // this makes it more intuitive to place blocks at the bottom of the grid
        for (int x = 0; x < gridWidth; x++)
        {
            grid[x, gridHeight] = true;
        }
    }

    public void Update(Block pblock, Scoreboard scoreboard, GameTime gameTime)
    {
        Place(pblock);
        RemoveLine(scoreboard);
        timer = 0;
    }
    public void RemoveLine(Scoreboard scoreboard)
    {
        bool rowIsFull;
        int amount = 0;

        // checks all rows
        for (int y = 0; y < gridHeight; y++)
        {
            rowIsFull = true;

            // this loop stops at the first false it finds
            for (int x = 0; x < gridWidth; x++)
            {
                if (!grid[x, y])
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
                        colors[xCopy, yDrop + 1] = colors[xCopy, yDrop];
                    }
                }
                // clears the top row (to false)
                for (int xCopy = 0; xCopy < gridWidth; xCopy++)
                {
                    grid[xCopy, 0] = false;
                }

                amount++;
            }
        }

        // adds score
        // rewards player if multiple rows are completed in one go
        if (amount != 0)
        {
            scoreboard.ScoreUp(100 * amount);
        }
    }
    public void Draw(SpriteBatch spriteBatch, Block pblock)
    {
        Vector2 position = new Vector2();
        Color color;

        // draws the sprite on the grid
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // draws the grid or the "background"
                color = Color.White;
                position.X = x * pblock.singleSize;
                position.Y = y * pblock.singleSize;

                // if there is a block, it will use the corresponding color
                if (grid[x, y])
                {
                    color = colors[x,y];
                }
                spriteBatch.Draw(pblock.spBlock, position, color);
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
                grid[x, y] = false;
            }
        }
        colors = new Color[gridWidth, gridHeight];
    }
    public bool IsLost()
    {
        bool isLost = false;
        for (int x = 0; x < gridWidth; x++)
        {
            if (grid[x, 0])
            {
                return isLost = true;
            }
        }
        return isLost;
    }
        public void Place(Block pblock)
    {
        // figures out the block's position in the grid
        int xGrid = (int)pblock.pos.X / pblock.singleSize;
        int yGrid = (int)pblock.pos.Y / pblock.singleSize;

        // checks which elements of the block array are true
        for (int xBlock = 0; xBlock < pblock.size; xBlock++)
        {
            for (int yBlock = 0; yBlock < pblock.size; yBlock++)
            {
                if (pblock.array[xBlock, yBlock])
                {
                    // places the block in the grid
                    grid[xGrid + xBlock, yGrid + yBlock] = true;

                    // saves the color the block had
                    colors[xGrid + xBlock, yGrid + yBlock] = pblock.color;
                }
            }
        }
    }
}



