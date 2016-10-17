using Microsoft.Xna.Framework.Input;

namespace Utilities
{
    static class KeyboardUtility
    {
        #region Member variables
        static KeyboardState myOldKeyboardState;
        static KeyboardState myNewKeyboardState;
        #endregion

        #region Public methods
        static public bool WasClicked(Keys aKey)
        {
            return myOldKeyboardState.IsKeyUp(aKey) && myNewKeyboardState.IsKeyDown(aKey);
        }

        static public bool IsHeldDown(Keys aKey)
        {
            return myOldKeyboardState.IsKeyDown(aKey) && myNewKeyboardState.IsKeyDown(aKey);
        }

        static public void Update()
        {
            myOldKeyboardState = myNewKeyboardState;
            myNewKeyboardState = Keyboard.GetState();
        }

        #endregion
    }
}
