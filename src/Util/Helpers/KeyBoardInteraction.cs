using System;
using MGUI.Controls;
using Microsoft.Xna.Framework.Input;

namespace MGUI.Util.Helpers
{
    public class KeyBoardInteraction
    {
        private Control _capturedControl;

        public void SetCapturedControl(Control control)
        {
            _capturedControl = control;
        }

        public void Update()
        {
            if(_capturedControl == null)
            {
                return;
            }

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if(InputManager.GetKeyDown(key))
                {
                    _capturedControl.OnKeyDown(key);
                }
                if(InputManager.GetKeyUp(key))
                {
                    _capturedControl.OnKeyUp(key);
                }       
                if(InputManager.GetKey(key))
                {
                    _capturedControl.OnKeyHeld(key);
                }                         
            }
        }
    }
}