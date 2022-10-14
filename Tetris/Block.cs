
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

internal class Block
{
    protected bool [,] array;
    protected int size, speed, singleSize;
    Texture2D spBlock;
    protected Color color;
    protected Vector2 pos;

    public Block(Texture2D sprite)
    {
        spBlock = sprite;
        pos = new Vector2 (0, 0);
        singleSize = spBlock.Width;
    }

    public void rotateRight()
    {
        //rotate block

        bool[,] temp = new bool[array.Length,array.Length];

        for(int i = 0; i < size; i++) 
        { 
            for(int j = 0; j < size; j++)
            {
                temp[size-j,i] = array[i,j];
            }
        }
        array = temp;
        
    }

    public void Draw(SpriteBatch spBatch)
    {        
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++) 
            {
                if (array[i,j])spBatch.Draw(spBlock, new Vector2(pos.X + singleSize*i, pos.Y + singleSize*j), color);
            } 
        }
    }

    public void Move(GameTime gameTime, InputHelper ip, GraphicsDeviceManager gr)
    {
        if (ip.KeyPressed(Keys.S) && pos.Y < gr.PreferredBackBufferHeight-singleSize*size)
        {
            pos.Y += singleSize;
        }

        if (ip.KeyPressed(Keys.D))
        {
            pos.X += singleSize;
        }

        if (ip.KeyPressed(Keys.A) && pos.X != 0)
        {
            pos.X -= singleSize;
        }
    }

}

class BlockL : Block
{   
    public BlockL(Texture2D sprite)
        : base(sprite)
    {
        base.size = 4;
        array = new bool[size, size];
        color = Color.BlueViolet;
        //fill Array with block, dependent on type
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                if (j == 0 || (j == 1 && i == 0)) array[i, j] = true;
                else array[i, j] = false;
        }

    }
}

class BlockR : Block
{
    public BlockR(Texture2D sprite)
        : base(sprite)
    {
        size = 4;
        array = new bool[size, size];
        color = Color.BlueViolet;
        //fill Array with block, dependent on type
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                if (j == 1 || (j == 0 && i == 0)) array[i, j] = true;
                else array[i, j] = false;
        }
    }


}

class BlockI : Block
{
    public BlockI(Texture2D sprite)
        : base(sprite)
    {
        size = 4;
        array = new bool[size, size];
        color = Color.BlueViolet;
        //fill Array with block, dependent on type
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                if (j == 0) array[i, j] = true;
                else array[i, j] = false;
        }
    }
}

class BlockO : Block
{
    public BlockO(Texture2D sprite)
            : base(sprite)
    {
        size = 2;
        array = new bool[size, size];
        color = Color.BlueViolet;
        //fill Array with block, dependent on type
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                array[i, j] = true;
            }
        }
    }
}

class BlockS : Block
{
    public BlockS(Texture2D sprite)
    : base(sprite)
    {
        size = 3;
        array = new bool[size, size];
        color = Color.BlueViolet;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                if ((j == 1 && i<2) || (j == 0 && i>0)) array[i, j] = true;
                else array[i, j] = false;
        }
    }
}

class Block2 : Block
{
    public Block2(Texture2D sprite)
    : base(sprite)
    {
        size = 3;
        array = new bool[size, size];
        color = Color.BlueViolet;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                if ((j == 1 && i > 0 ) || (j == 0 &&  i < 2)) array[i, j] = true;
                else array[i, j] = false;
        }
    }
}

class BlockT : Block
{
    public BlockT(Texture2D sprite)
    : base(sprite)
    {
        size = 3;
        array = new bool[size, size];
        color = Color.BlueViolet;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                if (j == 1 || (j == 0 && i == 1)) array[i, j] = true;
                else array[i, j] = false;
        }

    }
}