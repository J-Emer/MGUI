using Microsoft.Xna.Framework;

namespace MGUI.Util
{
    public class MouseEvent
    {
        public Point Position;
        public bool LeftDown;
        public bool LeftReleased;
        public bool RightDown;
        public bool MiddleDown;
        public int ScrollDelta;


        public MouseEvent()
        {
            Position = InputManager.MousePosition;
            LeftDown = InputManager.LeftDown;
            LeftReleased = InputManager.LeftReleased;
            RightDown = false;
            MiddleDown = false;
            ScrollDelta = InputManager.ScrollDelta;
        }


        public override string ToString()
        {
            return $"Position: {Position} | LeftDown: {LeftDown} | RightDown: {RightDown} | MiddleDown: {MiddleDown} | ScrollDelta: {ScrollDelta}";
        }
    }
}
