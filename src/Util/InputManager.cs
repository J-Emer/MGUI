using MGUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGUI.Util
{
    public static class InputManager
    {
        private static MouseState _currentMouse;
        private static MouseState _previousMouse;

        private static Control _mouseCapture; //---this is the control that currently has the mouse

        public static void Update()
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
        }

        public static Point MousePosition => new Point(_currentMouse.X, _currentMouse.Y);

        public static bool LeftPressed => _currentMouse.LeftButton == ButtonState.Pressed && _previousMouse.LeftButton == ButtonState.Released;

        public static bool LeftReleased => _currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed;

        public static bool LeftDown => _currentMouse.LeftButton == ButtonState.Pressed;

        public static int ScrollDelta => _currentMouse.ScrollWheelValue - _previousMouse.ScrollWheelValue;

        // -------------------------
        // Mouse Capture
        // -------------------------

        public static void CaptureMouse(Control control)
        {
            _mouseCapture = control;
        }

        public static void ReleaseMouse(Control control)
        {
            if (_mouseCapture == control)
                _mouseCapture = null;
        }

        public static Control CapturedControl => _mouseCapture;
    }
}
