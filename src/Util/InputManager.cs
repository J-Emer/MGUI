using MGUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGUI.Util
{
    public static class InputManager
    {
        private static MouseState _currentMouse;
        private static MouseState _previousMouse;


        private static KeyboardState _previousKeys;
        private static KeyboardState _currentKeys;





        public static void Update()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            _previousKeys = _currentKeys;
            _currentKeys = Keyboard.GetState();
        }

        public static Point MousePosition => new Point(_currentMouse.X, _currentMouse.Y);

        public static bool LeftPressed => _currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released;

        public static bool LeftReleased => _currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed;

        public static bool LeftDown => _currentMouse.LeftButton == ButtonState.Pressed;

        public static int ScrollDelta => _currentMouse.ScrollWheelValue - _previousMouse.ScrollWheelValue;





        /// <summary>
        /// Checks if a key is being continously held down
        /// </summary>
        /// <param name="key"></param>
        /// <returns>bool</returns>
        public static bool GetKey(Keys key) => _currentKeys.IsKeyDown(key);
        
        /// <summary>
        /// Checks if a key was pressed this frame
        /// </summary>
        /// <param name="key"></param>
        /// <returns>bool</returns>
        public static bool GetKeyDown(Keys key) => _currentKeys.IsKeyDown(key) && !_previousKeys.IsKeyDown(key);
        
        /// <summary>
        /// Checks if a key was released this frame
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetKeyUp(Keys key) => _currentKeys.IsKeyUp(key) && _previousKeys.IsKeyDown(key);



    }
}
