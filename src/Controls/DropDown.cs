using System;
using MGUI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGUI.Util
{
    public class DropDown : ContainerControl
    {
        private bool _showPanel = false;
        private Panel _panel;


        public DropDown() : base()
        {
            _panel = new Panel();
            _panel.BorderColor = Color.Red;
            _panel.BackgroundColor = Color.Yellow;
            _panel.Padding = 0;
            _panel.Layout = new RowLayout();
            _panel.Size = new Point(200, 30);
            _panel.Visible = _showPanel;

            Size = new Point(200, 30);
            BackgroundColor = Color.Yellow;
        }

        public override void Add(Control _control)
        {
            _panel.Add(_control);
        }

        public override void Remove(Control _control)
        {
            _panel.Remove(_control);
        }
        protected override void AfterDirty()
        {
            int _childHeight = 0;
            for (int i = 0; i < Controls.Count; i++)
            {
                _childHeight += Controls[i].Size.Y;
            }
            _panel.Position = new Point(Position.X, Bounds.Bottom + 5);
            _panel.Size = new Point(Size.X, _childHeight);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetLoader.Pixel, Bounds, BackgroundColor);

            if (BorderThickness > 0)
            {
                // Top
                spriteBatch.Draw(AssetLoader.Pixel,new Rectangle(Bounds.X, Bounds.Y, Bounds.Width, BorderThickness), BorderColor);
                // Bottom
                spriteBatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.X, Bounds.Y + Bounds.Height - BorderThickness, Bounds.Width, BorderThickness), BorderColor);
                // Left
                spriteBatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.X, Bounds.Y, BorderThickness, Bounds.Height), BorderColor);
                // Right
                spriteBatch.Draw(AssetLoader.Pixel, new Rectangle(Bounds.X + Bounds.Width - BorderThickness, Bounds.Y, BorderThickness, Bounds.Height), BorderColor);
            }              
        }
        public override void OnMouseDown(MouseEvent e)
        {
            _showPanel = !_showPanel;
            _panel.Visible = _showPanel;


            if(_showPanel)
            {
                UIManager.Instance.AddOverlayPanel(_panel);
            }
            else
            {
                UIManager.Instance.RemoveOverlayPanel(_panel);
            }
        }


    }
}