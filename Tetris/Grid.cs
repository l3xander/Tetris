using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

internal class Grid
{
    const int gridWidth = 12;
    const int gridHeight = 20;
    bool[,] grid;

    public Grid()
    {
        grid = new bool[gridWidth, gridHeight];

    }
    public void Update()
    {

    }
    public void RemoveLine()
    {

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
    public void IsFull()
    {

    }
}

