using System.Collections.Generic;
using System.Linq;
using MGUI.Controls;
using Microsoft.Xna.Framework;

namespace MGUI.Util.Helpers
{
    public class MouseInteraction
    {
        private Control _hoveredControl;
        private Control _capturedControl;

        public void Update(List<Control> controls)
        {
            Point mousePos = InputManager.MousePosition;

            // --------------------------------------------------
            // 1️⃣ Find Hovered Control (top-most first)
            // --------------------------------------------------
            Control newHovered = null;

            foreach (var control in controls.OrderByDescending(c => c.ZOrder))
            {
                if (control.Bounds.Contains(mousePos))
                {
                    newHovered = control;
                    break;
                }
            }

            // --------------------------------------------------
            // 2️⃣ Handle Hover Enter / Exit
            // --------------------------------------------------
            if (newHovered != _hoveredControl)
            {
                _hoveredControl?.OnMouseExit(new MouseEvent());
                newHovered?.OnMouseEnter(new MouseEvent());
                _hoveredControl = newHovered;
            }

            // --------------------------------------------------
            // 3️⃣ Mouse Down (Capture)
            // --------------------------------------------------
            if (InputManager.LeftPressed)
            {
                if (_hoveredControl != null)
                {
                    _capturedControl = _hoveredControl;
                    _capturedControl.OnMouseDown(new MouseEvent());
                }
            }

            // --------------------------------------------------
            // 4️⃣ Mouse Move
            // --------------------------------------------------
            if (_capturedControl != null)
            {
                // If dragging, send move to captured
                //_capturedControl.OnMouseDrag();
            }
            else if (_hoveredControl != null)
            {
                // Otherwise normal hover move
                _hoveredControl.OnMouseHover(new MouseEvent());
            }

            // --------------------------------------------------
            // 5️⃣ Mouse Up (Release Capture)
            // --------------------------------------------------
            if (InputManager.LeftReleased)
            {
                if (_capturedControl != null)
                {
                    _capturedControl.OnMouseUp(new MouseEvent());
                    _capturedControl = null;
                }
            }
        }
    }
}