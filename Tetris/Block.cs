
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using System;

internal class Block
{
    public bool[,] array;
    public int size;
    public int singleSize;
    public Vector2 pos;
    public Color color;
    public Texture2D spBlock;
    protected double timer, speed;

    public Block(Texture2D sprite, double pspeed)
    {
        spBlock = sprite;
        singleSize = spBlock.Width;        
        pos = new Vector2 (singleSize, 0);
        speed = pspeed;
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

    //handles all movement; input and automatic downward moving
    public void Move(GameTime gameTime, InputHelper ip, GraphicsDeviceManager gr)
    {
        timer += gameTime.ElapsedGameTime.TotalSeconds; 

        if (ip.KeyPressed(Keys.S))
        {
            pos.Y += singleSize;
            timer = 0;
        }
        if (ip.KeyPressed(Keys.Space)) 
        {
            while(pos.Y < gr.PreferredBackBufferHeight-size*singleSize)pos.Y += singleSize;
        }

        //lets the player rotate and move the block from side to side
        if (ip.KeyPressed(Keys.D) && this.IsWithinlimits(Keys.D, pos.X + singleSize))
        {
            pos.X += singleSize;
        }

        if (ip.KeyPressed(Keys.A) && this.IsWithinlimits(Keys.A, pos.X-singleSize))
        {
            pos.X -= singleSize;
        }
        if (ip.KeyPressed(Keys.Right) && this.IsWithinlimits(Keys.Right, pos.X))
        {
            this.rotateRight();
        }
        if (ip.KeyPressed(Keys.Left) && this.IsWithinlimits(Keys.Left, pos.X))
        {
            this.rotateLeft();
        }

        if (timer > speed)
        {
            pos.Y += singleSize;
            timer = 0;
        }
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

    public void rotateLeft()
    {
        //rotate block

        bool[,] temp = new bool[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                temp[i,j] = array[size - 1 - j, i];
            }
        }
        array = temp;
    }



    //checks if action a player wants to perform is within the grid, based on key being pressed & and the new x pos of the block
    private bool IsWithinlimits(Keys k, float xPos)
    {        
        
        if (k == Keys.A || k == Keys.D) 
        { 
        int limitL = 0;
        int limitR = 12 * spBlock.Width - size * singleSize;
            //when the first few rows in the arrays are empty they can go outside the screen
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (array[i, j])
                    {
                        i = size;
                        break;
                    }
                }
                if (i == size) break;
                limitL -= singleSize;
            }
            for (int i = size - 1; i > 0; i--)
            {
                for (int j = 0; j < size; j++)
                {
                    if (array[i, j])
                    {
                        i = 0;
                        break;
                    }
                }
                if (i == 0) break;
                limitR += singleSize;
            }

            return (xPos <= limitR && xPos >= limitL);
        }

        if (k == Keys.Right)
        {
            this.rotateRight();
            if (!IsWithinlimits(Keys.D, pos.X))  
            {
                rotateLeft();
                return false;
            }
            rotateLeft();
            return true;
        }

        if (k == Keys.Left)
        {
            this.rotateLeft();
            if (!IsWithinlimits(Keys.D, pos.X))
            {
                rotateRight();
                return false;
            }
            rotateRight();
            return true; 
        }

        else return false;
    }

    public bool finished(Grid pgrid)
    {
        pos.Y += singleSize;
        int gridPosX, gridPosY;
        gridPosX = (int)this.pos.X / singleSize;
        gridPosY = (int)this.pos.Y / singleSize;

        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                if (array[i, j] && pgrid.grid[gridPosX + i, gridPosY + j] || gridPosY + size > 20)
                {
                    pos.Y -= singleSize;
                    return true;
                }                
            }
        }
        pos.Y -= singleSize;
        return false;
    }


}



class BlockL : Block
{   
    public BlockL(Texture2D sprite, double pspeed)
        : base(sprite, pspeed)
    {
        size = 4;
        array = new bool[size, size];
        color = new Color(121, 70, 120);
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
    public BlockR(Texture2D sprite, double pspeed)
        : base(sprite, pspeed)
    {
        size = 4;
        array = new bool[size, size];
        color = new Color(124, 6, 6);
        
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
    public BlockI(Texture2D sprite, double pspeed)
        : base(sprite, pspeed)
    {
        size = 4;
        array = new bool[size, size];
        color = new Color(190, 1, 4);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                if (j == 1) array[i, j] = true;
                else array[i, j] = false;
        }
    }
}

class BlockO : Block
{
    public BlockO(Texture2D sprite, double pspeed)
            : base(sprite, pspeed)
    {
        size = 2;
        array = new bool[size, size];
        color = new Color(80, 98, 29);
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
    public BlockS(Texture2D sprite, double pspeed)
    : base(sprite, pspeed)
    {
        size = 3;
        array = new bool[size, size];
        color = new Color(251, 163, 14);

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
    public Block2(Texture2D sprite, double pspeed)
    : base(sprite, pspeed)
    {
        size = 3;
        array = new bool[size, size];
        color = new Color(254, 99, 1);

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
    public BlockT(Texture2D sprite, double pspeed)
    : base(sprite, pspeed)
    {
        size = 3;
        array = new bool[size, size];
        color = new Color(40, 84, 116);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
                if (j == 1 || (j == 0 && i == 1)) array[i, j] = true;
                else array[i, j] = false;
        }
    }
}