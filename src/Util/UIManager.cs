using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using MGUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGUI.Util
{
    public class UIManager
    {
        public static UIManager Instance{get; private set;}
        private Rectangle Bounds;
        private readonly GraphicsDevice _graphics;
        private readonly SpriteBatch _spriteBatch;
        public DockManager DockManager;
        private readonly List<ContainerControl> _controls = new();
        private List<ContainerControl> _panels = new();

        private Control _hoveredControl;
        private Control _pressedControl;   

        private MouseState _prevMouse;



        public UIManager(GraphicsDevice graphics, GameWindow gameWindow)
        {
            _graphics = graphics;
            _spriteBatch = new SpriteBatch(_graphics);

            DockManager = new DockManager(_graphics);

            gameWindow.ClientSizeChanged += ClientSizeChanged;

            Bounds = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);

            Instance = this;
        }
        private void ClientSizeChanged(object sender, EventArgs e)
        {
            Bounds = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);
            HandleLayout();
            Logger.Log(this, Bounds.ToString());
        }
        public void Add(ContainerControl _control)
        {
            _controls.Add(_control);
            _control.UIManager = this;
            HandleLayout();
        }
        public void Remove(ContainerControl _control)
        {
            _controls.Remove(_control);
            HandleLayout();
        }
        private void HandleLayout()
        {
            //todo: handle dock here            
        }
        public void Update(GameTime gameTime)
        {
            InputManager.Update();

            MouseEvent e = new MouseEvent
            {
                Position = InputManager.MousePosition,
                LeftDown = InputManager.LeftDown,
                LeftReleased = InputManager.LeftReleased,
                RightDown = false,
                MiddleDown = false,
                ScrollDelta = InputManager.ScrollDelta
            };

            // ==========================
            // 1. HIT TEST (Top-most first, recursive)
            // ==========================
            Control hit = HitTest(e.Position);

            // ==========================
            // 2. Hover Handling
            // ==========================
            if (_hoveredControl != hit)
            {
                _hoveredControl?.OnMouseLeave(e);
                hit?.OnMouseEnter(e);

                _hoveredControl = hit;
            }

            _hoveredControl?.OnMouseHover(e);

            // ==========================
            // 3. Mouse Down (capture)
            // ==========================
            if (InputManager.LeftPressed) // single frame press
            {
                _pressedControl = hit;

                if (_pressedControl != null)
                {
                    _pressedControl.OnMouseDown(e);

                    if(_pressedControl is ContainerControl)
                    {
                        BringToFrontRoot((ContainerControl)_pressedControl);
                    }
                }
            }

            // ==========================
            // 4. Mouse Move (goes to pressed control if dragging)
            // ==========================
            if (InputManager.LeftDown && _pressedControl != null)
            {
                _pressedControl.OnMouseMove(e);
            }

            // ==========================
            // 5. Mouse Up + Click
            // ==========================
            if (e.LeftReleased)
            {
                if (_pressedControl != null)
                {
                    _pressedControl.OnMouseUp(e);

                    if (_pressedControl == hit)
                    {
                        _pressedControl.OnMouseUp(e);
                    }
                }

                _pressedControl = null;
            }
        }
        private Control HitTest(Point position)
        {

            if(_panels.Count != 0)
            {
                for (int i = _panels.Count - 1; i >= 0; i--)
                {
                    var p = _panels[i];

                    if (!p.Visible)
                    {
                        continue;                    
                    }

                    // Check children first
                    if (p is ContainerControl container)
                    {
                        var childHit = p.HitTest(position);
                        if (childHit != null)
                        {
                            return childHit;                        
                        }
                    }

                    if (p.Bounds.Contains(position))
                    {
                        return p;                    
                    }
                }                
            }


            for (int i = _controls.Count - 1; i >= 0; i--)
            {
                var c = _controls[i];

                if (!c.Visible)
                {
                    continue;                    
                }

                // Check children first
                if (c is ContainerControl container)
                {
                    var childHit = c.HitTest(position);
                    if (childHit != null)
                    {
                        return childHit;                        
                    }
                }

                if (c.Bounds.Contains(position))
                {
                    return c;                    
                }
            }

            return null;
        }
        private void BringToFrontRoot(ContainerControl control)
        {
            if(_controls.Contains(control))
            {
                _controls.Remove(control);
                _controls.Add(control);
            }
        }
        public void Draw()
        {
            _spriteBatch.Begin();

            for (int i = 0; i < _controls.Count; i++)
            {
                _controls[i].Draw(_spriteBatch);
            }
        
            for (int i = 0; i < _panels.Count; i++)
            {
                _panels[i].Draw(_spriteBatch);
            }

            DockManager.DrawPreview(_spriteBatch);

            _spriteBatch.End();
        }


        public void AddOverlayPanel(ContainerControl _control)
        {
            _panels.Add(_control);
        }
        public void RemoveOverlayPanel(ContainerControl _control)
        {
            _panels.Remove(_control);
        }







    }
}
