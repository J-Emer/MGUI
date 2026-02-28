using System;
using System.Collections.Generic;
using System.Linq;
using MGUI.Controls;
using MGUI.Util.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGUI.Util
{
    public class UIManager
    {
        public static UIManager Instance{get; private set;}
        private Rectangle Bounds;
        public GraphicsDevice Graphics => _graphics;
        private readonly GraphicsDevice _graphics;
        private readonly SpriteBatch _spriteBatch;
        public DockManager _dockManager;
        private List<DockableControl> windows = new List<DockableControl>();
        private MouseInteraction _mouseInteractions;
        private KeyBoardInteraction _keyBoardInteraction;
        private RasterizerState RasterizerState = new RasterizerState
                                                                    {
                                                                        ScissorTestEnable = true,
                                                                    };
        private bool _showDropTargets = false;

        private List<ContainerControl> _overlayPanels = new List<ContainerControl>();






        public UIManager(Game game, string defaultFontName)
        {
            _graphics = game.GraphicsDevice;

            AssetLoader.Init(_graphics, game.Content.Load<SpriteFont>(defaultFontName));

            _spriteBatch = new SpriteBatch(_graphics);
            game.Window.ClientSizeChanged += HandleLayout;
            Bounds = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);
            _keyBoardInteraction = new KeyBoardInteraction();
            _mouseInteractions = new MouseInteraction(_keyBoardInteraction);
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
        public void Add(DockableControl window)
        {
            windows.Add(window);
            ChildDockChanged();
        }
        public void Remove(DockableControl window)
        {
            windows.Remove(window);
        }        
        public void Update(GameTime gameTime)
        {
            InputManager.Update();
            _mouseInteractions.Update(windows.ToList<Control>());
            _keyBoardInteraction.Update();

        }
        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState);

            Graphics.ScissorRectangle = Bounds;

            List<DockableControl> orderedWindows = windows.OrderBy(x => x.ZOrder).ToList();

            for (int i = 0; i < orderedWindows.Count; i++)
            {
                orderedWindows[i].Draw(_spriteBatch);
            }
            
            for (int i = 0; i < _overlayPanels.Count; i++)
            {
                _overlayPanels[i].Draw(_spriteBatch);
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



        public void CheckDock(DockableControl window)
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


        public void ShowOverlay(ContainerControl _control)
        {
            _overlayPanels.Add(_control);
        }
        public void RemoveOverlay(ContainerControl _control)
        {
            _overlayPanels.Remove(_control);
        }




    }
}
