
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

internal class Block
{
    protected bool [,] array;
    protected int size, singleSize;
    Texture2D spBlock;
    protected Color color;
    protected Vector2 pos;
    protected double timer, speed;

    public Block(Texture2D sprite)
    {
        spBlock = sprite;
        pos = new Vector2 (0, 0);
        singleSize = spBlock.Width;
        speed = 1.5;
    }

    public void rotateRight()
    {
        //rotate block

        bool[,] temp = new bool[size,size];

        for(int i = 0; i < size; i++) 
        { 
            for(int j = 0; j < size; j++)
            {
                temp[size-1-j,i] = array[i,j];
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
        timer += gameTime.ElapsedGameTime.TotalSeconds; 

        if (ip.KeyPressed(Keys.S))
        {
            pos.Y += singleSize;
            timer = 0;
        }

        if (ip.KeyPressed(Keys.D) && this.IsWithinlimitsRight())
        {
            pos.X += singleSize;
        }

        if (ip.KeyPressed(Keys.A) && this.IsWithinlimitsLeft())
        {
            pos.X -= singleSize;
        }

        if (timer > speed)
        {
            pos.Y += singleSize;
            timer = 0;
        }        
    }

    private bool IsWithinlimitsLeft()
    {
        int limitL = 0;

        //when the first few rows in the arrays are empty they can go outside the screen
        for (int i = 0; i < size - 1; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (array[i, j] == true)
                {
                    i = size;
                    break;
                }
            }
            if (i == size) break;
            limitL -= singleSize;
        }
        return ((pos.X - singleSize) >= limitL);

    }

    private bool IsWithinlimitsRight()
    {
        int limitR = 12*spBlock.Width - singleSize * size;
        for (int i = size; i < 1; i--)
        {
            for (int j = 0; j < size; j++)
            {
                if (array[i, j] == true)
                {
                    i = size;
                    break;
                }
            }
            if (i == size) break;
            limitR += singleSize;
        }

        return ((pos.X + singleSize) <= limitR);
    }

}

class BlockL : Block
{   
    public BlockL(Texture2D sprite)
        : base(sprite)
    {
        size = 4;
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