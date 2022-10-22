using System;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;

internal class Grid
{
    const int gridWidth = 12;
    const int gridHeight = 21;
    public bool[,] grid { get; private set; }
    Color[,] colors;

    public Grid()
    {
        grid = new bool[gridWidth, gridHeight];
        colors = new Color[gridWidth, gridHeight];
    }

    // todo: aan de onderkant een extra rij en die op true

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

                //ScoreUp(100);
            }
        }
        // todo: communicate this to scoreboard
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
                color = Color.White;
                position.X = x * pblock.singleSize;
                position.Y = y * pblock.singleSize;

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
        grid = new bool[gridWidth, gridHeight];
        colors = new Color[gridWidth, gridHeight];
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
                        colors[xGrid + xBlock, yGrid + yBlock] = pblock.color;
                    }
                }
            }
        }
    }



