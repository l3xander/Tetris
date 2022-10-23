using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


internal class InputHelper
{
    MouseState currentMouseState, previousMouseState;
    KeyboardState currentKeyboardState, previousKeyboardState;


    public void Update(GameTime gameTime)
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
    }
    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }
    public bool KeyHeld(Keys K)
    {
        return currentKeyboardState.IsKeyDown(K);
    }
    public bool MouseButtonPressed(bool left)
    {
        if (left)
        {
            return currentMouseState.LeftButton == ButtonState.Pressed
            && previousMouseState.LeftButton == ButtonState.Released;
        }
        else
        {
            return currentMouseState.RightButton == ButtonState.Pressed
            && previousMouseState.LeftButton == ButtonState.Released;
        }
    }

    public Vector2 MousePos
    {
        get { return new Vector2(currentMouseState.X, currentMouseState.Y); }
    }
}

