using System;
using System.Collections.Generic;
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
        public DockManager DockManager;
        private List<Control> _controls = new List<Control>();
        private MouseInteraction _mouseInteractions;
        private RasterizerState RasterizerState = new RasterizerState
                                                                    {
                                                                        ScissorTestEnable = true,
                                                                    };
  
        public UIManager(GraphicsDevice graphics, GameWindow window)
        {
            _graphics = graphics;
            _spriteBatch = new SpriteBatch(_graphics);
            window.ClientSizeChanged += HandleLayout;
            Bounds = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);
            _mouseInteractions = new MouseInteraction();

            Instance = this;
        }

        private void HandleLayout(object sender, EventArgs e)
        {
            Bounds = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);
            //todo: handle dock here
        }
        public void Add(Control _control)
        {
            _controls.Add(_control);
        }
        public void Remove(Control _control)
        {
            _controls.Remove(_control);
        }        
        public void Update(GameTime gameTime)
        {
            InputManager.Update();
            _mouseInteractions.Update(_controls);
        }

        public void Draw()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState);

            for (int i = 0; i < _controls.Count; i++)
            {
                _controls[i].Draw(_spriteBatch);
            }
        
            _spriteBatch.End();
        }

    }
}
