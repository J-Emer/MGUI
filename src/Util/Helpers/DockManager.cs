using System.Collections.Generic;
using MGUI.Controls;
using MGUI.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Util.Helpers
{
    public class DockManager
    {
        private Rectangle _bounds;
        private Dictionary<DockStyle, Rectangle> dropTargets = new Dictionary<DockStyle, Rectangle>();
        public int TargetSize{get;set;} = 100;

        public DockManager(Rectangle bounds)
        {
            _bounds = bounds;
            SetTargets();                        
        }
        public void BoundsChanged(Rectangle bounds)
        {
            _bounds = bounds;
            SetTargets();            
        }
        private void SetTargets()
        {
            dropTargets.Clear();

            //left
            dropTargets.Add(DockStyle.Left, new Rectangle(
                _bounds.X,
                _bounds.Y,
                TargetSize,
                _bounds.Height
            ));

            //right
            dropTargets.Add(DockStyle.Right, new Rectangle(
                _bounds.Right - TargetSize,
                _bounds.Y,
                TargetSize,
                _bounds.Height
            ));  

            //top
            dropTargets.Add(DockStyle.Top, new Rectangle(
                _bounds.X + TargetSize,
                _bounds.Y,
                _bounds.Width - (TargetSize * 2),
                TargetSize
            ));  

            //bottom
            dropTargets.Add(DockStyle.Bottom, new Rectangle(
                _bounds.X + TargetSize,
                _bounds.Bottom - TargetSize,
                _bounds.Width - (TargetSize * 2),
                TargetSize
            ));              
        }      
        public void DrawDropTargets(SpriteBatch spriteBatch)
        {
            foreach (var target in dropTargets)
            {
                spriteBatch.Draw(AssetLoader.Pixel, target.Value, Color.Yellow * 0.5f);
            }
        }
        public void CheckDock(Window _window)
        {
            foreach (var item in dropTargets)
            {
                if(_window.Bounds.Intersects(item.Value))
                {
                    _window.Dock = item.Key;
                    return;
                }
            }
        }

        public void HandleDock(List<Window> windows)
        {
            if (windows == null || windows.Count == 0)
                return;

            Rectangle remainingBounds = _bounds;

            foreach (var window in windows)
            {
                if (window.Dock == DockStyle.None)
                    continue;

                switch (window.Dock)
                {
                    case DockStyle.Left:
                    {
                        int width = window.Size.X;

                        window.Position = new Point(remainingBounds.Left, remainingBounds.Top);

                        window.Size = new Point(width, remainingBounds.Height);

                        remainingBounds = new Rectangle(
                            remainingBounds.Left + width,
                            remainingBounds.Top,
                            remainingBounds.Width - width,
                            remainingBounds.Height);

                        break;
                    }

                    case DockStyle.Right:
                    {
                        int width = window.Size.X;

                        window.Position = new Point(remainingBounds.Right - width, remainingBounds.Top);

                        window.Size = new Point(width, remainingBounds.Height);

                        remainingBounds = new Rectangle(
                            remainingBounds.Left,
                            remainingBounds.Top,
                            remainingBounds.Width - width,
                            remainingBounds.Height);

                        break;
                    }

                    case DockStyle.Top:
                    {
                        int height = window.Size.Y;

                        window.Position = new Point(remainingBounds.Left, remainingBounds.Top);

                        window.Size = new Point(remainingBounds.Width,height);

                        remainingBounds = new Rectangle(
                            remainingBounds.Left,
                            remainingBounds.Top + height,
                            remainingBounds.Width,
                            remainingBounds.Height - height);

                        break;
                    }

                    case DockStyle.Bottom:
                    {
                        int height = window.Size.Y;

                        window.Position = new Point(remainingBounds.Left, remainingBounds.Bottom - height);

                        window.Size = new Point(remainingBounds.Width,height);

                        remainingBounds = new Rectangle(
                            remainingBounds.Left,
                            remainingBounds.Top,
                            remainingBounds.Width,
                            remainingBounds.Height - height);

                        break;
                    }

                    case DockStyle.Fill:
                    {
                        window.Position = new Point(remainingBounds.Left, remainingBounds.Top);

                        window.Size = new Point(remainingBounds.Width, remainingBounds.Height);

                        remainingBounds = Rectangle.Empty;
                        break;
                    }
                }
            }
        }
    }
}
