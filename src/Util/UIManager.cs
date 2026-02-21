using System;
using System.Collections.Generic;
using System.Linq;
using MGUI.Controls;
using MGUI.Util.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Util
{
    public class UIManager
    {
        public static UIManager Instance{get; private set;}
        private Rectangle Bounds;
        private readonly GraphicsDevice _graphics;
        private readonly SpriteBatch _spriteBatch;
        public DockManager _dockManager;
        private List<Window> windows = new List<Window>();
        private MouseInteraction _mouseInteractions;
        private RasterizerState RasterizerState = new RasterizerState
                                                                    {
                                                                        ScissorTestEnable = true,
                                                                    };
        private bool _showDropTargets = false;


        public UIManager(GraphicsDevice graphics, GameWindow window)
        {
            _graphics = graphics;
            _spriteBatch = new SpriteBatch(_graphics);
            window.ClientSizeChanged += HandleLayout;
            Bounds = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);
            _mouseInteractions = new MouseInteraction();
            _dockManager = new DockManager(_graphics.Viewport.Bounds);

            Instance = this;
        }

        public void ChildDockChanged()
        {
            _dockManager.HandleDock(windows);
        }
        private void HandleLayout(object sender, EventArgs e)
        {
            Bounds = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);
            _dockManager.BoundsChanged(Bounds);
            _dockManager.HandleDock(windows);
        }
        public void Add(Window window)
        {
            windows.Add(window);
            ChildDockChanged();
        }
        public void Remove(Window window)
        {
            windows.Remove(window);
        }        
        public void Update(GameTime gameTime)
        {
            InputManager.Update();
            _mouseInteractions.Update(windows.ToList<Control>());
        }
        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState);

            List<Window> orderedWindows = windows.OrderBy(x => x.ZOrder).ToList();

            for (int i = 0; i < orderedWindows.Count; i++)
            {
                orderedWindows[i].Draw(_spriteBatch);
            }
            
            if(_showDropTargets)
            {
                _dockManager.DrawDropTargets(_spriteBatch);            
            }

            _spriteBatch.End();
        }
        public void BringToFront(Window window)
        {
            for (int i = 0; i < windows.Count; i++)
            {
                windows[i].ZOrder = 0;
            }

            window.ZOrder = 1;
        }



        public void CheckDock(Window window)
        {
            _dockManager.CheckDock(window);
        }
        public void ShowDropTargets()
        {
            _showDropTargets = true;
        }
        public void HideDropTargets()
        {
            _showDropTargets = false;
        }


    }
}
