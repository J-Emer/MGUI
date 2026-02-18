using MGUI.Controls;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Util
{
    public class DockManager
    {
        private readonly int _dockMargin = 300; // edge detection area
        private GraphicsDevice _graphicsDevice;

        public DockStyle CurrentDock { get; private set; } = DockStyle.None;

        public Rectangle PreviewRectangle { get; private set; }

        public DockManager(GraphicsDevice graphicsDevice)
        {
            _graphicsDevice = graphicsDevice;
        }

        public void HandleDock(ContainerControl container, bool mouseReleased)
        {
            Viewport viewport = _graphicsDevice.Viewport;
            Point mouse = InputManager.MousePosition;

            CurrentDock = DockStyle.None;

            // Detect edge zones
            if (mouse.X <= _dockMargin)
                CurrentDock = DockStyle.Left;
            else if (mouse.X >= viewport.Width - _dockMargin)
                CurrentDock = DockStyle.Right;
            else if (mouse.Y <= _dockMargin)
                CurrentDock = DockStyle.Top;
            else if (mouse.Y >= viewport.Height - _dockMargin)
                CurrentDock = DockStyle.Bottom;

            UpdatePreview(viewport);

            if (mouseReleased && CurrentDock != DockStyle.None)
            {
                ApplyDock(container, viewport);
                CurrentDock = DockStyle.None;
            }
        }

        private void UpdatePreview(Viewport viewport)
        {
            switch (CurrentDock)
            {
                case DockStyle.Left:
                    PreviewRectangle = new Rectangle(0, 0, _dockMargin, viewport.Height);
                    break;

                case DockStyle.Right:
                    PreviewRectangle = new Rectangle(viewport.Width - _dockMargin, 0, _dockMargin, viewport.Height);
                    break;

                case DockStyle.Top:
                    PreviewRectangle = new Rectangle(0, 0, viewport.Width, _dockMargin);
                    break;

                case DockStyle.Bottom:
                    PreviewRectangle = new Rectangle(0, viewport.Height - _dockMargin, viewport.Width, _dockMargin);
                    break;

                default:
                    PreviewRectangle = Rectangle.Empty;
                    break;
            }
        }

        private void ApplyDock(ContainerControl container, Viewport viewport)
        {
            container.Position = new Vector2(PreviewRectangle.X, PreviewRectangle.Y).ToPoint();
            container.Size = new Vector2(PreviewRectangle.Width, PreviewRectangle.Height).ToPoint();
        }

        public void DrawPreview(SpriteBatch spriteBatch)
        {
            if (CurrentDock == DockStyle.None)
            {
                return;                
            }

            spriteBatch.Draw(AssetLoader.Pixel, PreviewRectangle, new Color(0, 120, 215, 80));
        }
    }
}
