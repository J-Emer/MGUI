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
            // Find Hovered Control (top-most first)
            // --------------------------------------------------
            Control newHovered = null;

            foreach (var control in controls.OrderByDescending(c => c.ZOrder))
            {
                var hit = control.HitTest(mousePos);

                if(hit != null)
                {
                    newHovered = hit;
                    break;
                }
            }

            // if(newHovered != null)
            // {
            //     Logger.Log(this, "Have a hovered");
            // }

            // --------------------------------------------------
            // Handle Hover Enter / Exit
            // --------------------------------------------------
            if (newHovered != _hoveredControl)
            {
                _hoveredControl?.OnMouseExit(new MouseEvent());
                newHovered?.OnMouseEnter(new MouseEvent());
                _hoveredControl = newHovered;
            }

            // --------------------------------------------------
            // Mouse Down (Capture)
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
            // Mouse Move
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
            // Mouse Up (Release Capture)
            // --------------------------------------------------
            if (InputManager.LeftReleased)
            {
                if (_capturedControl != null)
                {
                    _capturedControl.OnMouseUp(new MouseEvent());
                    _capturedControl = null;
                }
            }

            if(InputManager.ScrollDelta != 0 && _hoveredControl != null)
            {
                _hoveredControl.OnMouseScroll(new MouseEvent());
            }
        }
    }
}