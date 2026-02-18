using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
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

        public UIManager(GraphicsDevice graphics, GameWindow window)
        {
            _graphics = graphics;
            window.ClientSizeChanged += HandleLayout;
            Bounds = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);
        }

        private void HandleLayout(object sender, EventArgs e)
        {
            Bounds = new Rectangle(0, 0, _graphics.Viewport.Width, _graphics.Viewport.Height);
            //todo: handle dock here
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw()
        {
            
        }

    }
}
